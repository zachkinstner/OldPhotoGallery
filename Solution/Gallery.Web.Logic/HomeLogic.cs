using System.Collections.Generic;
using Fabric.AppBase.CSharp.Standard.Web.Logic;
using Fabric.Clients.CSharp.Fluent;
using Gallery.Domain;
using Gallery.Web.Logic.Dto;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace Gallery.Web.Logic {

	/*================================================================================================*/
	public class HomeLogic : LogicBase {


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public FabVersion GetFabricVersion() {
			return Fabric.Core.Version.Get();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public IList<WebAlbum> GetAlbums() {
			IList<WebAlbum> albums = null;
			IList<WebAlbumTag> tags = null;

			AddTxFunc(delegate(ISession pSession) {
				Photo phoAlias = null;
				Tag tagAlias = null;
				WebAlbumTag dtoTag = null;

				albums = GetAlbumQuery(pSession)
					.OrderBy(Projections.Max(() => phoAlias.ExifDTOrig)).Desc
					.List<WebAlbum>();

				tags = pSession.QueryOver<Tag>(() => tagAlias)
					.JoinQueryOver<PhotoTag>(t => t.PhotoTags, JoinType.InnerJoin)
					.JoinQueryOver<Photo>(pt => pt.Photo, () => phoAlias, JoinType.InnerJoin)
					.SelectList(list => list
						.SelectGroup(t => t.Id).WithAlias(() => dtoTag.Id)
						.SelectGroup(() => phoAlias.Album.Id).WithAlias(() => dtoTag.AlbumId)
						.SelectMin(t => t.Name).WithAlias(() => dtoTag.Name)
						.SelectCount(t => t.Id).WithAlias(() => dtoTag.NumTags)
					)
					.OrderBy(Projections.Count(() => tagAlias.Id)).Desc
					.TransformUsing(Transformers.AliasToBean<WebAlbumTag>())
					.List<WebAlbumTag>();
			});

			ExecuteTxFuncs();

			////

			var tagsMap = new Dictionary<int, IList<WebAlbumTag>>();

			foreach ( WebAlbumTag t in tags ) {
				if ( !tagsMap.ContainsKey(t.AlbumId) ) {
					tagsMap.Add(t.AlbumId, new List<WebAlbumTag>());
				}

				tagsMap[t.AlbumId].Add(t);
			}

			for ( int i = 0 ; i < albums.Count ; ++i ) {
				WebAlbum a = albums[i];
				a.Index = i;
				a.Tags = (tagsMap.ContainsKey(a.AlbumId) ? tagsMap[a.AlbumId] : new List<WebAlbumTag>());
			}

			return albums;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		internal static IQueryOver<Album, Album> GetAlbumQuery(ISession pSession) {
			Album albAlias = null;
			Photo phoAlias = null;
			WebAlbum dto = null;

			return pSession.QueryOver<Album>(() => albAlias)
				.JoinAlias(a => a.Photos, () => phoAlias, JoinType.InnerJoin)
				.SelectList(list => list
					.SelectGroup(a => a.Id).WithAlias(() => dto.AlbumId)
					.SelectMin(a => a.Title).WithAlias(() => dto.Title)
					.SelectCount(() => phoAlias.Id).WithAlias(() => dto.NumPhotos)
					.SelectMin(() => phoAlias.Id).WithAlias(() => dto.FirstPhotoId)
					.SelectMin(() => phoAlias.ExifDTOrig).WithAlias(() => dto.StartDate)
					.SelectMax(() => phoAlias.ExifDTOrig).WithAlias(() => dto.EndDate)
					.SelectSubQuery(
						QueryOver.Of<Photo>()
						.Where(p => p.Album.Id == albAlias.Id && p.Favorite > 0)
						.ToRowCountQuery()
					).WithAlias(() => dto.NumFavs)
				)
				.TransformUsing(Transformers.AliasToBean<WebAlbum>());
		}

	}

}
using System.Collections.Generic;
using System.Linq;
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
		public IList<WebAlbum> GetAlbums() {
			IList<WebAlbum> albums = null;

			AddTxFunc(delegate(ISession pSession) {
				Photo phoAlias = null;

				albums = GetAlbumQuery(pSession)
					.OrderBy(Projections.Max(() => phoAlias.ExifDTOrig)).Desc
					.List<WebAlbum>();
			});

			ExecuteTxFuncs();
			return albums;
		}

		/*--------------------------------------------------------------------------------------------*/
		internal static IQueryOver<Album, Album> GetAlbumQuery(ISession pSession) {
			Album albAlias = null;
			Photo phoAlias = null;
			WebAlbum dto = null;

			return pSession.QueryOver<Album>(() => albAlias)
				.JoinAlias(a => a.Photos, () => phoAlias, JoinType.LeftOuterJoin)
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


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public FabUser GetFabMe() {
			return Fabric.Core.Me.Get();
		}

		/*--------------------------------------------------------------------------------------------*/
		public FabMemberCore GetFabMember(FabUserKey pUserKey) {
			IList<FabMemberCore> members = Fabric.Core.Myapp.Members.Get();
			return members.FirstOrDefault(m => m.UserKey.Id == pUserKey.Id);
		}

	}

}
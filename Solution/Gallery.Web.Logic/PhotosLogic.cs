using System.Collections.Generic;
using Fabric.AppBase.CSharp.Standard.Web.Logic;
using Gallery.Domain;
using Gallery.Web.Logic.Dto;
using NHibernate;

namespace Gallery.Web.Logic {

	/*================================================================================================*/
	public class PhotosLogic : LogicBase {


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public WebAlbum GetAlbum(int pAlbumId) {
			WebAlbum album = null;

			AddTxFunc(delegate(ISession pSession) {
				album = HomeLogic.GetAlbumQuery(pSession)
					.Where(a => a.Id == pAlbumId)
					.SingleOrDefault<WebAlbum>();
			});

			ExecuteTxFuncs();
			return album;
		}

		/*--------------------------------------------------------------------------------------------*/
		public IList<WebPhotoCore> GetPhotos(List<int> pPhotoIds) {
			IList<Photo> photos = null;

			AddTxFunc(delegate(ISession pSession) {
				photos = pSession.QueryOver<Photo>()
					.OrderBy(p => p.ExifDTOrig).Asc
					.WhereRestrictionOn(p => p.Id).IsIn(pPhotoIds)
					.List<Photo>();
			});

			ExecuteTxFuncs();

			////

			var webPhotos = new List<WebPhotoCore>();

			foreach ( Photo p in photos ) {
				webPhotos.Add(new WebPhotoCore(p));
			}

			return webPhotos;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public List<int> GetAlbumPhotoIds(int pAlbumId) {
			IList<int> ids = null;

			AddTxFunc(delegate(ISession pSession) {
				ids = pSession.QueryOver<Photo>()
					.Where(p => p.Album.Id == pAlbumId)
					.OrderBy(p => p.ExifDTOrig).Asc
					.Select(p => p.Id)
					.List<int>();
			});

			ExecuteTxFuncs();
			return new List<int>(ids);
		}

	}

}
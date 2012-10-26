using System.Collections.Generic;
using System.Web.Mvc;
using Fabric.AppBase.CSharp.Standard.Web.Application;
using Gallery.Web.Logic;
using Gallery.Web.Models.Photos;

namespace Gallery.Web.Controllers {
	
	/*================================================================================================*/
	public class PhotosController : GalleryMasterController {

		private PhotosLogic vPhotos;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public PhotosController(IWebSessionBaseProvider pSessProv, PhotosLogic pPho) : base(pSessProv) {
			vPhotos = pPho;
			PrepareLogic(vPhotos);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ActionResult Albums(int? id) {
			if ( id == null ) { return Show404(); }

			var m = NewModel<PhotosAlbumModel>();
			m.Album = vPhotos.GetAlbum((int)id);
			if ( m.Album == null ) { return Show404(); }

			List<int> phoIds = vPhotos.GetAlbumPhotoIds(m.Album.AlbumId);
			GallerySession.ActivePhotos.Update(phoIds, "Album: "+m.Album.Title, null);

			m.Photos = vPhotos.GetPhotos(phoIds);
			return View(m);
		}

	}

}
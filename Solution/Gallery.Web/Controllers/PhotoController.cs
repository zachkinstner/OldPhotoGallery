using System.Web.Mvc;
using Fabric.AppBase.CSharp.Standard.Web.Application;
using Gallery.Web.Logic;
using Gallery.Web.Logic.Dto;
using Gallery.Web.Models.Photo;

#if !DEBUG
using Gallery.Web.Logic.Util;
#endif

namespace Gallery.Web.Controllers {
	
	/*================================================================================================*/
	public class PhotoController : GalleryMasterController {

		private readonly PhotoLogic vPhoto;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public PhotoController(IWebSessionBaseProvider pSessProv, PhotoLogic pPhoto) : base(pSessProv) {
			vPhoto = pPhoto;
			PrepareLogic(vPhoto);
		}

		/*--------------------------------------------------------------------------------------------*/
		public bool CheckFullSizeExists(WebPhoto pPhoto) {
#if !DEBUG
			string path = Server.MapPath("~"+GalleryUtil.BuildPhotoPath(
				pPhoto.AlbumId, pPhoto.PhotoId, GalleryUtil.PhotoSize.Full));
			return System.IO.File.Exists(path);
#else
			return true;
#endif
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ActionResult Index(int? id) {
			if ( id == null ) { return Show404(); }

			var m = NewModel<PhotoIndexModel>();
			m.Photo = vPhoto.GetPhoto((int)id);
			m.FullSizeExists = CheckFullSizeExists(m.Photo);
			if ( m.Photo == null ) { return Show404(); }

			return View(m);
		}

	}

}
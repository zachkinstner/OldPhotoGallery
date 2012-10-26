using System.Web.Mvc;
using Fabric.AppBase.CSharp.Standard.Web.Application;
using Gallery.Web.Logic;
using Gallery.Web.Models.Photo;

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


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ActionResult Index(int? id) {
			if ( id == null ) { return Show404(); }

			var m = NewModel<PhotoIndexModel>();
			m.Photo = vPhoto.GetPhoto((int)id);
			if ( m.Photo == null ) { return Show404(); }

			return View(m);
		}

	}

}
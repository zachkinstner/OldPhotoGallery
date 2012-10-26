using System;
using System.Web.Mvc;
using Fabric.AppBase.CSharp.Standard.Web.Application;
using Fabric.AppBase.CSharp.Standard.Web.Controllers;
using Gallery.Web.Application;
using Gallery.Web.Models.Shared;

namespace Gallery.Web.Controllers {
	
	/*================================================================================================*/
	public class GalleryMasterController : MasterController {

		public GalleryWebSession GallerySession { get; private set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public GalleryMasterController(IWebSessionBaseProvider pSessProv) : base(pSessProv) {
			GallerySession = (WebSession as GalleryWebSession);

			if ( GallerySession == null ) {
				throw new Exception("Invalid GallerySession: "+WebSession);
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public ActionResult Show404() {
			var m = NewModel<Gallery404Model>();
			return View("404", m);
		}

	}

}
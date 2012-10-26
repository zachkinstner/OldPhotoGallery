using System.Web.Mvc;
using Fabric.AppBase.CSharp.Standard.Web.Application;
using Fabric.AppBase.CSharp.Standard.Web.Controllers;
using Gallery.Web.Logic;
using Gallery.Web.Models.Home;

namespace Gallery.Web.Controllers {
	
	/*================================================================================================*/
	public class HomeController : GalleryMasterController {

		private HomeLogic vHome;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public HomeController(IWebSessionBaseProvider pSessProv, HomeLogic pHome) : base(pSessProv) {
			vHome = pHome;
			PrepareLogic(vHome);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ActionResult Index() {
			var m = NewModel<HomeIndexModel>();
			m.Albums = vHome.GetAlbums();
			return View(m);
		}

	}

}
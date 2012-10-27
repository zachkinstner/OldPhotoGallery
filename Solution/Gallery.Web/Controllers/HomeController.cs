using System.Web.Mvc;
using Fabric.AppBase.CSharp.Standard.Web.Application;
using Fabric.Clients.CSharp.Fluent;
using Gallery.Web.Logic;
using Gallery.Web.Models.Home;

namespace Gallery.Web.Controllers {
	
	/*================================================================================================*/
	public class HomeController : GalleryMasterController {

		private readonly HomeLogic vHome;


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


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ActionResult FabricOauthRedirect(string error=null, string error_description=null) {
			if ( error != null ) {
				Response.Write("<p><b>"+error+"</b><br/>"+error_description+"</p>");
				return null;
			}

			FabOauthAccess oa = GallerySession.Fabric.PersonSession.HandleGrantCodeRedirect(Request);
			GallerySession.ActiveUser = vHome.GetFabMe();
			GallerySession.ActiveMember = vHome.GetFabMember(GallerySession.ActiveUser.Key);
			ReloadPageAndClosePopup();
			return null;
		}

		/*--------------------------------------------------------------------------------------------*/
		private void ReloadPageAndClosePopup() {
			Response.Write(
				"<script type='text/javascript'>"+
					"window.opener.location.reload();"+
					"window.close();"+
				"</script>"
			);
		}

	}

}
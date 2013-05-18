using Fabric.AppBase.CSharp.Standard.Web.Models;
using Gallery.Web.Controllers;
using Gallery.Web.Models.Shared;

namespace Gallery.Web.Models.Account {

	/*================================================================================================*/
	public abstract class AccountModel : GalleryModel {


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override string ControllerTitle { get { return "Account"; } }

		/*--------------------------------------------------------------------------------------------*/
		public ActionLinkModel<AccountController> IndexLink {
			get {
				return new ActionLinkModel<AccountController>(x => x.Index(), "Account");
			}
		}
		
		/*--------------------------------------------------------------------------------------------*/
		public ActionLinkModel<AccountController> AddAlbumLink {
			get {
				return new ActionLinkModel<AccountController>(x => x.AddAlbum(), "Add Album");
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public ActionLinkModel<AccountController> UploadLink {
			get {
				return new ActionLinkModel<AccountController>(x => x.Upload(), "Upload");
			}
		}
		
	}

}
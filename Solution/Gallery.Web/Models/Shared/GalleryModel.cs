using Fabric.AppBase.CSharp.Standard.Web.Models;
using Gallery.Web.Application;

namespace Gallery.Web.Models.Shared {

	/*================================================================================================*/
	public abstract class GalleryModel : ModelBase {

		
		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public GalleryWebSession GallerySession {
			get { return (WebSession as GalleryWebSession); }
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override string HtmlPageTitle {
			get { return "Kinstner Gallery :: "+ControllerTitle+" > "+PageTitle; }
		}

		/*--------------------------------------------------------------------------------------------*/
		public override string AreaTitle { get { return null; } }

	}

}
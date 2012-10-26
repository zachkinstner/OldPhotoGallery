using Fabric.AppBase.CSharp.Standard.Web.Application;

namespace Gallery.Web.Application {

	/*================================================================================================*/
	public class GalleryWebSession : WebSessionBase {

		public PhotoCollection ActivePhotos { get; private set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public GalleryWebSession() {
			ActivePhotos = new PhotoCollection();
		}

	}

}
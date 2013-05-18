using Fabric.AppBase.CSharp.Standard.Web.Application;
using Fabric.Clients.CSharp.Fluent;

namespace Gallery.Web.Application {

	/*================================================================================================*/
	public class GalleryWebSession : WebSessionBase {

		public PhotoCollection ActivePhotos { get; private set; }
		public FabUser ActiveUser { get; set; }
		public FabMemberCore ActiveMember { get; set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public GalleryWebSession() {
			ActivePhotos = new PhotoCollection();
		}

		/*--------------------------------------------------------------------------------------------*/
		public bool IsAdminUser {
			get {
				return (Fabric.PersonSession.IsAuthenticated && ActiveMember.MemberTypeKey.Id == 7);
			}
		}

	}

}
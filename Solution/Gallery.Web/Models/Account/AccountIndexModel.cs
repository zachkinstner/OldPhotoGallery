namespace Gallery.Web.Models.Account {

	/*================================================================================================*/
	public class AccountIndexModel : AccountModel {
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override string PageTitle {
			get {
				return (GallerySession.ActiveUser == null ? "" : GallerySession.ActiveUser.Name);
			}
		}

	}

}
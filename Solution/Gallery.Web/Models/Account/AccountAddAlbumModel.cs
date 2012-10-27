using System.ComponentModel.DataAnnotations;

namespace Gallery.Web.Models.Account {

	/*================================================================================================*/
	public class AccountAddAlbumModel : AccountModel {

		[Required]
		[StringLength(64, ErrorMessage="Title cannot exceed 64 characters.")]
		public string Title { get; set; }

		public int SuccessAlbumId { get; set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override string PageTitle { get { return "Add Album"; } }

	}

}
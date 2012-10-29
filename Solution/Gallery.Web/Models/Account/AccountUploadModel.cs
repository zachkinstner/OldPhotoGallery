using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Gallery.Web.Models.Account {

	/*================================================================================================*/
	public class AccountUploadModel : AccountModel {


		[Required]
		[Display(Name="AlbumId")]
		public int AlbumId { get; set; }

		[Required]
		[Display(Name="Image Files")]
		public IEnumerable<HttpPostedFileBase> Files { get; set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override string PageTitle { get { return "Upload Photos"; } }

	}

}
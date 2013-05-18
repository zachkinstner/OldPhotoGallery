using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Gallery.Web.Logic.Dto;

namespace Gallery.Web.Models.Account {

	/*================================================================================================*/
	public class AccountUploadModel : AccountModel {

		[Required]
		[Display(Name="Album")]
		public int? SelectedAlbumId { get; set; }
		public IList<WebAlbumCore> AlbumList { get; set; }

		[Required]
		[Display(Name="Image Files")]
		public IEnumerable<HttpPostedFileBase> Files { get; set; }

		public IList<WebUploadResult> UploadResults { get; set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override string PageTitle { get { return "Upload Photos"; } }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public IEnumerable<SelectListItem> AlbumItems {
			get {
				var items = new List<SelectListItem>();

				foreach ( WebAlbumCore a in AlbumList ) {
					var item = new SelectListItem();
					item.Text = a.AlbumId+": "+a.Title;
					item.Value = a.AlbumId+"";
					items.Add(item);

					if ( SelectedAlbumId != null ) {
						item.Selected = (a.AlbumId == (int)SelectedAlbumId);
					}
				}

				return items;
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public WebAlbumCore SelectedAlbum {
			get {
				if ( SelectedAlbumId != null ) {
					foreach ( WebAlbumCore a in AlbumList ) {
						if ( a.AlbumId == (int)SelectedAlbumId ) { return a; }
					}
				}

				return null;
			}
		}
	}

}
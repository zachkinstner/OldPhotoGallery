using System;
using System.Collections.Generic;
using Gallery.Web.Logic.Dto;

namespace Gallery.Web.Models.Photos {

	/*================================================================================================*/
	public class PhotosAlbumModel : PhotosModel {
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public WebAlbum Album { get; set; }
		public IList<WebPhotoCore> Photos { get; set; }

		/*--------------------------------------------------------------------------------------------*/
		public override string PageTitle {
			get {
				return (Album == null ? "" : Album.Title);
			}
		}

	}

}
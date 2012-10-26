using System;
using Gallery.Domain;
using Gallery.Web.Logic.Util;

namespace Gallery.Web.Logic.Dto {

	/*================================================================================================*/
	public class WebPhotoCore {
		
		public int PhotoId { get; internal set; }
		public string ImgName { get; internal set; }
		public int Favorite { get; internal set; }
		public int AlbumId { get; internal set; }
		public DateTime ExifDtOrig { get; internal set; }
		public int FabricArtifactId { get; internal set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public WebPhotoCore() {}

		/*--------------------------------------------------------------------------------------------*/
		public WebPhotoCore(Photo p) {
			PhotoId = p.Id;
			ImgName = p.ImgName;
			Favorite = p.Favorite;
			AlbumId = p.Album.Id;
			ExifDtOrig = p.ExifDTOrig;
			FabricArtifactId = p.FabricArtifactId;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public string ImageUrl {
			get {
				return GalleryUtil.BuildPhotoUrl(AlbumId, PhotoId, GalleryUtil.PhotoSize.Med);
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public string ThumbUrl {
			get {
				return GalleryUtil.BuildPhotoUrl(AlbumId, PhotoId, GalleryUtil.PhotoSize.Sm);
			}
		}

	}

}
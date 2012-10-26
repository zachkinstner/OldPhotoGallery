using Gallery.Web.Logic.Dto;

namespace Gallery.Web.Models.Photo {

	/*================================================================================================*/
	public class PhotoIndexModel : PhotoModel {
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public WebPhoto Photo { get; set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public int PhotoIndex {
			get {
				var ids = GallerySession.ActivePhotos.PhotoIds;
				if ( ids == null ) { return 0; }
				return GallerySession.ActivePhotos.FindPhotoIndex(Photo.PhotoId);
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public int PhotoCount {
			get {
				var ids = GallerySession.ActivePhotos.PhotoIds;
				return (ids == null ? 1 : ids.Count);
			}
		}
		
		/*--------------------------------------------------------------------------------------------*/
		public int PrevPhotoId {
			get {
				return GallerySession.ActivePhotos.FindPhotoOffset(Photo.PhotoId, -1);
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		public int NextPhotoId {
			get {
				return GallerySession.ActivePhotos.FindPhotoOffset(Photo.PhotoId, 1);
			}
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override string PageTitle {
			get {
				return (Photo == null ? "" : Photo.PhotoId+"");
			}
		}

	}

}
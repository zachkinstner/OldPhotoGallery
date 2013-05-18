using System.Collections.Generic;

namespace Gallery.Web.Application {

	/*================================================================================================*/
	public class PhotoCollection {

		public List<int> PhotoIds { get; private set; }
		public string Title { get; private set; }
		public string ReturnUrl { get; private set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public void Reset() {
			PhotoIds = null;
			Title = "";
			ReturnUrl = "";
		}

		/*--------------------------------------------------------------------------------------------*/
		public void Update(List<int> pPhotoIds, string pTitle, string pReturnUrl) {
			PhotoIds = pPhotoIds;
			Title = pTitle;
			ReturnUrl = pReturnUrl;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public int FindPhotoIndex(int pPhotoId) {
			if ( PhotoIds == null ) { return -1; }
			int n = PhotoIds.Count;

			for ( int i = 0 ; i < n ; ++i ) {
				if ( PhotoIds[i] == pPhotoId ) { return i; }
			}

			return -1;
		}

		/*--------------------------------------------------------------------------------------------*/
		public int FindPhotoOffset(int pPhotoId, int pOffset) {
			int i = FindPhotoIndex(pPhotoId);

			if ( PhotoIds == null || i == -1 ) {
				return -1;
			}

			i += pOffset;
			
			if ( i < 0 || i >= PhotoIds.Count ) {
				return -1;
			}

			return PhotoIds[i];
		}

	}

}
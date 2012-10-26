﻿using System;
using System.Collections.Generic;

namespace Gallery.Web.Logic.Dto {

	/*================================================================================================*/
	public class WebAlbum {

		public int AlbumId { get; internal set; }
		public int Index { get; internal set; }
		public string Title { get; internal set; }
		public int NumPhotos { get; internal set; }
		public int NumFavs { get; internal set; }
		public int FirstPhotoId { get; internal set; }
		public DateTime StartDate { get; internal set; }
		public DateTime EndDate { get; internal set; }
		public IList<WebAlbumTag> Tags { get; internal set; }


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public WebPhoto FirstPhoto {
			get {
				var p = new WebPhoto();
				p.AlbumId = AlbumId;
				p.PhotoId = FirstPhotoId;
				p.ImgName = Title;
				return p;
			}
		}

	}

}
using System;
using System.Collections.Generic;
using Fabric.AppBase.CSharp.Standard.Domain;

namespace Gallery.Domain {
	
	/*================================================================================================*/
	public class Photo : EntityWithId<int> {

		public virtual Album Album { get; set; }
		public virtual string ImgName { get; set; }
		public virtual int Favorite { get; set; }
		
		public virtual DateTime ExifDTOrig { get; set; }
		public virtual double ExifExposureTime { get; set; }
		public virtual double ExifISOSpeed { get; set; }
		public virtual double ExifFNumber { get; set; }
		public virtual double ExifFocalLength { get; set; }
		
		public virtual int FabricArtifactId { get; set; }
		public virtual int FabricTalkId { get; set; }
		
		public virtual IList<PhotoTag> PhotoTags { get; set; }
		public virtual IList<PhotoMeta> PhotoMetas { get; set; }

    }

}

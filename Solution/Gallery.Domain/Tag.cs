using System.Collections.Generic;
using Fabric.AppBase.CSharp.Standard.Domain;

namespace Gallery.Domain {
	
	/*================================================================================================*/
	public class Tag : EntityWithId<int> {

		public virtual string Name { get; set; }
		
		public virtual IList<PhotoTag> PhotoTags { get; set; }

    }

}

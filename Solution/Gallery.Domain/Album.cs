using System.Collections.Generic;
using Fabric.AppBase.CSharp.Standard.Domain;

namespace Gallery.Domain {
	
	/*================================================================================================*/
	public class Album : EntityWithId<int> {

		public virtual string Title { get; set; }
		public virtual string LocalPath { get; set; }
		
		public virtual IList<Photo> Photos { get; set; }

    }

}

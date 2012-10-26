using Fabric.AppBase.CSharp.Standard.Domain;

namespace Gallery.Domain {
	
	/*================================================================================================*/
	public class PhotoTag : EntityWithId<int> {

		public virtual Photo Photo { get; set; }
		public virtual Tag Tag { get; set; }

    }

}

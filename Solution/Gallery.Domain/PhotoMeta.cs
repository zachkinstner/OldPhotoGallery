using Fabric.AppBase.CSharp.Standard.Domain;

namespace Gallery.Domain {
	
	/*================================================================================================*/
	public class PhotoMeta : EntityWithId<int> {

		public virtual Photo Photo { get; set; }
		public virtual string Label { get; set; }
		public virtual string Type { get; set; }
		public virtual string Value { get; set; }

    }

}

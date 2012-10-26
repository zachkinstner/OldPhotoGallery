using System.Reflection;
using Gallery.Domain;

namespace Gallery.Web.Logic {

	/*================================================================================================*/
	public class GlobalLogic {


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public static Assembly GetDomainAssembly() {
			return Assembly.GetAssembly(typeof(Photo));
		}
	}

}
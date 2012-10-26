using System.Collections.Generic;
using Gallery.Web.Logic.Dto;

namespace Gallery.Web.Models.Home {

	/*================================================================================================*/
	public class HomeIndexModel : HomeModel {
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public IList<WebAlbum> Albums { get; set; }

		/*--------------------------------------------------------------------------------------------*/
		public override string HtmlPageTitle { get { return "Kinstner Gallery"; } }
		public override string PageTitle { get { return "Index"; } }

	}

}
﻿using Fabric.AppBase.CSharp.Standard.Web.Logic;
using Fabric.Clients.CSharp.Fluent;

namespace Gallery.Web.Logic {

	/*================================================================================================*/
	public class AccountLogic : LogicBase {


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public FabUser GetFabMe() {
			return Fabric.Core.Me.Get();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public int? AddAlbum(string pTitle) {
			return 62;
		}

	}

}
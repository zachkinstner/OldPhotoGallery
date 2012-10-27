using Fabric.AppBase.CSharp.Standard.Web.Logic;
using Fabric.Clients.CSharp.Fluent;
using Gallery.Domain;
using NHibernate;

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
			Album album = null;

			AddTxFunc(delegate(ISession pSession) {
				album = new Album();
				album.Title = pTitle;
				album.LocalPath = "";
				pSession.Save(album);
			});

			ExecuteTxFuncs();
			return album.Id;
		}

	}

}
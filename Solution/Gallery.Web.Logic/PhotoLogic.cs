using Fabric.AppBase.CSharp.Standard.Web.Logic;
using Gallery.Domain;
using Gallery.Web.Logic.Dto;
using NHibernate;
using NHibernate.SqlCommand;

namespace Gallery.Web.Logic {

	/*================================================================================================*/
	public class PhotoLogic : LogicBase {


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public WebPhoto GetPhoto(int pPhotoId) {
			Photo photo = null;

			AddTxFunc(delegate(ISession pSession) {
				photo = pSession.QueryOver<Photo>()
					.Where(p => p.Id == pPhotoId)
					.JoinQueryOver(p => p.Album, JoinType.InnerJoin)
					.SingleOrDefault();
			});

			ExecuteTxFuncs();
			return (photo == null ? null : new WebPhoto(photo));
		}

	}

}
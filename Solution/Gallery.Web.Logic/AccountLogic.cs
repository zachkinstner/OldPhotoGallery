using System.Collections.Generic;
using System.Web;
using Fabric.AppBase.CSharp.Standard.Web.Logic;
using Fabric.Clients.CSharp.Fluent;
using Gallery.Domain;
using Gallery.Web.Logic.Dto;
using NHibernate;
using NHibernate.Transform;

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


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public IList<WebAlbumCore> GetAlbumCores() {
			IList<WebAlbumCore> albums = null;

			AddTxFunc(delegate(ISession pSession) {
				WebAlbumCore dto = null;

				albums = pSession.QueryOver<Album>()
					.OrderBy(a => a.Id).Desc
					.SelectList(sl => sl
						.Select(a => a.Id).WithAlias(() => dto.AlbumId)
						.Select(a => a.Title).WithAlias(() => dto.Title)
					)
					.TransformUsing(Transformers.AliasToBean<WebAlbumCore>())
					.List<WebAlbumCore>();
			});

			ExecuteTxFuncs();
			return albums;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public IList<WebUploadResult> SaveFiles(int pAlbumId, string pUploadDir,
															IEnumerator<HttpPostedFileBase> pFiles) {
			var upload = new AccountUploadLogic(pAlbumId, pUploadDir, pFiles);
			upload.SessionFactory = SessionFactory;

			//var thread = new Thread(upload.SaveFiles);
			//thread.Start();
			//thread.Join();

			upload.SaveFiles();
			return upload.Results;
		}
	}

}
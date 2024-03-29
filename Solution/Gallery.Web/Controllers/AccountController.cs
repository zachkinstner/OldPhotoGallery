﻿using System.Web.Mvc;
using Fabric.AppBase.CSharp.Standard.Infrastructure.Logging;
using Fabric.AppBase.CSharp.Standard.Web.Application;
using Gallery.Web.Logic;
using Gallery.Web.Models.Account;

namespace Gallery.Web.Controllers {
	
	/*================================================================================================*/
	public class AccountController : GalleryMasterController {

		private readonly AccountLogic vAcct;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public AccountController(IWebSessionBaseProvider pSessProv, AccountLogic pAcct)
																					: base(pSessProv) {
			vAcct = pAcct;
			PrepareLogic(vAcct);
		}

		/*--------------------------------------------------------------------------------------------*/
		public bool NotAuth() {
			return !GallerySession.Fabric.PersonSession.IsAuthenticated;
		}
		/*--------------------------------------------------------------------------------------------*/
		public ActionResult NotAuthRedir() {
			return RedirectToAction("Index", "Home");
		}



		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ActionResult Index() {
			if ( NotAuth() ) { return NotAuthRedir(); }

			var m = NewModel<AccountIndexModel>();
			return View(m);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ActionResult AddAlbum() {
			if ( NotAuth() ) { return NotAuthRedir(); }

			var m = NewModel<AccountAddAlbumModel>();
			return View(m);
		}

		/*--------------------------------------------------------------------------------------------*/
		[HttpPost]
		public ActionResult AddAlbum(AccountAddAlbumModel pModel) {
			if ( NotAuth() ) { return NotAuthRedir(); }
			pModel.WebSession = GallerySession;
			
			if ( !ModelState.IsValid ) {
				return View(pModel);
			}

			int? albumId = vAcct.AddAlbum(pModel.Title);

			if ( albumId != null ) {
				pModel.SuccessAlbumId = (int)albumId;
				return View("AddAlbum_Success", pModel);
			}

			ModelState.AddModelError("", "A new album was not created.");
			return View(pModel);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ActionResult Upload() {
			if ( NotAuth() ) { return NotAuthRedir(); }

			var m = NewModel<AccountUploadModel>();
			m.AlbumList = vAcct.GetAlbumCores();
			return View(m);
		}

		/*--------------------------------------------------------------------------------------------*/
		[HttpPost]
		public ActionResult Upload(AccountUploadModel pModel) {
			if ( NotAuth() ) { return NotAuthRedir(); }
			pModel.WebSession = GallerySession;

			if ( !ModelState.IsValid || pModel.SelectedAlbumId == null ) {
				pModel.AlbumList = vAcct.GetAlbumCores();
				return View(pModel);
			}

			string albumDir = Server.MapPath("~/uploads/albums")+"/"+pModel.SelectedAlbumId+"/";
			Log.Debug("UPLOAD: "+pModel.SelectedAlbumId+" / "+pModel.Files);
			Log.Debug("UPLOAD TO: "+albumDir);
			pModel.UploadResults = 
				vAcct.SaveFiles((int)pModel.SelectedAlbumId, albumDir, pModel.Files.GetEnumerator());
			return View("Upload_Status", pModel);
		}

	}

}
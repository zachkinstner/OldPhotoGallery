using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Fabric.AppBase.CSharp.Standard.Infrastructure.Logging;
using Fabric.AppBase.CSharp.Standard.Web.Logic;
using Fabric.Clients.CSharp.Fluent;
using Gallery.Domain;
using Gallery.Web.Logic.Util;
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


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public IList<bool> SaveFiles(int pAlbumId, string pUploadDir,
															IEnumerator<HttpPostedFileBase> pFiles) {
			var results = new List<bool>();

			while ( pFiles.MoveNext() ) {
				bool r = SaveFile(pAlbumId, pUploadDir, pFiles.Current);
				results.Add(r);
				Log.Debug(" ... "+pFiles.Current.FileName+": "+r);
			}

			return results;
		}
		
		/*--------------------------------------------------------------------------------------------*/
		private bool SaveFile(int pAlbumId, string pUploadDir, HttpPostedFileBase pImgFile) {
			Photo photo;
			Image fullImg;
			Image medImg;
			Image smImg;

			try {
				fullImg = Image.FromStream(pImgFile.InputStream, true, true);
				medImg = ResizeImage(fullImg, new Size(1024, 1024));
				smImg = ResizeImage(fullImg, new Size(100, 100));
			}
			catch ( Exception ex ) {
				//Response.Write("EXCEPTION: "+ex+";");
				//Response.Write("ERROR|Could not convert and resize images.");
				return false;
			}

			////

			AddTxFunc(delegate(ISession pSession) {
				try {
					photo = new Photo();
					photo.Album = pSession.Load<Album>(pAlbumId);
					photo.ImgName = pImgFile.FileName;
					photo.Favorite = 0;
					photo.ExifDTOrig = new DateTime(1800, 1, 1);
					pSession.Save(photo);

					ProcessPhotoMetadata(pSession, photo, fullImg);
				}
				catch ( Exception ex ) {
					//Response.Write("EXCEPTION: "+ex+";");
					//Response.Write("ERROR|Could not save Photo and metadata.");
					throw;
				}

				string albumDir = pUploadDir;
				if ( !Directory.Exists(albumDir) ) { Directory.CreateDirectory(albumDir); }

				////

				try {
					string url = albumDir+photo.Id;
					fullImg.Save(url+"full.jpg");
					SaveJpeg(url+"med.jpg", medImg, 90);
					SaveJpeg(url+"sm.jpg", smImg, 75);
				}
				catch ( Exception ex ) {
					//Response.Write("EXCEPTION: "+ex+";");
					//Response.Write("ERROR|Cound not save images.");
					throw;
				}
			});

			ExecuteTxFuncs();
			return true;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		//www.switchonthecode.com/tutorials/csharp-tutorial-image-editing-saving-cropping-and-resizing
		/*--------------------------------------------------------------------------------------------*/
		private static Image ResizeImage(Image pSrcImg, Size pSize) {
			int srcW = pSrcImg.Width;
			int srcH = pSrcImg.Height;
			float scaleW = (float)pSize.Width/(float)srcW;
			float scaleH = (float)pSize.Height/(float)srcH;
			float scale = Math.Min(scaleH, scaleW);
			int destW = (int)Math.Floor(srcW*scale);
			int destH = (int)Math.Floor(srcH*scale);

			Bitmap b = new Bitmap(destW, destH);
			Graphics g = Graphics.FromImage((Image)b);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.DrawImage(pSrcImg, 0, 0, destW, destH);
			g.Dispose();
			return (Image)b;
		}

		/*--------------------------------------------------------------------------------------------*/
		private static void SaveJpeg(string pPath, Image pImage, int pQuality) {
			EncoderParameter qualParam = new EncoderParameter(Encoder.Quality, pQuality);
			ImageCodecInfo jpeg = GetEncoderInfo("image/jpeg");
			if ( jpeg == null ) { return; }

			EncoderParameters encParams = new EncoderParameters(1);
			encParams.Param[0] = qualParam;
			pImage.Save(pPath, jpeg, encParams);
		}

		/*--------------------------------------------------------------------------------------------*/
		private static ImageCodecInfo GetEncoderInfo(string pMimeType) {
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

			for ( int i = 0 ; i < codecs.Length ; ++i ) {
				if ( codecs[i].MimeType == pMimeType ) { return codecs[i]; }
			}

			return null;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private void ProcessPhotoMetadata(ISession pSession, Photo pPhoto, Image pImage) {
			Dictionary<PropertyTagId,
				KeyValuePair<PropertyTagType, Object>> imgMeta = GalleryUtil.BuildPropMap(pImage);

			foreach ( KeyValuePair<PropertyTagId,
					KeyValuePair<PropertyTagType, Object>> prop in imgMeta ) {
				var meta = new PhotoMeta();
				meta.Photo = pPhoto;
				meta.Label = prop.Key.ToString();
				if ( meta.Label.Substring(0, 5) == "Thumb" ) { continue; }

				meta.Type = prop.Value.Key.ToString();
				if ( meta.Type == "Byte" ) { continue; }
				if ( meta.Type == "Undefined" ) { continue; }

				meta.Value = prop.Value.Value.ToString();
				pSession.Save(meta);
			}

			////

			pPhoto.ExifDTOrig = GalleryUtil.ParseMetaDate(
				(string)imgMeta[PropertyTagId.ExifDTOrig].Value);
			pPhoto.ExifISOSpeed = Convert.ToDouble(imgMeta[PropertyTagId.ExifISOSpeed].Value);
			pPhoto.ExifExposureTime = Convert.ToDouble(imgMeta[PropertyTagId.ExifExposureTime].Value);
			pPhoto.ExifFNumber = Convert.ToDouble(imgMeta[PropertyTagId.ExifFNumber].Value);
			pPhoto.ExifFocalLength = Convert.ToDouble(imgMeta[PropertyTagId.ExifFocalLength].Value);

			pSession.SaveOrUpdate(pPhoto);
			//pSession.Flush();

			/*Log.Info("META: "+pPhoto.ExifFNumber+" / "+pPhoto.ExifFocalLength+
				" / "+pPhoto.ExifExposureTime+" / "+pPhoto.ExifISOSpeed+" / "+pPhoto.ExifDTOrig);*/
		}

	}

}
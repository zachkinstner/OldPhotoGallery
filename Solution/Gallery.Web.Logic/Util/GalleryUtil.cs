﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Gallery.Web.Logic.Util {
	
	/*================================================================================================*/
	public class GalleryUtil {

		public enum PhotoSize {
			Full = 1,
			Med,
			Sm
		}
		
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public static string BuildImageUrl(int pAlbumId, int pPhotoId, PhotoSize pSize) {
			return "http://www.zachkinstner.com/gallery"+BuildPhotoPath(pAlbumId, pPhotoId, pSize);
		}

		/*--------------------------------------------------------------------------------------------*/
		public static string BuildPhotoPath(int pAlbumId, int pPhotoId, PhotoSize pSize) {
			return "/uploads/albums/"+pAlbumId+"/"+pPhotoId+pSize+".jpg";
		}
		
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		//adapted from: www.switchonthecode.com/tutorials/getting-image-metadata-with-csharp
		public static Dictionary<PropertyTagId, KeyValuePair<PropertyTagType, Object>>
																	BuildPropMap(Image pMetaImage) {
			Dictionary<PropertyTagId, KeyValuePair<PropertyTagType, Object>> propMap =
				new Dictionary<PropertyTagId, KeyValuePair<PropertyTagType, object>>();

			foreach ( PropertyItem p in pMetaImage.PropertyItems ) {
				Object propValue = new Object();

				switch ( (PropertyTagType)p.Type ) {
					case PropertyTagType.ASCII:
						ASCIIEncoding encoding = new ASCIIEncoding();
						propValue = encoding.GetString(p.Value, 0, p.Len-1);
						break;

					case PropertyTagType.Int16:
						propValue = BitConverter.ToInt16(p.Value, 0);
						break;

					case PropertyTagType.SLONG:
					case PropertyTagType.Int32:
						propValue = BitConverter.ToInt32(p.Value, 0);
						break;

					case PropertyTagType.SRational:
					case PropertyTagType.Rational:
						UInt32 numer = BitConverter.ToUInt32(p.Value, 0);
						UInt32 denom = BitConverter.ToUInt32(p.Value, 4);
						if ( denom != 0 ) { propValue = ((double)numer/(double)denom).ToString(); }
						else { propValue = "0"; }
						if ( propValue.ToString() == "NaN" ) { propValue = "0"; }
						break;

					case PropertyTagType.Undefined:
						propValue = "Undefined Data";
						break;
				}

				propMap.Add(NumToEnum<PropertyTagId>(p.Id),
					new KeyValuePair<PropertyTagType, object>(
						NumToEnum<PropertyTagType>(p.Type), propValue));
			}

			return propMap;
		}
		
		/*--------------------------------------------------------------------------------------------*/
		public static T NumToEnum<T>(int pNumber) {
			return (T)Enum.ToObject(typeof(T), pNumber);
		}
				
		/*--------------------------------------------------------------------------------------------*/
		public static DateTime ParseMetaDate(string pString) {
			pString = pString.Substring(0,4)+"-"+pString.Substring(5,2)+"-"+pString.Substring(8);
			return DateTime.Parse(pString);
		}

	}

}
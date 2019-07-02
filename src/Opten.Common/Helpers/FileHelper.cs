using System;
using System.Globalization;
using System.IO;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace Opten.Common.Helpers
{
	/// <summary>
	/// The File Helper.
	/// </summary>
	public static class FileHelper
	{

		/// <summary>
		/// Gets the file name with date.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="extension">The extension.</param>
		/// <param name="date">The date.</param>
		/// <param name="format">The date format.</param>
		/// <returns></returns>
		public static string GetFileNameWithDate(string fileName, string extension, DateTime date, string format = "yyyyMMdd_HHmmss")
		{
			fileName = GetFileNameWithoutExtension(fileName: fileName); //TODO: Problem is when file like C.FREI AG the 'FREI AG' is meant as an extension :-/
			fileName = RemoveInvalidCharactersFromFileName(fileName: fileName);
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}.{2}", fileName, date.ToString(format), FixExtension(extension, false));
		}

		/// <summary>
		/// Gets the file name with date.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="date">The date.</param>
		/// <param name="format">The date format.</param>
		/// <returns></returns>
		public static string GetFileNameWithDate(string fileName, DateTime date, string format = "yyyyMMdd_HHmmss")
		{
			string extension = GetExtensionByFileName(fileName: fileName);
			return GetFileNameWithDate(fileName: fileName, extension: extension, date: date, format: format);
		}

		/// <summary>
		/// Gets file name with extension.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="extension">The extension.</param>
		/// <returns></returns>
		public static string GetFileNameWithExtension(string fileName, string extension)
		{
			return GetFileNameWithoutExtension(fileName) + FixExtension(extension);
		}

		/// <summary>
		/// Gets the file name without extension.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public static string GetFileNameWithoutExtension(string fileName)
		{
			// No need for null check trim, should break!
			fileName = fileName.Trim();

			if (fileName.Contains(".") == false) return fileName;
			return fileName.Substring(0, fileName.LastIndexOf("."));
		}

		/// <summary>
		/// Removes the path from file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public static string RemovePathFromFileName(string fileName)
		{
			//TODO: Add testings
			fileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
			fileName = fileName.Trim(new[] { '\"' });
			return fileName.Trim();
		}

		/// <summary>
		/// Removes invalid characters from file name (/\?: etc.).
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public static string RemoveInvalidCharactersFromFileName(string fileName)
		{
			// No need for null check trim, should break!
			fileName = fileName.Trim();

			foreach (char c in Path.GetInvalidFileNameChars())
			{
				fileName = fileName.Replace(c, ' ');
			}

			// Replace multiple whitespaces with one whitespace
			fileName = Regex.Replace(fileName, @"\s+", " ");

			//TODO: Should we remove more/less?
			fileName = fileName.ToLowerInvariant();
			fileName = fileName.Replace(" ", "_");
			fileName = fileName.Replace("ä", "ae");
			fileName = fileName.Replace("ö", "oe");
			fileName = fileName.Replace("ü", "ue");

            // there are invalid for zipping
            fileName = fileName.Replace("’", string.Empty);
            fileName = fileName.Replace("à", "a");
            fileName = fileName.Replace("â", "a");
            fileName = fileName.Replace("é", "e");
            fileName = fileName.Replace("ê", "e");
            fileName = fileName.Replace("è", "e");
            fileName = fileName.Replace("î", "i");

            if (fileName.EndsWith("_"))
            {
                fileName = fileName.Substring(0, fileName.Length - 1);
            }

			return fileName;
		}

		/// <summary>
		/// Removes special characters from file name.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <returns></returns>
		public static string RemoveSpecialCharactersFromFileName(string fileName)
		{
			// No need for null check trim, should break!
			fileName = fileName.Trim();

			fileName = fileName.ToLowerInvariant();

			fileName = Regex.Replace(fileName, "[^0-9a-zA-Z-_]+", string.Empty);

			return RemoveInvalidCharactersFromFileName(fileName);
		}

		/// <summary>
		/// Fixes the extension.
		/// </summary>
		/// <param name="extension">The extension.</param>
		/// <param name="withPoint">if set to <c>true</c> [with point].</param>
		/// <returns></returns>
		public static string FixExtension(string extension, bool withPoint = true)
		{
			// No need for null check trim, should break!
			extension = extension.Trim().ToLowerInvariant();
			if (withPoint == false) extension.Replace(".", string.Empty);
			return extension.StartsWith(".") || withPoint == false ? extension : "." + extension;
		}

		/// <summary>
		/// Gets the extension of the file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <returns></returns>
		public static string GetExtensionByFileName(string fileName)
		{
			if (fileName.Contains(".") == false)
			{
				throw new NotSupportedException("Filename doesn't contains extension.");
			}

			return FixExtension(fileName.Substring(fileName.LastIndexOf('.') + 1), false);
		}

		/// <summary>
		/// Gets the content type of the file/extension.
		/// </summary>
		/// <param name="fileNameOrExtension">File or extension.</param>
		/// <returns></returns>
		public static string GetContentType(string fileNameOrExtension)
		{
			if (string.IsNullOrWhiteSpace(fileNameOrExtension))
			{
				throw new ArgumentNullException("fileNameOrExtension", "Please provide a filename or extension.");
			}

			string extension = fileNameOrExtension;

			if (extension.Contains("."))
			{
				extension = GetExtensionByFileName(fileName: fileNameOrExtension);
			}

			extension = FixExtension(extension, false).ToLowerInvariant();

			if (extension == "ai") { return "application/postscript"; }
			else if (extension == "aif") { return "audio/x-aiff"; }
			else if (extension == "aifc") { return "audio/x-aiff"; }
			else if (extension == "aiff") { return "audio/x-aiff"; }
			else if (extension == "asc") { return MediaTypeNames.Text.Plain; }
			else if (extension == "atom") { return "application/atom+xml"; }
			else if (extension == "au") { return "audio/basic"; }
			else if (extension == "avi") { return "video/x-msvideo"; }
			else if (extension == "bcpio") { return "application/x-bcpio"; }
			else if (extension == "bin") { return MediaTypeNames.Application.Octet; }
			else if (extension == "bmp") { return "image/bmp"; }
			else if (extension == "cdf") { return "application/x-netcdf"; }
			else if (extension == "cgm") { return "image/cgm"; }
			else if (extension == "class") { return MediaTypeNames.Application.Octet; }
			else if (extension == "cpio") { return "application/x-cpio"; }
			else if (extension == "cpt") { return "application/mac-compactpro"; }
			else if (extension == "csh") { return "application/x-csh"; }
			else if (extension == "css") { return "text/css"; }
			else if (extension == "dcr") { return "application/x-director"; }
			else if (extension == "dif") { return "video/x-dv"; }
			else if (extension == "dir") { return "application/x-director"; }
			else if (extension == "djv") { return "image/vnd.djvu"; }
			else if (extension == "djvu") { return "image/vnd.djvu"; }
			else if (extension == "dll") { return MediaTypeNames.Application.Octet; }
			else if (extension == "dmg") { return MediaTypeNames.Application.Octet; }
			else if (extension == "dms") { return MediaTypeNames.Application.Octet; }
			else if (extension == "doc") { return "application/msword"; }
			else if (extension == "docx") { return "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; }
			else if (extension == "dotx") { return "application/vnd.openxmlformats-officedocument.wordprocessingml.template"; }
			else if (extension == "docm") { return "application/vnd.ms-word.document.macroEnabled.12"; }
			else if (extension == "dotm") { return "application/vnd.ms-word.template.macroEnabled.12"; }
			else if (extension == "dtd") { return "application/xml-dtd"; }
			else if (extension == "dv") { return "video/x-dv"; }
			else if (extension == "dvi") { return "application/x-dvi"; }
			else if (extension == "dxr") { return "application/x-director"; }
			else if (extension == "eps") { return "application/postscript"; }
			else if (extension == "etx") { return "text/x-setext"; }
			else if (extension == "exe") { return MediaTypeNames.Application.Octet; }
			else if (extension == "ez") { return "application/andrew-inset"; }
			else if (extension == "gif") { return MediaTypeNames.Image.Gif; }
			else if (extension == "gram") { return "application/srgs"; }
			else if (extension == "grxml") { return "application/srgs+xml"; }
			else if (extension == "gtar") { return "application/x-gtar"; }
			else if (extension == "hdf") { return "application/x-hdf"; }
			else if (extension == "hqx") { return "application/mac-binhex40"; }
			else if (extension == "htm") { return MediaTypeNames.Text.Html; }
			else if (extension == "html") { return MediaTypeNames.Text.Html; }
			else if (extension == "ice") { return "x-conference/x-cooltalk"; }
			else if (extension == "ico") { return "image/x-icon"; }
			else if (extension == "ics") { return "text/calendar"; }
			else if (extension == "ief") { return "image/ief"; }
			else if (extension == "ifb") { return "text/calendar"; }
			else if (extension == "iges") { return "model/iges"; }
			else if (extension == "igs") { return "model/iges"; }
			else if (extension == "jnlp") { return "application/x-java-jnlp-file"; }
			else if (extension == "jp2") { return "image/jp2"; }
			else if (extension == "jpe") { return MediaTypeNames.Image.Jpeg; }
			else if (extension == "jpeg") { return MediaTypeNames.Image.Jpeg; }
			else if (extension == "jpg") { return MediaTypeNames.Image.Jpeg; }
			else if (extension == "js") { return "application/x-javascript"; }
			else if (extension == "kar") { return "audio/midi"; }
			else if (extension == "latex") { return "application/x-latex"; }
			else if (extension == "lha") { return MediaTypeNames.Application.Octet; }
			else if (extension == "lzh") { return MediaTypeNames.Application.Octet; }
			else if (extension == "m3u") { return "audio/x-mpegurl"; }
			else if (extension == "m4a") { return "audio/mp4a-latm"; }
			else if (extension == "m4b") { return "audio/mp4a-latm"; }
			else if (extension == "m4p") { return "audio/mp4a-latm"; }
			else if (extension == "m4u") { return "video/vnd.mpegurl"; }
			else if (extension == "m4v") { return "video/x-m4v"; }
			else if (extension == "mac") { return "image/x-macpaint"; }
			else if (extension == "man") { return "application/x-troff-man"; }
			else if (extension == "mathml") { return "application/mathml+xml"; }
			else if (extension == "me") { return "application/x-troff-me"; }
			else if (extension == "mesh") { return "model/mesh"; }
			else if (extension == "mid") { return "audio/midi"; }
			else if (extension == "midi") { return "audio/midi"; }
			else if (extension == "mif") { return "application/vnd.mif"; }
			else if (extension == "mov") { return "video/quicktime"; }
			else if (extension == "movie") { return "video/x-sgi-movie"; }
			else if (extension == "mp2") { return "audio/mpeg"; }
			else if (extension == "mp3") { return "audio/mpeg"; }
			else if (extension == "mp4") { return "video/mp4"; }
			else if (extension == "mpe") { return "video/mpeg"; }
			else if (extension == "mpeg") { return "video/mpeg"; }
			else if (extension == "mpg") { return "video/mpeg"; }
			else if (extension == "mpga") { return "audio/mpeg"; }
			else if (extension == "ms") { return "application/x-troff-ms"; }
			else if (extension == "msh") { return "model/mesh"; }
			else if (extension == "mxu") { return "video/vnd.mpegurl"; }
			else if (extension == "nc") { return "application/x-netcdf"; }
			else if (extension == "oda") { return "application/oda"; }
			else if (extension == "ogg") { return "application/ogg"; }
			else if (extension == "pbm") { return "image/x-portable-bitmap"; }
			else if (extension == "pct") { return "image/pict"; }
			else if (extension == "pdb") { return "chemical/x-pdb"; }
			else if (extension == "pdf") { return MediaTypeNames.Application.Pdf; }
			else if (extension == "pgm") { return "image/x-portable-graymap"; }
			else if (extension == "pgn") { return "application/x-chess-pgn"; }
			else if (extension == "pic") { return "image/pict"; }
			else if (extension == "pict") { return "image/pict"; }
			else if (extension == "png") { return "image/png"; }
			else if (extension == "pnm") { return "image/x-portable-anymap"; }
			else if (extension == "pnt") { return "image/x-macpaint"; }
			else if (extension == "pntg") { return "image/x-macpaint"; }
			else if (extension == "ppm") { return "image/x-portable-pixmap"; }
			else if (extension == "ppt") { return "application/vnd.ms-powerpoint"; }
			else if (extension == "pptx") { return "application/vnd.openxmlformats-officedocument.presentationml.presentation"; }
			else if (extension == "potx") { return "application/vnd.openxmlformats-officedocument.presentationml.template"; }
			else if (extension == "ppsx") { return "application/vnd.openxmlformats-officedocument.presentationml.slideshow"; }
			else if (extension == "ppam") { return "application/vnd.ms-powerpoint.addin.macroEnabled.12"; }
			else if (extension == "pptm") { return "application/vnd.ms-powerpoint.presentation.macroEnabled.12"; }
			else if (extension == "potm") { return "application/vnd.ms-powerpoint.template.macroEnabled.12"; }
			else if (extension == "ppsm") { return "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"; }
			else if (extension == "ps") { return "application/postscript"; }
			else if (extension == "qt") { return "video/quicktime"; }
			else if (extension == "qti") { return "image/x-quicktime"; }
			else if (extension == "qtif") { return "image/x-quicktime"; }
			else if (extension == "ra") { return "audio/x-pn-realaudio"; }
			else if (extension == "ram") { return "audio/x-pn-realaudio"; }
			else if (extension == "ras") { return "image/x-cmu-raster"; }
			else if (extension == "rdf") { return "application/rdf+xml"; }
			else if (extension == "rgb") { return "image/x-rgb"; }
			else if (extension == "rm") { return "application/vnd.rn-realmedia"; }
			else if (extension == "roff") { return "application/x-troff"; }
			else if (extension == "rtf") { return "text/rtf"; }
			else if (extension == "rtx") { return MediaTypeNames.Text.RichText; }
			else if (extension == "sgm") { return "text/sgml"; }
			else if (extension == "sgml") { return "text/sgml"; }
			else if (extension == "sh") { return "application/x-sh"; }
			else if (extension == "shar") { return "application/x-shar"; }
			else if (extension == "silo") { return "model/mesh"; }
			else if (extension == "sit") { return "application/x-stuffit"; }
			else if (extension == "skd") { return "application/x-koan"; }
			else if (extension == "skm") { return "application/x-koan"; }
			else if (extension == "skp") { return "application/x-koan"; }
			else if (extension == "skt") { return "application/x-koan"; }
			else if (extension == "smi") { return "application/smil"; }
			else if (extension == "smil") { return "application/smil"; }
			else if (extension == "snd") { return "audio/basic"; }
			else if (extension == "so") { return MediaTypeNames.Application.Octet; }
			else if (extension == "spl") { return "application/x-futuresplash"; }
			else if (extension == "src") { return "application/x-wais-source"; }
			else if (extension == "sv4cpio") { return "application/x-sv4cpio"; }
			else if (extension == "sv4crc") { return "application/x-sv4crc"; }
			else if (extension == "svg") { return "image/svg+xml"; }
			else if (extension == "swf") { return "application/x-shockwave-flash"; }
			else if (extension == "t") { return "application/x-troff"; }
			else if (extension == "tar") { return "application/x-tar"; }
			else if (extension == "tcl") { return "application/x-tcl"; }
			else if (extension == "tex") { return "application/x-tex"; }
			else if (extension == "texi") { return "application/x-texinfo"; }
			else if (extension == "texinfo") { return "application/x-texinfo"; }
			else if (extension == "tif") { return MediaTypeNames.Image.Tiff; }
			else if (extension == "tiff") { return MediaTypeNames.Image.Tiff; }
			else if (extension == "tr") { return "application/x-troff"; }
			else if (extension == "tsv") { return "text/tab-separated-values"; }
			else if (extension == "txt") { return MediaTypeNames.Text.Plain; }
			else if (extension == "ustar") { return "application/x-ustar"; }
			else if (extension == "vcd") { return "application/x-cdlink"; }
			else if (extension == "vrml") { return "model/vrml"; }
			else if (extension == "vxml") { return "application/voicexml+xml"; }
			else if (extension == "wav") { return "audio/x-wav"; }
			else if (extension == "wbmp") { return "image/vnd.wap.wbmp"; }
			else if (extension == "wbmxl") { return "application/vnd.wap.wbxml"; }
			else if (extension == "wml") { return "text/vnd.wap.wml"; }
			else if (extension == "wmlc") { return "application/vnd.wap.wmlc"; }
			else if (extension == "wmls") { return "text/vnd.wap.wmlscript"; }
			else if (extension == "wmlsc") { return "application/vnd.wap.wmlscriptc"; }
			else if (extension == "wrl") { return "model/vrml"; }
			else if (extension == "xbm") { return "image/x-xbitmap"; }
			else if (extension == "xht") { return "application/xhtml+xml"; }
			else if (extension == "xhtml") { return "application/xhtml+xml"; }
			else if (extension == "xls") { return "application/vnd.ms-excel"; }
			else if (extension == "xml") { return "application/xml"; }
			else if (extension == "xpm") { return "image/x-xpixmap"; }
			else if (extension == "xsl") { return "application/xml"; }
			else if (extension == "xlsx") { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
			else if (extension == "xltx") { return "application/vnd.openxmlformats-officedocument.spreadsheetml.template"; }
			else if (extension == "xlsm") { return "application/vnd.ms-excel.sheet.macroEnabled.12"; }
			else if (extension == "xltm") { return "application/vnd.ms-excel.template.macroEnabled.12"; }
			else if (extension == "xlam") { return "application/vnd.ms-excel.addin.macroEnabled.12"; }
			else if (extension == "xlsb") { return "application/vnd.ms-excel.sheet.binary.macroEnabled.12"; }
			else if (extension == "xslt") { return "application/xslt+xml"; }
			else if (extension == "xul") { return "application/vnd.mozilla.xul+xml"; }
			else if (extension == "xwd") { return "image/x-xwindowdump"; }
			else if (extension == "xyz") { return "chemical/x-xyz"; }
			else if (extension == "zip") { return MediaTypeNames.Application.Zip; }

			return MediaTypeNames.Application.Octet;
		}

	}
}
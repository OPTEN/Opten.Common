using Opten.Common.Helpers;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Image Extensions.
	/// </summary>
	public static class ImageExtensions
	{

		/// <summary>
		/// Gets the font awesome icon by extension.
		/// </summary>
		/// <param name="extension">The extension.</param>
		/// <returns></returns>
		public static string GetFontAwesomeIconByExtension(this string extension)
		{
			if (string.IsNullOrWhiteSpace(extension)) return string.Empty;

			extension = extension.Trim();

			if (extension.Contains(".") && extension.StartsWith(".") == false)
			{
				extension = FileHelper.GetExtensionByFileName(extension);
			}

			extension = extension.Replace(".", string.Empty);

			switch (extension.ToLowerInvariant())
			{
				case "xls":
				case "xll":
				case "xlw":
				case "xlt":
				case "xlm":
				case "xlam":
				case "xla":
				case "xlsx":
				case "xlsm":
				case "xlsb":
				case "xltx":
				case "xltm":
				case "csv":
					return "fa-file-excel-o";
				case "doc":
				case "dot":
				case "docx":
				case "docm":
				case "dotx":
				case "dotm":
				case "docb":
					return "fa-file-word-o";
				case "ppt":
				case "pot":
				case "pps":
				case "pptx":
				case "pptm":
				case "potx":
				case "potm":
				case "ppam":
				case "ppsx":
				case "ppsm":
				case "sldx":
				case "sldm":
					return "fa-file-powerpoint-o";
				case "log":
				case "msg":
				case "pages":
				case "rtf":
				case "txt":
				case "wpd":
				case "wps":
					return "fa-file-text-o";
				case "pdf":
					return "fa-file-pdf-o";
				case "jpg":
				case "jp2":
				case "jpe":
				case "jpeg":
				case "tif":
				case "gif":
				case "png":
				case "bmp":
					return "fa-file-image-o";
				case "aif":
				case "iif":
				case "aifc":
				case "au":
				case "kar":
				case "m3u":
				case "m4a":
				case "m4p":
				case "m4b":
				case "ra":
				case "ram":
				case "snd":
				case "wav":
				case "avi":
				case "div":
				case "dv":
				case "m4u":
				case "m4v":
				case "mov":
				case "movie":
				case "mp2":
				case "mp4":
				case "mpe":
				case "mpeg":
				case "mpg":
				case "mpga":
				case "qt":
				case "mid":
				case "mp3":
				case "wma":
				case "3g2":
				case "3gp":
				case "asf":
				case "asx":
				case "flv":
				case "swf":
				case "vob":
					return "fa-file-video-o";
				case "zip":
				case "rar":
				case "7z":
				case "tar":
				case "gz":
				case "deb":
				case "pkg":
				case "rpm":
				case "sit":
				case "sitx":
				case "tar.gz":
				case "zipx":
					return "fa-file-archive-o";
				default:
					return "fa-file-o";
			}
		}

	}
}
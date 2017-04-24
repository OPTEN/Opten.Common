using System.Globalization;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Culture Extensions.
	/// </summary>
	public static class CultureExtensions
	{

		/// <summary>
		/// Gets the ISO code of the culture.
		/// </summary>
		/// <param name="cultureInfo">The culture information.</param>
		/// <returns></returns>
		public static string GetISOCode(this CultureInfo cultureInfo)
		{
			return cultureInfo.Name;
		}

		/*public static string GetTwoLetterIsoCodeByName(this string name)
		{
			if (string.IsNullOrWhiteSpace(name)) return string.Empty;
			if (name.Contains("-") == false) return name;
			return name.Split('-')[0];
		}*/

		/*public static string GetLanguage(this HttpRequestBase request, CultureHelper cultureHelper)
		{
			return cultureHelper.GetCulture(request).TwoLetterISOLanguageName;
		}

		public static string GetLanguage(this HttpRequestMessage request, CultureHelper cultureHelper)
		{
			//TODO: This has to be better..
			var newRequest = new HttpRequestWrapper(HttpContext.Current.Request);
			return newRequest.GetLanguage(cultureHelper);
		}*/

	}
}
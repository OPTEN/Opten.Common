using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace Opten.Common.Helpers
{
	public class CultureHelper
	{
		
		public CultureInfo[] Cultures;
		public CultureInfo DefaultCulture;

		public CultureHelper(CultureInfo[] cultures, CultureInfo defaultCulture = null)
		{
			this.Cultures = cultures;
			this.DefaultCulture = defaultCulture ?? cultures.First();
		}

		public bool IsValidLanguage(string language)
		{
			foreach (CultureInfo ci in Cultures)
				if (language.Equals(ci.TwoLetterISOLanguageName, StringComparison.InvariantCultureIgnoreCase))
					return true;

			return false;
		}

		public CultureInfo GetCulture(HttpRequestBase request)
		{
			CultureInfo defaultCulture = DefaultCulture;

			CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;

			if (currentCulture == null)
				currentCulture = GetBrowserCulture(request);

			return IsValidLanguage(currentCulture.Name.Split('-')[0]) ? currentCulture : defaultCulture;
		}

		public CultureInfo GetBrowserCulture(HttpRequestBase request)
		{
			CultureInfo defaultCulture = DefaultCulture;

			try
			{
				string browserCulture = (request.UserLanguages ?? Enumerable.Empty<string>()).FirstOrDefault();

				if (string.IsNullOrWhiteSpace(browserCulture))
					browserCulture = Thread.CurrentThread.CurrentUICulture.Name;

				return IsValidLanguage(browserCulture.Split('-')[0])
					? Cultures.First(o => o.Name.StartsWith(browserCulture.Split('-')[0]))
					: defaultCulture;
			}
			catch (Exception ex)
			{
				//TODO: Log error
				//LogHelper.Error<HttpRequestBase>("Couldn't get browser language, return default language: " + defaultCulture, ex);
				return defaultCulture;
			}
		}

	}
}
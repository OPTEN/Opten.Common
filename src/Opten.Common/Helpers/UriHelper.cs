using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Opten.Common.Extensions;

namespace Opten.Common.Helpers
{
	public class UriHelper
	{

		private Uri _uri;
		private CultureHelper _cultureHelper;

		public UriHelper(Uri uri, CultureHelper cultureHelper)
		{
			_uri = uri;
			_cultureHelper = cultureHelper;
		}

		public string GetWithoutDomainWithLanguage(string language, bool withQuery = true)
		{
			if ((_uri.AbsolutePath + "/").Contains("/" + language + "/"))
				return _uri.GetUrl(withQuery);

			string url = GetWithoutDomainWithoutLanguage(withQuery).RemoveLeadingAndTralingSlash();

			return string.Format(CultureInfo.InvariantCulture, "/{0}/{1}", language, url);
		}

		public bool ContainsLanguage()
		{
			var toCompare = _cultureHelper.Cultures.Select(o => o.TwoLetterISOLanguageName);

			return ContainsSegment(toCompare);
		}

		public string GetWithoutDomainWithoutLanguage(bool withQuery = true)
		{
			string url = _uri.GetUrl(withQuery);
			if (ContainsLanguage() == false) return url;

			var segments = new List<string>();

			bool found = false;
			string compare;
			foreach (var segment in _uri.Segments)
			{
				found = false;
				compare = segment.DecodeSegment();

				if (string.IsNullOrWhiteSpace(compare)) continue;

				foreach (CultureInfo ci in _cultureHelper.Cultures)
					if (compare.Equals(ci.TwoLetterISOLanguageName, StringComparison.InvariantCultureIgnoreCase))
					{
						found = true;
						break;
					}

				if (found == false) segments.Add(compare);
			}

			string query = url.Contains("?") ? "?" + url.Split('?')[1] : string.Empty;
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", string.Join("/", segments), query);
		}

		public string GetLanguage()
		{
			string url = _uri.GetUrl();
			if (ContainsLanguage())
			{
				string compare;
				foreach (var segment in _uri.Segments)
				{
					compare = segment.DecodeSegment();

					if (string.IsNullOrWhiteSpace(compare)) continue;

					foreach (CultureInfo ci in _cultureHelper.Cultures)
						if (compare.Equals(ci.TwoLetterISOLanguageName, StringComparison.InvariantCultureIgnoreCase))
							return ci.TwoLetterISOLanguageName.ToLower();
				}
			}

			return _cultureHelper.DefaultCulture.TwoLetterISOLanguageName.ToLower();
		}

		#region Private methods

		private bool ContainsSegment(IEnumerable<string> values)
		{
			if (_uri.Segments == null || _uri.Segments.Any() == false)
				return false;
			if (values == null || values.Any() == false)
				return false;

			string compare;
			foreach (var segment in _uri.Segments)
			{
				compare = segment.DecodeSegment();

				if (string.IsNullOrWhiteSpace(compare)) continue;

				foreach (string value in values)
					if (compare.Equals(value, StringComparison.InvariantCultureIgnoreCase))
						return true;
			}

			return false;
		}

		#endregion

	}
}
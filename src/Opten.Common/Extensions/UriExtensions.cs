using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Uri Extensions.
	/// </summary>
	public static class UriExtensions
	{

		/// <summary>
		/// Gets the base URL (http://www.opten.ch?queryParams... > http://wwww.opten.ch).
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns></returns>
		public static string GetBaseUrl(this Uri uri)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", uri.Scheme, uri.Host, uri.IsDefaultPort ? string.Empty : ":" + uri.Port);
		}

		/// <summary>
		/// Determines whether the url contains m. (e.g. m.opten.ch)
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns></returns>
		public static bool IsMobile(this Uri uri)
		{
			return uri.AbsoluteUri.IndexOf("://m.") >= 0;
		}

		/// <summary>
		/// Determines whether the url contains staging.|stg. or m.staging.|m.stg. (e.g. stg.opten.ch)
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns></returns>
		public static bool IsStaging(this Uri uri)
		{
			return uri.AbsoluteUri.IndexOf("://staging.") >= 0 || uri.AbsoluteUri.IndexOf("://stg.") >= 0 ||
				   uri.AbsoluteUri.IndexOf("://m.staging.") >= 0 || uri.AbsoluteUri.IndexOf("://m.stg.") >= 0;
		}

		/// <summary>
		/// Gets the URL (http://www.opten.ch?queryParams... > http://wwww.opten.ch?queryParams..).
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="withQuery">if set to <c>true</c> [with query].</param>
		/// <param name="withDomain">if set to <c>true</c> [with domain].</param>
		/// <returns></returns>
		public static string GetUrl(this Uri uri, bool withQuery = true, bool withDomain = true)
		{
			if (uri.IsAbsoluteUri == false)
			{
				return string.Empty;
			}

			string url = uri.AbsolutePath;

			if (withQuery && string.IsNullOrWhiteSpace(uri.PathAndQuery) == false)
				url = HttpUtility.UrlDecode(uri.PathAndQuery, Encoding.UTF8);

			return ((withDomain) ? uri.GetBaseUrl() : string.Empty) + url;
		}

		/// <summary>
		/// Gets the segments of the URL (/de/subpage/ > [de,supage]).
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <returns></returns>
		public static string[] GetSegments(this string url)
		{
			List<string> segments = new List<string>();
			string decoded;
			foreach (string segment in url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
			{
				decoded = segment.DecodeSegment();

				if (string.IsNullOrWhiteSpace(decoded)) continue;

				segments.Add(decoded.ToLowerInvariant());
			}
			return segments.ToArray();
		}

		/// <summary>
		/// Adds a query parameter to the url (if not already added).
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="param">The parameter.</param>
		/// <param name="value">The value.</param>
		/// <param name="overrideParam">if set to <c>true</c> [override parameter].</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">uri, param or value;Some required arguments are missing!</exception>
		public static Uri AddQueryParam(this Uri uri, string param, object value, bool overrideParam = false)
		{
			if (uri == null || string.IsNullOrWhiteSpace(param) || value == null)
				throw new ArgumentNullException("uri, param or value", "Some required arguments are missing!");

			if (overrideParam)
			{
				return uri.UpdateQueryParam(param: param, value: value);
			}

			param = param.Trim();

			if (uri.GetQueryCollection().ContainsKey(param)) return uri;

			string url = uri.GetUrl(withQuery: true, withDomain: true);

			string prefix = "?";
			if (url.Contains("?")) prefix = "&";

			string stringValue = value.ToString().Trim();
			if (value.GetType() == typeof(bool)) stringValue = stringValue.ToLowerInvariant();

			return new Uri(url + prefix + param + "=" + stringValue, UriKind.Absolute);
		}

		/// <summary>
		/// Removes a query parameter from the url.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="param">The parameter.</param>
		/// <returns></returns>
		public static Uri RemoveQueryParam(this Uri uri, string param)
		{
			string url = uri.GetUrl(withQuery: true, withDomain: true);

			param = param.Trim();

			NameValueCollection query = uri.GetQueryCollection();

			if (query == null || query.ContainsKey(param) == false) return uri;

			query.Remove(param);

			url = uri.GetUrl(withQuery: false, withDomain: true);

			if (query.Count == 0) return new Uri(url, UriKind.Absolute);

			return new Uri(url + "?" + HttpUtility.UrlDecode(query.ToString(), Encoding.UTF8), UriKind.Absolute);
		}

		/// <summary>
		/// Updates or adds a query parameter to/from the url.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="param">The parameter.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">uri, param or value;Some required arguments are missing!</exception>
		public static Uri UpdateQueryParam(this Uri uri, string param, object value)
		{
			if (uri == null || string.IsNullOrWhiteSpace(param) || value == null)
				throw new ArgumentNullException("uri, param or value", "Some required arguments are missing!");

			return uri.RemoveQueryParam(param).AddQueryParam(param, value);
		}

		/// <summary>
		/// Determines whether the name value collection contains the key.
		/// </summary>
		/// <param name="collection">The collection.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static bool ContainsKey(this NameValueCollection collection, string key)
		{
			if (collection == null || collection.Count == 0 || string.IsNullOrWhiteSpace(key)) return false;
			foreach (string keyToFind in collection.Keys)
			{
				if (string.IsNullOrWhiteSpace(keyToFind)) continue;

				// InvariantCultureIgnoreCase because we cannot have the same key
				// and due to MVC Models the first char could be upper case!
				// AFAIK the HttpRequestBase doesn't care about the case...
				if (keyToFind.Equals(key, StringComparison.InvariantCultureIgnoreCase)) return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the uri query collection.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns></returns>
		public static NameValueCollection GetQueryCollection(this Uri uri)
		{
			if (string.IsNullOrWhiteSpace(uri.Query)) return null;

			NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);

			return query;
		}

		/// <summary>
		/// Determines whether the uri contains the segment.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="segments">The URL segments.</param>
		/// <returns></returns>
		public static bool ContainsSegment(this Uri uri, IEnumerable<string> segments)
		{
			if (uri.Segments == null || uri.Segments.Any() == false) return false;
			if (segments == null || segments.Any() == false) return false;

			string compare;
			foreach (string segment in uri.Segments)
			{
				compare = segment.DecodeSegment();

				if (string.IsNullOrWhiteSpace(compare)) continue;

				foreach (string part in segments)
					if (compare.Equals(part, StringComparison.InvariantCultureIgnoreCase))
						return true;
			}

			return false;
		}

		/// <summary>
		/// Decodes the segment (for comparision).
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <returns></returns>
		public static string DecodeSegment(this string segment)
		{
			return HttpUtility.UrlDecode(segment).Replace("/", string.Empty);
		}

		/// <summary>
		/// This is a performance tweak to check if this is an ASP.Net server file
		/// .Net will pass these requests through to the module when in integrated mode.
		/// </summary>
		/// <param name="uri">The URL.</param>
		/// <returns></returns>
		public static bool IsClientSideAspNetRequest(this Uri uri)
		{
			// Copied from Umbraco due to internal
			string extension = Path.GetExtension(uri.LocalPath);

			if (string.IsNullOrWhiteSpace(extension)) return true;

			string[] toInclude = new[] { ".aspx", ".ashx", ".asmx", ".axd", ".svc" };

			return toInclude.Any(o => o.Equals(extension, StringComparison.OrdinalIgnoreCase));
		}

		#region Private helpers

		/*private static string ToQueryString(this Dictionary<string, string> query)
		{
			string queryString = "?";

			foreach (KeyValuePair<string, string> param in query)
			{
				if (queryString.Length > 1) queryString += "&";
				queryString += param.Key + "=" + param.Value;
			}

			return queryString;
		}*/

		#endregion

	}
}
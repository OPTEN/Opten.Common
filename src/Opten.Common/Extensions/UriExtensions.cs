using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            return uri.GetLeftPart(UriPartial.Authority);
        }

        /// <summary>
        /// Gets the URL (http://www.opten.ch?queryParams... &gt; http://wwww.opten.ch?queryParams..).
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="withQuery">if set to <c>true</c> [with query].</param>
        /// <param name="withDomain">if set to <c>true</c> [with domain].</param>
        /// <param name="withFragment">if set to <c>true</c> [with fragment].</param>
        /// <returns></returns>
        public static string GetUrl(this Uri uri, bool withQuery = true, bool withDomain = true, bool withFragment = true)
        {
            string url = uri.GetPath();
            string pathAndQuery = uri.GetPathAndQuery();

            if (withQuery && string.IsNullOrWhiteSpace(pathAndQuery) == false)
            {
                url = HttpUtility.UrlDecode(pathAndQuery, Encoding.UTF8);
            }

            if (withFragment)
            {
                url += uri.GetFragment();
            }

            return ((withDomain && uri.IsAbsoluteUri) ? uri.GetBaseUrl() : string.Empty) + url;
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
            if (overrideParam)
            {
                return uri.UpdateQueryParam(param: param, value: value);
            }

            param = param.Trim();

            if (uri.GetQueryCollection().ContainsKey(param)) return uri;

            string url = uri.GetUrl(withQuery: true, withDomain: true, withFragment: false);
            string fragment = uri.GetFragment();

            string prefix = "?";
            if (url.Contains("?")) prefix = "&";

            string stringValue = value.ToString().Trim();
            if (value.GetType() == typeof(bool)) stringValue = stringValue.ToLowerInvariant();

            return new Uri(url + prefix + param + "=" + stringValue + fragment, uri.IsAbsoluteUri
                ? UriKind.Absolute
                : UriKind.Relative);
        }

        /// <summary>
        /// Removes a query parameter from the url.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public static Uri RemoveQueryParam(this Uri uri, string param)
        {
            param = param.Trim();

            NameValueCollection query = uri.GetQueryCollection();

            if (query == null || query.ContainsKey(param) == false) return uri;

            query.Remove(param);

            string url = uri.GetUrl(withQuery: false, withDomain: true, withFragment: false);
            string fragment = uri.GetFragment();

            if (query.Count == 0)
            {
                return new Uri(url + fragment, uri.IsAbsoluteUri
                    ? UriKind.Absolute
                    : UriKind.Relative);
            }

            return new Uri(url + "?" + HttpUtility.UrlDecode(query.ToString(), Encoding.UTF8) + fragment, uri.IsAbsoluteUri
                ? UriKind.Absolute
                : UriKind.Relative);
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
            string query = uri.IsAbsoluteUri
                ? uri.Query
                : uri.OriginalString.Contains('?')
                    ? uri.OriginalString.Split('?')[1]
                    : null;

            query = query.ForceStartsWith('?');

            if (string.IsNullOrWhiteSpace(query)) return null;

            return HttpUtility.ParseQueryString(query);
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

        private static string GetPath(this Uri uri)
        {
            if (uri.IsAbsoluteUri)
            {
                return uri.AbsolutePath;
            }

            string url = uri.OriginalString;

            url = url.Contains("#")
                ? url.Split('#')[0]
                : url;

            return url.Contains("?")
                ? url.Split('?')[0]
                : url;
        }

        private static string GetPathAndQuery(this Uri uri)
        {
            if (uri.IsAbsoluteUri)
            {
                return uri.PathAndQuery;
            }

            return uri.OriginalString.Contains("#")
                ? uri.OriginalString.Split('#')[0]
                : uri.OriginalString;
        }

        private static string GetFragment(this Uri uri)
        {
            if (uri.IsAbsoluteUri)
            {
                return string.IsNullOrWhiteSpace(uri.Fragment)
                    ? string.Empty
                    : uri.Fragment.ForceStartsWith('#');
            }

            return uri.OriginalString.Contains("#")
                ? uri.OriginalString.Split('#')[1].ForceStartsWith('#')
                : string.Empty;
        }

        #endregion

    }
}
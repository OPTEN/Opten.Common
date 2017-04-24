using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Request Extensions.
	/// </summary>
	public static class RequestExtensions
	{

		#region WEB request

		/// <summary>
		/// Determines whether the request contains the key.
		/// </summary>
		/// <param name="httpRequest">The HTTP request.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static bool ContainsKey(this HttpRequestBase httpRequest, string key)
		{
			if (httpRequest == null) return false;

			if (string.IsNullOrWhiteSpace(key)) return false;

			if (string.IsNullOrWhiteSpace(httpRequest[key])) return false;

			return true;
		}

		/// <summary>
		/// Gets the value of the request.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="httpRequest">The HTTP request.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static T Get<T>(this HttpRequestBase httpRequest, string key)
		{
			T defaultValue = default(T);

			return httpRequest.Get<T>(key, defaultValue);
		}

		/// <summary>
		/// Gets the value of the request.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="httpRequest">The HTTP request.</param>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static T Get<T>(this HttpRequestBase httpRequest, string key, T defaultValue)
		{
			if (httpRequest.ContainsKey(key) == false) return defaultValue;

			string value = httpRequest[key];

			if (string.IsNullOrWhiteSpace(value)) return defaultValue;

			// http://www.hanselman.com/blog/TypeConvertersTheresNotEnoughTypeDescripterGetConverterInTheWorld.aspx
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
			return (T)(converter.ConvertFromInvariantString(value));
		}

		/// <summary>
		/// Tries to get culture from accept-language.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="cultures">The cultures which gets filtered by the accept-language.</param>
		/// <returns></returns>
		public static CultureInfo TryGetCultureFromAcceptLanguage(this HttpRequestBase request, CultureInfo[] cultures)
		{
			return TryGetCultureFromAcceptLanguage(
				acceptLanguage: request.GetAcceptLanguage(),
				cultures: cultures);
		}

		/// <summary>
		/// Tries to set culture from accept-language.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="cultures">The cultures which gets filtered by the accept-language.</param>
		/// <returns></returns>
		public static bool TrySetCultureFromAcceptLanguage(this HttpRequestBase request, CultureInfo[] cultures)
		{
			CultureInfo culture = request.TryGetCultureFromAcceptLanguage(
				cultures: cultures);

			if (culture != null)
			{
				Thread.CurrentThread.CurrentCulture = culture;
				Thread.CurrentThread.CurrentUICulture = culture;

				return true;
			}

			return false;
		}

		#endregion

		#region API request

		/// <summary>
		/// Tries to get culture from accept-language.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="cultures">The cultures which gets filtered by the accept-language.</param>
		/// <returns></returns>
		public static CultureInfo TryGetCultureFromAcceptLanguage(this HttpRequestMessage request, CultureInfo[] cultures)
		{
			CultureInfo culture = TryGetCultureFromAcceptLanguage(
				acceptLanguage: request.Headers.AcceptLanguage.ToArray(),
				cultures: cultures);

			return culture;
		}

		/// <summary>
		/// Tries to set culture from accept-language.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="cultures">The cultures which gets filtered by the accept-language.</param>
		/// <returns></returns>
		public static bool TrySetCultureFromAcceptLanguage(this HttpRequestMessage request, CultureInfo[] cultures)
		{
			CultureInfo culture = request.TryGetCultureFromAcceptLanguage(
				cultures: cultures);

			if (culture != null)
			{
				Thread.CurrentThread.CurrentCulture = culture;
				Thread.CurrentThread.CurrentUICulture = culture;

				return true;
			}

			return false;
		}

		#endregion

		#region Accept Language

		/// <summary>
		/// Gets the accept language from the request's header.
		/// </summary>
		/// <param name="httpRequest">The HTTP request.</param>
		/// <returns></returns>
		public static StringWithQualityHeaderValue[] GetAcceptLanguage(this HttpRequestBase httpRequest)
		{
			if (httpRequest.Headers["accept-language"] == null) return new StringWithQualityHeaderValue[0];

			return httpRequest.Headers["accept-language"].ToAcceptLanguage();
		}

		#endregion

		#region Internal methods

		internal static StringWithQualityHeaderValue[] ToAcceptLanguage(this string acceptLanguage)
		{
			if (string.IsNullOrWhiteSpace(acceptLanguage)) return new StringWithQualityHeaderValue[0];

			List<StringWithQualityHeaderValue> languages = new List<StringWithQualityHeaderValue>();

			string value;
			double quality;
			foreach (string language in acceptLanguage.ConvertCommaSeparatedToStringArray())
			{
				value = language.NullCheckTrim();

				if (string.IsNullOrWhiteSpace(value)) continue;

				if (language.Contains(";q="))
				{
					string[] values = value.Split(new string[] { ";q=" }, StringSplitOptions.None);

					value = values[0].Trim();

					if (double.TryParse(values[1].Trim(), out quality))
					{
						languages.Add(new StringWithQualityHeaderValue(value, quality));
					}
				}
				else
				{
					languages.Add(new StringWithQualityHeaderValue(value));
				}
			}

			return languages.ToArray();
		}

		internal static CultureInfo TryGetCultureFromAcceptLanguage(this StringWithQualityHeaderValue[] acceptLanguage, CultureInfo[] cultures)
		{
			if (acceptLanguage == null || acceptLanguage.Any() == false) return null;
			if (cultures == null || cultures.Any() == false) return null;

			// Accept-Language: da, en-gb;q=0.8, en;q=0.7
			// would mean: "I prefer Danish, but will accept British English and other types of English."

			//TODO: Is it okay we do it here or has it to be in the Opten.Common.Extensions?
			// As described here http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html => The quality value defaults to "q=1".
			List<StringWithQualityHeaderValue> languages = new List<StringWithQualityHeaderValue>();
			foreach (StringWithQualityHeaderValue language in acceptLanguage)
			{
				languages.Add(new StringWithQualityHeaderValue(language.Value, language.Quality.HasValue ? language.Quality.Value : 1.0));
			}

			CultureInfo culture = null;
			foreach (StringWithQualityHeaderValue language in languages.OrderByDescending(o => o.Quality))
			{
				if (culture != null) break;

				// First or default because maybe language doesn't exist!
				culture = cultures.FirstOrDefault(o => o.Name.StartsWith(language.Value));
			}

			return culture;
		}

		#endregion

	}
}
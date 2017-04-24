using System;
using System.Globalization;

namespace Opten.Common.Parsers
{
	/// <summary>
	/// The Date Time Parser (ISO, Swiss and more).
	/// </summary>
	public static class DateTimeParser
	{

		static readonly string[] _iso8601Formats = {
			// Basic formats
			"yyyyMMddTHHmmsszzz",
			"yyyyMMddTHHmmsszz",
			"yyyyMMddTHHmmssZ",
			// Extended formats
			"yyyy-MM-ddTHH:mm:sszzz",
			"yyyy-MM-ddTHH:mm:sszz",
			"yyyy-MM-ddTHH:mm:ssZ",
			// All of the above with reduced accuracy
			"yyyyMMddTHHmmzzz",
			"yyyyMMddTHHmmzz",
			"yyyyMMddTHHmmZ",
			"yyyy-MM-ddTHH:mmzzz",
			"yyyy-MM-ddTHH:mmzz",
			"yyyy-MM-ddTHH:mmZ",
			// Accuracy reduced to hours
			"yyyyMMddTHHzzz",
			"yyyyMMddTHHzz",
			"yyyyMMddTHHZ",
			"yyyy-MM-ddTHHzzz",
			"yyyy-MM-ddTHHzz",
			"yyyy-MM-ddTHHZ"
		};

		static readonly string[] _urlFormats = {
			"yyyy-MM-dd"
		};

		static readonly string[] _swissFormats = {
			"dd'.'MM'.'yyyy HH:mm:ss",
			"dd'.'MM'.'yyyy HH:mm",
			"dd'.'MM'.'yyyy"
		};

		/// <summary>
		/// Parses the string to a ISO 8601 date time.
		/// </summary>
		/// <param name="dateTime">The date time.</param>
		/// <returns></returns>
		public static DateTime ParseISO8601String(string dateTime)
		{
			return DateTime.ParseExact(dateTime, _iso8601Formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
		}

		/// <summary>
		/// Parses the URL to a date time.
		/// </summary>
		/// <param name="dateTime">The date time.</param>
		/// <returns></returns>
		public static DateTime ParseUrlString(string dateTime)
		{
			return DateTime.ParseExact(dateTime, _urlFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
		}

		/// <summary>
		/// Parses the string to a swiss date time.
		/// </summary>
		/// <param name="dateTime">The date time.</param>
		/// <returns></returns>
		public static DateTime ParseSwissDateTimeString(string dateTime)
		{
			DateTime date;
			if (DateTime.TryParseExact(dateTime, _swissFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
			{
				return date;
			}
			else
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>
		/// Parses the string to a swiss date time.
		/// </summary>
		/// <param name="dateTime">The date time.</param>
		/// <returns></returns>
		[Obsolete("Use ParseSwissDateTimeString(string dateTime); instead!")]
		public static DateTime ParseSwissDateString(string dateTime)
		{
			return ParseSwissDateTimeString(dateTime);
		}

		/// <summary>
		/// Determines whether the string [is a swiss date].
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static bool IsSwissDate(string input)
		{
			//TODO: Unit tests
			if (string.IsNullOrWhiteSpace(input)) return false;
			if (input.Contains(".") == false) return false;
			if (input.Length < 10) return false;
			return DateTimeParser.ParseSwissDateTimeString(input) > DateTime.MinValue;
		}

		/// <summary>
		/// Parses an unix time number to a UTC DateTime
		/// </summary>
		/// <param name="unixTime">the unix time number</param>
		/// <returns></returns>
		public static DateTime ParseUnixTime(double unixTime)
		{
			DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return dt.AddSeconds(unixTime);
		}

	}
}
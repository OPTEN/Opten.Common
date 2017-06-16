using Opten.Common.Extensions;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Opten.Common.Helpers
{
	/// <summary>
	/// The Display Helper (Human-Readable, Numbers, Dates, ...).
	/// </summary>
	public static class DisplayHelper
	{

		#region Human

		/// <summary>
		/// Gets the full name with first name in front.
		/// </summary>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		/// <returns></returns>
		public static string FullNameWithFirstNameInFront(string firstName, string lastName)
		{
			firstName = firstName.NullCheckTrim();
			lastName = lastName.NullCheckTrim();

			if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
				return string.Empty; //TODO: return null?

			if (string.IsNullOrWhiteSpace(lastName)) return firstName;
			if (string.IsNullOrWhiteSpace(firstName)) return lastName;

			return string.Format("{0} {1}", firstName, lastName);
		}

		/// <summary>
		/// Gets the full name with last name in front.
		/// </summary>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		/// <returns></returns>
		public static string FullNameWithLastNameInFront(string firstName, string lastName)
		{
			firstName = firstName.NullCheckTrim();
			lastName = lastName.NullCheckTrim();

			if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
				return string.Empty; //TODO: return null?

			if (string.IsNullOrWhiteSpace(firstName)) return lastName;
			if (string.IsNullOrWhiteSpace(lastName)) return firstName;

			return string.Format("{1} {0}", firstName, lastName);
		}

		/// <summary>
		/// Gets the full name with surname in front.
		/// </summary>
		/// <param name="firstName">The first name.</param>
		/// <param name="surname">The surname.</param>
		/// <returns></returns>
		[Obsolete("Use FullNameWithLastNameInFront(string firstName, string lastName); instead!")]
		public static string FullNameWithSurnameInFront(string firstName, string surname)
		{
			return FullNameWithLastNameInFront(firstName: firstName, lastName: surname);
		}

		/// <summary>
		/// Displays the decimal in centimeters ({0}cm).
		/// </summary>
		/// <param name="centimeters">The centimeters.</param>
		/// <param name="isHumanBody">if set to <c>true</c> [is human body].</param>
		/// <param name="precision">The precision.</param>
		/// <returns></returns>
		public static string AsCentimeters(this decimal centimeters, bool isHumanBody, int precision)
		{
			if (isHumanBody)
			{
				if (centimeters < 1) return string.Empty;
				if ((centimeters / 100) > 4) return string.Empty; // Taller than 4m is "not" possible
			}

			return string.Format("{0}cm", centimeters.AsNumber(precision: precision)); //TODO: Dictionary
		}

		/// <summary>
		/// Displays the decimal in kilograms ({0}kg).
		/// </summary>
		/// <param name="kilograms">The kilograms.</param>
		/// <param name="isHumanBody">if set to <c>true</c> [is human body].</param>
		/// <param name="precision">The precision.</param>
		/// <returns></returns>
		public static string AsKilograms(this decimal kilograms, bool isHumanBody, int precision)
		{
			if (isHumanBody)
			{
				if (kilograms < 1) return string.Empty;
			}

			return string.Format("{0}kg", kilograms.AsNumber(precision: precision)); //TODO: Dictionary
		}

		/// <summary>
		/// Displays the integer in centimeters ({0}cm).
		/// </summary>
		/// <param name="centimeters">The centimeters.</param>
		/// <param name="isHumanBody">if set to <c>true</c> [is human body].</param>
		/// <returns></returns>
		public static string AsCentimeters(this int centimeters, bool isHumanBody)
		{
			return new decimal(centimeters).AsCentimeters(isHumanBody, 0);
		}

		/// <summary>
		/// Displays the integer in kilograms ({0}kg).
		/// </summary>
		/// <param name="kilograms">The kilograms.</param>
		/// <param name="isHumanBody">if set to <c>true</c> [is human body].</param>
		/// <returns></returns>
		public static string AsKilograms(this int kilograms, bool isHumanBody)
		{
			return new decimal(kilograms).AsKilograms(isHumanBody, 0);
		}

		#endregion

		#region Date

		/// <summary>
		/// Displays the date in Swiss-Format (dd.MM.yyyy).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsSwissDate(this DateTime date)
		{
			return date.ToString("dd.MM.yyyy");
		}

		/// <summary>
		/// Displays the date in Swiss-Format (dd.MM.yyyy).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsSwissDate(this DateTime? date)
		{
			if (date.HasValue == false) return null;

			return date.Value.AsSwissDate();
		}

		/// <summary>
		/// Displays the date and time in Swiss-Format (dd.MM.yyyy HH:mm).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="includeSeconds">if set to <c>true</c> [include seconds].</param>
		/// <returns></returns>
		public static string AsSwissDateTime(this DateTime date, bool includeSeconds = false)
		{
			string format = "dd.MM.yyyy HH:mm";
			if (includeSeconds) format += ":ss";
			return date.ToString(format);
		}

		/// <summary>
		/// Displays the date and time in Swiss-Format (dd.MM.yyyy HH:mm).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="includeSeconds">if set to <c>true</c> [include seconds].</param>
		/// <returns></returns>
		public static string AsSwissDateTime(this DateTime? date, bool includeSeconds = false)
		{
			if (date.HasValue == false) return null;

			return date.Value.AsSwissDateTime(includeSeconds: includeSeconds);
		}

		/// <summary>
		/// Displays the date in Swiss-Format (dddd, d. MMMM yyyy).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsSwissDateLong(this DateTime date)
		{
			return date.ToString("dddd, d. MMMM yyyy");
		}

		/// <summary>
		/// Displays the date in Swiss-Format (dddd, d. MMMM yyyy).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsSwissDateLong(this DateTime? date)
		{
			if (date.HasValue == false) return null;

			return date.Value.AsSwissDateLong();
		}

		/// <summary>
		/// Displays the date in Swiss-Format (dd.MM).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsSwissDateShort(this DateTime date)
		{
			return date.ToString("dd.MM");
		}

		/// <summary>
		/// Displays the date in Swiss-Format (dd.MM).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsSwissDateShort(this DateTime? date)
		{
			if (date.HasValue == false) return null;

			return date.Value.AsSwissDateShort();
		}

		/// <summary>
		/// Displays the date (if midnight) or date time in Swiss-Format.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsSwissDateTimeIgnoreTimeIfMidnight(this DateTime date)
		{
			if (date.IsMidnight())
			{
				return date.AsSwissDate();
			}

			return date.AsSwissDateTime();
		}

		/// <summary>
		/// Displays the date (if midnight) or date time in Swiss-Format.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsSwissDateTimeIgnoreTimeIfMidnight(this DateTime? date)
		{
			if (date.HasValue == false) return null;

			return date.Value.AsSwissDateTimeIgnoreTimeIfMidnight();
		}

		/// <summary>
		/// Displays the date in Swiss-Birthday-Format.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="showAge">if set to <c>true</c> [show age].</param>
		/// <returns></returns>
		public static string AsSwissBirthday(this DateTime date, bool showAge = false)
		{
			if (showAge == false) return date.AsSwissDate();
			else return string.Format("{0} ({1})", date.AsSwissDate(), date.GetAge());
		}

		/// <summary>
		/// Displays the date in Swiss-Birthday-Format.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="showAge">if set to <c>true</c> [show age].</param>
		/// <returns></returns>
		public static string AsSwissBirthday(this DateTime? date, bool showAge = false)
		{
			if (date.HasValue == false) return null;

			return date.Value.AsSwissBirthday(showAge: showAge);
		}

		/// <summary>
		/// Gets the age.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static int GetAge(this DateTime date)
		{
			DateTime today = DateTime.Today;

			int age = today.Year - date.Year;

			if (date > today.AddYears(-age))
			{
				age--;
			}

			return age;
		}

		/// <summary>
		/// Gets the age.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static int GetAge(this DateTime? date)
		{
			if (date.HasValue == false) return 0;

			return date.Value.GetAge();
		}

		/// <summary>
		/// Date in URL-Format (yyyy-MM-dd).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsUrl(this DateTime date)
		{
			return date.ToString("yyyy-MM-dd");
		}

		/// <summary>
		/// Date in URL-Format (yyyy-MM-dd).
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static string AsUrl(this DateTime? date)
		{
			if (date.HasValue == false) return null;

			return date.Value.AsUrl();
		}

		/// <summary>
		/// Determines whether this date is midnight.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static bool IsMidnight(this DateTime date)
		{
			return date.TimeOfDay.Ticks == 0;
		}

		/// <summary>
		/// Determines whether this date is midnight.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static bool IsMidnight(this DateTime? date)
		{
			if (date.HasValue == false) return false;

			return date.Value.IsMidnight();
		}

		#endregion

		#region Time

		/// <summary>
		/// Displays the time in Swiss-Format (hh:mm[:ss]).
		/// </summary>
		/// <param name="time">The time.</param>
		/// <param name="ignoreSeconds">if set to <c>true</c> [ignore seconds].</param>
		/// <returns></returns>
		public static string AsTime(this TimeSpan? time, bool ignoreSeconds = true)
		{
			if (time.HasValue == false) return null;

			return time.Value.AsTime();
		}

		/// <summary>
		/// Displays the time in Swiss-Format (hh:mm[:ss]).
		/// </summary>
		/// <param name="time">The time.</param>
		/// <param name="ignoreSeconds">if set to <c>true</c> [ignore seconds].</param>
		/// <returns></returns>
		public static string AsTime(this TimeSpan time, bool ignoreSeconds = true)
		{
			return time.ToString($@"hh\:mm{(ignoreSeconds ? string.Empty : @"\:ss")}");
		}

		#endregion

		#region Number

		/// <summary>
		/// Displays the decimal as a number with thread's ui culture.
		/// </summary>
		/// <param name="value">The integer.</param>
		/// <param name="precision">The precision.</param>
		/// <returns></returns>
		public static string AsNumber(this decimal value, int precision = 0)
		{
			return value.AsNumber(precision, Thread.CurrentThread.CurrentUICulture);
		}

		/// <summary>
		/// Displays the decimal as a number with desired culture.
		/// </summary>
		/// <param name="value">The integer.</param>
		/// <param name="precision">The precision.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public static string AsNumber(this decimal value, int precision, CultureInfo culture)
		{
			return value.ToString("N" + precision, culture.NumberFormat);
		}

		/// <summary>
		/// Displays the decimal as a number with thread's ui culture.
		/// </summary>
		/// <param name="value">The integer.</param>
		/// <param name="precision">The precision.</param>
		/// <returns></returns>
		public static string AsNumber(this decimal? value, int precision = 0)
		{
			if (value.HasValue == false) return null;

			return value.Value.AsNumber(precision);
		}

		/// <summary>
		/// Displays the decimal as a number with desired culture.
		/// </summary>
		/// <param name="value">The integer.</param>
		/// <param name="precision">The precision.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public static string AsNumber(this decimal? value, int precision, CultureInfo culture)
		{
			if (value == null || value.HasValue == false) return null;

			return value.Value.AsNumber(precision, culture);
		}

		/// <summary>
		/// Displays the integer as a number with thread's ui culture.
		/// </summary>
		/// <param name="value">The integer.</param>
		/// <returns></returns>
		public static string AsNumber(this int value)
		{
			return value.AsNumber(Thread.CurrentThread.CurrentUICulture);
		}

		/// <summary>
		/// Displays the integer as a number with desired culture.
		/// </summary>
		/// <param name="value">The integer.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public static string AsNumber(this int value, CultureInfo culture)
		{
			return value.ToString("N0", culture.NumberFormat);
		}

		/// <summary>
		/// Displays the integer as a number with thread's ui culture.
		/// </summary>
		/// <param name="value">The integer.</param>
		/// <returns></returns>
		public static string AsNumber(this int? value)
		{
			if (value.HasValue == false) return null;

			return value.Value.AsNumber();
		}

		/// <summary>
		/// Displays the integer as a number with desired culture.
		/// </summary>
		/// <param name="value">The integer.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public static string AsNumber(this int? value, CultureInfo culture)
		{
			if (value.HasValue == false) return null;

			return value.Value.AsNumber(culture);
		}

		#endregion

		#region Misc

		/// <summary>
		/// Gets the default value if value is empty.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static string DefaultValueIfEmpty(this string input, string defaultValue)
		{
			if (string.IsNullOrWhiteSpace(input)) return defaultValue;

			return input;
		}

		/// <summary>
		/// Displays the bytes as a string (27.9KB).
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public static string BytesToString(string fileName)
		{
			//http://stackoverflow.com/a/4975942
			long byteCount = File.ReadAllBytes(path: fileName).Length;

			string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB

			if (byteCount == 0) return "0" + suf[0];
			long bytes = Math.Abs(byteCount);
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			double num = Math.Round(bytes / Math.Pow(1024, place), 1);
			return (Math.Sign(byteCount) * num).ToString() + suf[place];
		}

		#endregion

	}
}
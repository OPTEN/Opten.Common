using System;
using System.Collections.Generic;

using Opten.Common.Extensions;
using Opten.Common.Parsers;

namespace Opten.Common.Comparers
{
	/// <summary>
	/// Compares Swiss Numerics, Dates and more.
	/// </summary>
	public class SwissStringComparer : IComparer<string>
	{
		/// <summary>
		/// Compares the inputs.
		/// </summary>
		/// <param name="s1">The s1.</param>
		/// <param name="s2">The s2.</param>
		/// <returns></returns>
		public int Compare(string s1, string s2)
		{
			if (string.IsNullOrWhiteSpace(s1) || string.IsNullOrWhiteSpace(s2))
				return -1;

			if (s1.IsDigitsOnly() && s2.IsDigitsOnly())
			{
				if (Convert.ToInt32(s1) > Convert.ToInt32(s2)) return 1;
				if (Convert.ToInt32(s1) < Convert.ToInt32(s2)) return -1;
				if (Convert.ToInt32(s1) == Convert.ToInt32(s2)) return 0;
			}

			if (DateTimeParser.IsSwissDate(s1) && DateTimeParser.IsSwissDate(s2))
			{
				if (DateTimeParser.ParseSwissDateTimeString(s1) > DateTimeParser.ParseSwissDateTimeString(s2)) return 1;
				if (DateTimeParser.ParseSwissDateTimeString(s1) < DateTimeParser.ParseSwissDateTimeString(s2)) return -1;
				if (DateTimeParser.ParseSwissDateTimeString(s1) == DateTimeParser.ParseSwissDateTimeString(s2)) return 0;
			}

			if (s1.IsDigitsOnly() && !s2.IsDigitsOnly())
				return -1;

			if (DateTimeParser.IsSwissDate(s1) && !DateTimeParser.IsSwissDate(s2))
				return -1;

			if (!s1.IsDigitsOnly() && s2.IsDigitsOnly())
				return 1;

			if (!DateTimeParser.IsSwissDate(s1) && DateTimeParser.IsSwissDate(s2))
				return 1;

			return string.Compare(s1, s2, true);
		}
	}
}
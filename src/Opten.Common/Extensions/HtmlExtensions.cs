using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The HTML Extensions.
	/// </summary>
	public static class HtmlExtensions
	{

		/// <summary>
		/// Determines whether [is null or empty] [the specified HTML].
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <returns></returns>
		public static bool IsNullOrEmpty(this IHtmlString html)
		{
			if (html == null) return true;

			return string.IsNullOrEmpty(html.ToString());
		}

		/// <summary>
		/// Determines whether [is null or whitespace] [the specified HTML].
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <returns></returns>
		public static bool IsNullOrWhiteSpace(this IHtmlString html)
		{
			if (html == null) return true;

			return string.IsNullOrWhiteSpace(html.ToString());
		}

		/// <summary>
		/// Converts the line breaks to HTML line breaks.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <returns></returns>
		public static IHtmlString ConvertLineBreaksToHtmlLineBreaks(this string html)
		{
			string[] lines = html.ConvertLineBreaksToArray();
			if (lines == null || lines.Length == 0) return new HtmlString(string.Empty);

			return new HtmlString(string.Join("<br/>", lines));
		}

		/// <summary>
		/// Converts the line breaks to array.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <returns></returns>
		public static string[] ConvertLineBreaksToArray(this string html)
		{
			if (string.IsNullOrWhiteSpace(html)) return new string[0];

			return html.Split(new string[] { "\n\r", "\n" }, StringSplitOptions.None); // None => empty are <br/>
		}

		/// <summary>
		/// Truncates the text and checks for white space.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="length">The length.</param>
		/// <param name="suffix">The suffix.</param>
		/// <returns></returns>
		public static string TruncateAndCheckForWhiteSpace(this string text, int startIndex = 0, int length = 50, string suffix = "...")
		{
			length = length - suffix.Length;

			text = text.NullCheckTrim();
			if (string.IsNullOrWhiteSpace(text)) return string.Empty;

			if (text.Length <= length) return text;

			string truncated = text.Substring(startIndex, length);
			if (Char.IsWhiteSpace(truncated[truncated.Length - 1]) == false)
				truncated = text.TruncateAndCheckForWhiteSpace(startIndex, length + 1, string.Empty);

			return truncated.Trim() + suffix;
		}

		/// <summary>
		/// Removes the HTML tags.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static string RemoveHtmlTags(this string text)
		{
			return Regex.Replace(text, "<.*?>", string.Empty);
		}

		/// <summary>
		/// Removes the HTML tags.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <returns></returns>
		public static string RemoveHtmlTags(this IHtmlString html)
		{
			return html.ToString().RemoveHtmlTags();
		}

	}
}
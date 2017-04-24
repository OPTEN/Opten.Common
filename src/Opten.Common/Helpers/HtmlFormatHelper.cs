using System.Globalization;
using System.Text.RegularExpressions;

namespace Opten.Common.Helpers
{
	/// <summary>
	/// The HTML Format Helper.
	/// </summary>
	public static class HtmlFormatHelper
	{

		/// <summary>
		/// Rewrites title and href of all html links found in the content (e.g. RTE).
		/// href > {0}
		/// title > {1}
		/// target > {2}
		/// class > {3}
		/// name > {4}
		/// </summary>
		/// <param name="content"></param>
		/// <param name="linkFormat">place {0} and {1}</param>
		/// <returns>string with rewritten links</returns>
		public static string Links(this string content, string linkFormat)
		{
			MatchCollection links = Regex.Matches(content, @"(<a.*?>.*?</a>)", RegexOptions.IgnoreCase);

			string value;
			Match href, title, name, target, className;
			string dataHref, dataTitle, dataName, dataTarget, dataClass, formatted;
			foreach (Match link in links)
			{
				if (link.Groups.Count > 1)
				{
					value = link.Groups[1].Value;

					href = Regex.Match(value, @"href=\""(.*?)\""", RegexOptions.IgnoreCase);
					title = Regex.Match(value, @"title=\""(.*?)\""", RegexOptions.IgnoreCase);
					target = Regex.Match(value, @"target=\""(.*?)\""", RegexOptions.IgnoreCase);
					className = Regex.Match(value, @"class=\""(.*?)\""", RegexOptions.IgnoreCase);
					name = Regex.Match(value, @">(.*?)</a>", RegexOptions.IgnoreCase);

					dataHref = GetValueFromMatch(href); // > {0}
					dataTitle = GetValueFromMatch(title); // > {1}
					dataTarget = GetValueFromMatch(target); // > {2}
					dataClass = GetValueFromMatch(className); // > {3}
					dataName = GetValueFromMatch(name); // > {4}

					formatted = string.Format(CultureInfo.InvariantCulture, linkFormat, dataHref, dataTitle, dataTarget, dataClass, dataName);
					content = content.Replace(value, formatted);
				}
			}

			return content;
		}

		private static string GetValueFromMatch(Match match)
		{
			if (match == null || match.Success == false || match.Groups.Count < 2)
			{
				return string.Empty;
			}

			return match.Groups[1].Value.ToString();
		}
	}
}
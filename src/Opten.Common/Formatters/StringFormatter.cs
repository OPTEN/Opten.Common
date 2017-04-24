using System;

namespace Opten.Common.Formatters
{
	/// <summary>
	/// String Format Provider.
	/// </summary>
	public class StringFormatter : IFormatProvider, ICustomFormatter
	{

		/// <summary>
		/// Returns an object that provides formatting services for the specified type.
		/// </summary>
		/// <param name="formatType">An object that specifies the type of format object to return.</param>
		/// <returns>
		/// An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, null.
		/// </returns>
		public object GetFormat(Type formatType)
		{
			if (formatType == typeof(ICustomFormatter))
				return this;
			else
				return null;
		}

		/// <summary>
		/// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
		/// </summary>
		/// <param name="format">A format string containing formatting specifications.</param>
		/// <param name="arg">An object to format.</param>
		/// <param name="formatProvider">An object that supplies format information about the current instance.</param>
		/// <returns>
		/// The string representation of the value of <paramref name="arg" />, formatted as specified by <paramref name="format" /> and <paramref name="formatProvider" />.
		/// </returns>
		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			string result = arg.ToString();

			if (string.IsNullOrWhiteSpace(result))
				return string.Empty;

			result = result.Trim();

			if (string.IsNullOrWhiteSpace(format))
				return result;

			switch (format.ToUpper())
			{
				case "()":
				case "RB":
				case "ROUNDBRACKET":
				case "ROUNDBRACKETS":
				case "ROUNDBRACE":
				case "ROUNDBRACES": return "(" + result + ")";

				case "{}":
				case "CB":
				case "CURLYBRACKET":
				case "CURLYBRACKETS":
				case "CURLYBRACE":
				case "CURLYBRACES": return "{" + result + "}";

				case "[]":
				case "SB":
				case "SQUAREBRACKET":
				case "SQUAREBRACKETS":
				case "SQUAREBRACE":
				case "SQUAREBRACES": return "[" + result + "]";

				default: return result;
			}
		}
	}
}
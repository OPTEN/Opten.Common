using System.Text.RegularExpressions;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The JSON Extensions.
	/// </summary>
	public static class JsonExtensions
	{

		/// <summary>
		/// Converts the json to a string array.
		/// </summary>
		/// <param name="json">The json.</param>
		/// <returns></returns>
		public static string[] ConvertJsonToArray(this string json)
		{
			if (string.IsNullOrWhiteSpace(json)) return new string[0];

			Regex regex = new Regex("[^\\d\\,]");

			json = regex.Replace(json, string.Empty).Trim();

			return json.Split(',');
		}

		/// <summary>
		/// Detects if the string is json.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static bool DetectIsJson(this string input)
		{
			if (string.IsNullOrWhiteSpace(input)) return false;

			input = input.Trim();
			return (input.StartsWith("{") && input.EndsWith("}"))
				   || (input.StartsWith("[") && input.EndsWith("]"));
		}

	}
}
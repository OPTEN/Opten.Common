using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Array Extensions.
	/// </summary>
	public static class ArrayExtensions
	{

		/// <summary>
		/// Converts the string array to int array.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <returns></returns>
		public static int[] ConvertStringArrayToIntArray(this string[] array)
		{
			if (array.Length == 0) return new int[0];

			int integer;
			List<int> newArray = new List<int>();
			foreach (string item in array)
			{
				if (string.IsNullOrWhiteSpace(item)) continue;

				if (int.TryParse(item.Trim(), out integer))
					newArray.Add(integer);
			}

			return newArray.ToArray();
		}

		/// <summary>
		/// Converts the text to array.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		public static string[] TextToArray(this string text, int length)
		{
			text = text.NullCheckTrim();
			if (string.IsNullOrWhiteSpace(text)) return new string[0];

			if (text.Length <= length) return new string[] { text };

			return Regex.Matches(text, ".{1," + length + "}").Cast<Match>().Select(m => m.Value).ToArray();
		}

		/// <summary>
		/// Determines whether the array contains any from the other array.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array">The array.</param>
		/// <param name="otherArray">The other array.</param>
		/// <returns></returns>
		public static bool ContainsAny<T>(this T[] array, T[] otherArray)
		{
			if (otherArray == null) return false;
			return array.Any(t => otherArray.Contains(t));
		}

	}
}
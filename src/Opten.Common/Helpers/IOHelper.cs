using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Opten.Common.Helpers
{
	/// <summary>
	/// The IO Helper.
	/// </summary>
	public static class IOHelper
	{

		/// <summary>
		/// Gets a valid path name (checks for /\: etc).
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="replaceChar">The replace character.</param>
		/// <returns></returns>
		public static string GetPathName(string name, char replaceChar = ' ')
		{
			// No need for null check trim, should break!
			name = name.Trim();

			char[] invalid = Path.GetInvalidFileNameChars();

			// Check if the replace char is not invalid
			if (invalid.Contains(replaceChar))
			{
				replaceChar = ' ';
			}

			// Replace invalid char in the path name with the replacing char
			foreach (char c in invalid)
			{
				name = name.Replace(c, replaceChar);
			}

			// Replace multiple whitespaces with one whitespace
			name = Regex.Replace(name, @"\s+", " ");

			return name;
		}
		
		/// <summary>
		/// Search files.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="searchPattern">The search pattern.</param>
		/// <param name="searchOption">The search option.</param>
		/// <param name="stopIfSomethingFoundInADirectory">if set to <c>true</c> search stops when something found.</param>
		/// <returns></returns>
		public static string[] SearchFiles(string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool stopIfSomethingFoundInADirectory = false)
		{
			List<string> found = new List<string>();

			if (string.IsNullOrWhiteSpace(searchPattern) || string.IsNullOrWhiteSpace(path)) return new string[0];

			foreach (string file in Directory.EnumerateFiles(path: path, searchPattern: searchPattern, searchOption: searchOption))
			{
				if (stopIfSomethingFoundInADirectory)
				{
					return new string[] { file };
				}
				else
				{
					lock (found) found.Add(file);
				}
			}

			return found.ToArray();
		}

		/// <summary>
		/// Finds the first file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="fileName">The file name.</param>
		/// <param name="searchOption">The search option.</param>
		/// <param name="ignoreExtension">if set to <c>true</c> [ignore extension].</param>
		/// <returns></returns>
		public static string FindFirstFile(string path, string fileName, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreExtension = true)
		{
			if (ignoreExtension)
			{
				fileName = FileHelper.GetFileNameWithoutExtension(fileName) + ".*";
			}

			string[] found = SearchFiles(
				path: path,
				searchPattern: fileName,
				searchOption: searchOption,
				stopIfSomethingFoundInADirectory: true);

			if (found == null || found.Any() == false) return string.Empty;

			return found.First();
		}

	}
}
using System.Linq;
using System.Collections.Generic;

using Opten.Common.Interfaces;

namespace Opten.Common.Formatters
{
	/// <summary>
	/// The Pattern Formatter.
	/// </summary>
	public class PatternReplaceFormatter
	{

		private readonly IDictionary<string, IPatternReplacement> _patterns;

		/// <summary>
		/// Initializes a new instance of the <see cref="PatternReplaceFormatter" /> class.
		/// </summary>
		/// <param name="patterns">The patterns.</param>
		public PatternReplaceFormatter(IDictionary<string, IPatternReplacement> patterns)
		{
			_patterns = patterns;
		}

		/// <summary>
		/// Formats the input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public string[] Format(string[] input)
		{
			// Format the input with the provided patterns
			if (input == null || input.Any() == false || CanFormat() == false) return input;
			return (from item in input select Format(input: item)).ToArray();
		}

		/// <summary>
		/// Formats the input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public string Format(string input)
		{
			// Format the input with the provided patterns
			if (CanFormat())
			{
				foreach (KeyValuePair<string, IPatternReplacement> pattern in _patterns)
				{
					input = pattern.Value.Replace(pattern.Key, input);
				}
			}

			return input;
		}

		private bool CanFormat()
		{
			if (_patterns == null) return false;

			return _patterns.Any();
		}

	}
}
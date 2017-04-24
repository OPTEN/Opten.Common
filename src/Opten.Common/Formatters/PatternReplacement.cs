using Opten.Common.Interfaces;

namespace Opten.Common.Formatters
{
	/// <summary>
	/// A Replacement.
	/// </summary>
	public class PatternReplacement : IPatternReplacement
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="PatternReplacement" /> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public PatternReplacement(string value)
		{
			this.Value = value;
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public string Value { get; private set; }

		/// <summary>
		/// The replace method.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public string Replace(string key, string input)
		{
			return input.Replace(key, this.Value);
		}

	}
}
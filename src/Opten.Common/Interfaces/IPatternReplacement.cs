namespace Opten.Common.Interfaces
{
	/// <summary>
	/// A Replacement.
	/// </summary>
	public interface IPatternReplacement
	{

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		string Value { get; }

		/// <summary>
		/// The replace method.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		string Replace(string key, string input);

	}
}
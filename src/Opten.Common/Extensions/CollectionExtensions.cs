using System.Collections.Specialized;
using System.ComponentModel;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Collection Extensions.
	/// </summary>
	public static class CollectionExtensions
	{

		/// <summary>
		/// Gets the value by key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static T Get<T>(this NameValueCollection collection, string key)
		{
			T defaultValue = default(T);

			return collection.Get<T>(key, defaultValue);
		}

		/// <summary>
		/// Gets the value by key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static T Get<T>(this NameValueCollection collection, string key, T defaultValue)
		{
			string value = collection[key];
			if (string.IsNullOrWhiteSpace(value)) return defaultValue;

			// http://www.hanselman.com/blog/TypeConvertersTheresNotEnoughTypeDescripterGetConverterInTheWorld.aspx
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
			return (T)(converter.ConvertFromInvariantString(value));
		}

	}
}
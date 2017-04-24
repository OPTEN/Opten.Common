using System;
using System.Reflection;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Attribute Extensions.
	/// </summary>
	public static class AttributeExtensions
	{

		/// <summary>
		/// Gets the custom attributes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			return provider.GetCustomAttributes<T>(true);
		}

		/// <summary>
		/// Gets the custom attributes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="provider">The provider.</param>
		/// <param name="inherit">if set to <c>true</c> [inherit].</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">provider</exception>
		public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider provider, bool inherit) where T : Attribute
		{
			if (provider == null) throw new ArgumentNullException("provider");

			T[] array = provider.GetCustomAttributes(typeof(T), inherit) as T[];

			if (array == null)
			{
				return new T[0];
			}

			return array;
		}

		/// <summary>
		/// Gets the single attribute.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">memberInfo</exception>
		public static T GetSingleAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			if (provider == null) throw new ArgumentNullException("provider");

			object[] customAttributes = provider.GetCustomAttributes(typeof(T), false);

			if (customAttributes.Length > 0)
			{
				return (T)((object)customAttributes[0]);
			}

			return default(T);
		}

		/// <summary>
		/// Determines whether [has custom attribute].
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">memberInfo</exception>
		public static bool HasCustomAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			if (provider == null) throw new ArgumentNullException("provider");

			return provider.GetSingleAttribute<T>() != null;
		}
	}
}
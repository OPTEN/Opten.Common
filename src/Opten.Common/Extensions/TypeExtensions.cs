using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Type Extensions.
	/// </summary>
	public static class TypeExtensions
	{

		/// <summary>
		/// Gets the properties with attribute.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">The type.</param>
		/// <param name="bindingFlags">The binding flags.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">type;The type is missing!</exception>
		public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(
			this Type type,
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public) where T : Attribute
		{
			if (type == null) throw new ArgumentNullException("type", "The type is missing!");

			return
				from pi in type.GetProperties(bindingFlags)
				where pi.HasCustomAttribute<T>()
				select pi;
		}

		/// <summary>
		/// Gets the type of the read and writeable properties of.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">The type.</param>
		/// <param name="bindingFlags">The binding flags.</param>
		/// <returns></returns>
		public static IEnumerable<PropertyInfo> GetReadAndWriteablePropertiesOfType<T>(
			this Type type,
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
		{
			return
				from pi in type.GetProperties(bindingFlags)
				where pi.PropertyType == typeof(T)
				where pi.CanWrite
				where pi.CanRead
				select pi;
		}

	}
}
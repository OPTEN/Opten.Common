using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Opten.Common.Extensions
{
#pragma warning disable CS1591

	public static class AttributeExtensions
	{

		public static TAttribute FirstAttribute<TAttribute>(this Type type)
		{
			return type.FirstAttribute<TAttribute>(true);
		}

		public static TAttribute FirstAttribute<TAttribute>(this Type type, bool inherit)
		{
			object[] attrs = type.GetCustomAttributes(typeof(TAttribute), inherit);
			return (TAttribute)(attrs.Length > 0 ? attrs[0] : null);
		}

		public static TAttribute FirstAttribute<TAttribute>(this PropertyInfo propertyInfo)
		{
			return propertyInfo.FirstAttribute<TAttribute>(true);
		}

		public static TAttribute FirstAttribute<TAttribute>(this PropertyInfo propertyInfo, bool inherit)
		{
			var attrs = propertyInfo.GetCustomAttributes(typeof(TAttribute), inherit);
			return (TAttribute)(attrs.Length > 0 ? attrs[0] : null);
		}

		public static TAttribute GetCustomAttribute<TAttribute>(this Type type, bool inherit) where TAttribute : Attribute
		{
			return type.GetCustomAttributes<TAttribute>(inherit).SingleOrDefault();
		}

		public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this Type type, bool inherited) where TAttribute : Attribute
		{
			if (type == null) return Enumerable.Empty<TAttribute>();
			return type.GetCustomAttributes(typeof(TAttribute), inherited).OfType<TAttribute>();
		}

	}

#pragma warning restore CS1591

}
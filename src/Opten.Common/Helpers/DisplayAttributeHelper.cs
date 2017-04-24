using Opten.Common.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Opten.Common.Helpers
{
	/// <summary>
	/// The Display Attribute Helper.
	/// </summary>
	public static class DisplayAttributeHelper
	{

		/// <summary>
		/// Gets the property display string.
		/// </summary>
		/// <typeparam name="TClass"></typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">No property reference expression was found.;propertyExpression</exception>
		public static string GetPropertyDisplayString<TClass>(Expression<Func<TClass, object>> propertyExpression) where TClass : class
		{
			MemberExpression member = propertyExpression.GetMemberExpression();
			
			PropertyInfo propertyInfo = member.Member as PropertyInfo;

			DisplayAttribute displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>(
				inherit: false);

			if (displayAttribute != null)
			{
				return displayAttribute.Name;
			}
			else
			{
				DisplayNameAttribute displayNameAttribute = propertyInfo.GetCustomAttribute<DisplayNameAttribute>(
					inherit: false);

				if (displayNameAttribute != null)
				{
					return displayNameAttribute.DisplayName;
				}
				else
				{
					return propertyInfo.Name;
				}
			}
		}
	}
}

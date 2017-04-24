using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Expression Extensions.
	/// </summary>
	public static class ExpressionExtensions
	{
		
		/// <summary>
		/// Gets the member expression.
		/// </summary>
		/// <typeparam name="TClass">The type of the class.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="propertyLambda">The property lambda.</param>
		/// <returns></returns>
		public static MemberExpression GetMemberExpression<TClass, TProperty>(
			this Expression<Func<TClass, TProperty>> propertyLambda) where TClass : class
		{
			MemberExpression member;

			// Maybe it's something like Convert(p => p.Price)
			// because we have a Expression<Func<Product, dynamic>> (but it's type is decimal)
			// so we have to get the operand of the expression
			switch (propertyLambda.Body.NodeType)
			{
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
					UnaryExpression unaryExpression = propertyLambda.Body as UnaryExpression;
					member = ((unaryExpression == null) ? null : unaryExpression.Operand) as MemberExpression;
					break;
				default:
					member = propertyLambda.Body as MemberExpression;
					break;
			}

			return member;
		}

		/// <summary>
		/// Gets the name of the argument; Foo(() => testClass) > testClass).
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="memberExpression">The member expression.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException"></exception>
		public static string GetArgumentName<T>(
			this Expression<Func<T>> memberExpression) where T : class
		{
			MemberExpression member = memberExpression.Body as MemberExpression;

			if (member == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					memberExpression.ToString()));

			return member.Member.Name;
		}

		/// <summary>
		/// Gets the property's name of the argument; Foo(o => o.Name) > Name).
		/// </summary>
		/// <typeparam name="TClass">The type of the class.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="propertyLambda">The property lambda.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">
		/// </exception>
		public static string GetArgumentName<TClass, TProperty>(
			this Expression<Func<TClass, TProperty>> propertyLambda) where TClass : class
		{
			Type type = typeof(TClass);

			MemberExpression member = propertyLambda.GetMemberExpression();

			if (member == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					propertyLambda.ToString()));

			PropertyInfo propertyInfo = member.Member as PropertyInfo;

			if (propertyInfo == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a field, not a property.",
					propertyLambda.ToString()));

			if ((type == propertyInfo.ReflectedType) == false &&
				type.IsSubclassOf(propertyInfo.ReflectedType) == false)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a property that is not from type {1}.",
					propertyLambda.ToString(),
					type));

			return propertyInfo.Name;
		}

	}
}
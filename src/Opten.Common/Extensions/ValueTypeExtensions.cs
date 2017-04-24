using System;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Value Type Extensions (T).
	/// </summary>
	public static class ValueTypeExtensions
	{
		/// <summary>
		/// Coalescings the default value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static T Coalescing<T>(this T value, T defaultValue) where T : struct
		{
			return (value.IsNotEmpty() ? value : defaultValue);
		}

		/// <summary>
		/// Determines whether the type is empty.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static bool IsEmpty<T>(this T value) where T : struct
		{
			return value.Equals(default(T));
		}

		/// <summary>
		/// Determines whether the type [is not empty].
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static bool IsNotEmpty<T>(this T value) where T : struct
		{
			return (value.IsEmpty() == false);
		}

		/// <summary>
		/// Determines if the type is empty.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static T IfEmpty<T>(this T value, T defaultValue) where T : struct
		{
			return (value.IsNotEmpty() ? value : defaultValue);
		}

		/// <summary>
		/// Determines if the type is empty.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static bool IfEmpty<T>(this T value, Action<T> action) where T : struct
		{
			if (value.IsEmpty())
			{
				action(value);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Determines if the type is empty.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static bool IfNotEmpty<T>(this T value, Action<T> action) where T : struct
		{
			if (value.IsNotEmpty())
			{
				action(value);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Formats the type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		public static string Format<T>(this T value, string format) where T : struct
		{
			return string.Format(format, value);
		}

		/// <summary>
		/// Formats the type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		public static string FormatIf<T>(this T value, string format) where T : struct
		{
			return FormatIf(value, null, format, null);
		}

		/// <summary>
		/// Formats the type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="format">The format.</param>
		/// <param name="defaultText">The default text.</param>
		/// <returns></returns>
		public static string FormatIf<T>(this T value, string format, string defaultText) where T : struct
		{
			return FormatIf(value, null, format, defaultText);
		}

		/// <summary>
		/// Formats the type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="condition">The condition.</param>
		/// <param name="format">The format.</param>
		/// <param name="defaultText">The default text.</param>
		/// <returns></returns>
		public static string FormatIf<T>(this T value, Func<T, bool> condition, string format, string defaultText) where T : struct
		{
			if (condition == null)
				condition = v => v.IsNotEmpty();
			return (condition(value) ? string.Format(format, value) : defaultText);
		}

		/// <summary>
		/// Gets the nullable of the type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static Nullable<T> GetNullable<T>(this T value) where T : struct
		{
			return (value.IsEmpty() ? (Nullable<T>)null : (Nullable<T>)value);
		}
	}
}
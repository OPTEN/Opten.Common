using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The IEnumerable extensions.
	/// </summary>
	public static class EnumerableExtensions
	{

		/// <summary>
		/// Gets the index of the item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static int IndexOf<T>(this IEnumerable<T> obj, T value)
		{
			return obj.IndexOf(value, null);
		}

		/// <summary>
		/// Gets the index of the item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="value">The value.</param>
		/// <param name="comparer">The comparer.</param>
		/// <returns></returns>
		public static int IndexOf<T>(this IEnumerable<T> obj, T value, IEqualityComparer<T> comparer)
		{
			comparer = comparer ?? EqualityComparer<T>.Default;
			var found = obj
				.Select((a, i) => new { a, i })
				.FirstOrDefault(o => comparer.Equals(o.a, value));
			return found == null ? -1 : found.i;
		}

		/// <summary>
		/// Converts the string enumerable to int enumerable.
		/// </summary>
		/// <param name="enumerable">The enumerable.</param>
		/// <returns></returns>
		public static IEnumerable<int> ConvertStringEnumerableToIntEnumerable(this IEnumerable<string> enumerable)
		{
			if (enumerable.Count<string>() == 0) return Enumerable.Empty<int>();

			int integer;
			List<int> newEnumerable = new List<int>();
			foreach (string item in enumerable)
			{
				if (string.IsNullOrWhiteSpace(item)) continue;

				if (int.TryParse(item.Trim(), out integer))
					newEnumerable.Add(integer);
			}

			return newEnumerable.AsEnumerable();
		}

		/// <summary>
		/// Distincts by the key selector.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="keySelector">The key selector.</param>
		/// <returns></returns>
		public static IEnumerable<TSource> Distinct/*By*/<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
			  where TKey : IEquatable<TKey>
		{
			//HINT: Cannot name as 'DistinctBy' because Umbraco already uses this Extension name :(.
			HashSet<TKey> seenKeys = new HashSet<TKey>();
			foreach (TSource element in source)
			{
				if (seenKeys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
		}

		/// <summary>
		/// Determines whether the specified enumerable contains any of the other enumerable.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable">The enumerable.</param>
		/// <param name="otherEnumerable">The other enumerable.</param>
		/// <returns></returns>
		public static bool ContainsAny<T>(this IEnumerable<T> enumerable, IEnumerable<T> otherEnumerable)
		{
			if (otherEnumerable == null) return false;
			return enumerable.Any(t => otherEnumerable.Contains(t));
		}

		/// <summary>
		/// Shuffles the enumerable.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable">The enumerable.</param>
		/// <returns></returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
		{
			List<T> list = enumerable.ToList();
			using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
			{
				int n = list.Count<T>();
				while (n > 1)
				{
					byte[] box = new byte[1];
					do provider.GetBytes(box);
					while (!(box[0] < n * (Byte.MaxValue / n)));
					int k = (box[0] % n);
					n--;
					T value = list[k];
					list[k] = list[n];
					list[n] = value;
				}
				return list;
			}
		}
		
		// => throws Exception because already implemented by Umbraco :(
		//public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source) where T : class
		//{
		//	return source.Where(o => o != null);
		//}

		/// <summary>
		/// Determines whether the Enumerable has any value which is not null or whitespace.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns>
		///   <c>true</c> if the specified source has values; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasValues(this IEnumerable<string> source)
		{
			if (source == null || source.Any() == false) return false;

			return source.Any(o => string.IsNullOrWhiteSpace(o) == false);
		}

		/// <summary>
		/// Determines whether the Enumerable has any value which is not null or whitespace.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns>
		///   <c>true</c> if the specified source has values; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasValues(this IEnumerable<IEnumerable<string>> source)
		{
			if (source == null || source.Any() == false) return false;

			return source.Any(o => o.HasValues());
		}


		/// <summary>
		/// Determines whether [is null or empty].
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <returns>
		///   <c>true</c> if [is null or empty] [the specified data]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
		{
			return source == null || source.Any() == false;
		}
	}
}
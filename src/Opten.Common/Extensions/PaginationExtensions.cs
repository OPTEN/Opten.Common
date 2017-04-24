using Opten.Common.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Pagination extensions.
	/// </summary>
	public static class PaginationExtensions
	{

		/// <summary>
		/// Creates a subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
		/// </summary>
		/// <typeparam name="T">The type of object the collection should contain.</typeparam>
		/// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}" />, it will be treated as such.</param>
		/// <param name="currentPage">The one-based index of the subset of objects to be contained by this instance.</param>
		/// <param name="itemsPerPage">The maximum size of any individual subset.</param>
		/// <param name="maxSize">The maximum size.</param>
		/// <returns>
		/// A subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
		/// </returns>
		/// <seealso cref="Pagination{T}" />
		public static IPagination<T> ToPagedList<T>(this IEnumerable<T> superset, int currentPage, int itemsPerPage = 25, int maxSize = 7)
		{
			return new Pagination<T>(superset, currentPage, itemsPerPage, maxSize);
		}

		/// <summary>
		/// Creates a subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
		/// </summary>
		/// <typeparam name="T">The type of object the collection should contain.</typeparam>
		/// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}" />, it will be treated as such.</param>
		/// <param name="currentPage">The one-based index of the subset of objects to be contained by this instance.</param>
		/// <param name="itemsPerPage">The maximum size of any individual subset.</param>
		/// <param name="maxSize">The maximum size.</param>
		/// <returns>
		/// A subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
		/// </returns>
		/// <seealso cref="Pagination{T}" />
		public static IPagination<T> ToPagedList<T>(this IQueryable<T> superset, int currentPage, int itemsPerPage = 25, int maxSize = 7)
		{
			return new Pagination<T>(superset, currentPage, itemsPerPage, maxSize);
		}

		/// <summary>
		/// Creates a subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
		/// </summary>
		/// <typeparam name="T">The type of object the collection should contain.</typeparam>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}" />, it will be treated as such.</param>
		/// <param name="keySelector">The key selector.</param>
		/// <param name="isAscending">if set to <c>true</c> [is ascending].</param>
		/// <param name="currentPage">The current page.</param>
		/// <param name="itemsPerPage">The items per page.</param>
		/// <param name="maxSize">The maximum size.</param>
		/// <returns>
		/// A subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
		/// </returns>
		/// <seealso cref="Pagination{T}" />
		public static IPagination<T> ToPagedList<T, TKey>(this IQueryable<T> superset, Expression<Func<T, TKey>> keySelector, bool isAscending, int currentPage, int itemsPerPage = 25, int maxSize = 7)
		{
			return new PaginationQuery<T, TKey>(superset, keySelector, isAscending, currentPage, itemsPerPage, maxSize);
		}

		/// <summary>
		/// Splits a collection of objects into an unknown number of pages with n items per page (for example, if I have a list of 45 shoes and say 'shoes.Partition(10)' I will now have 4 pages of 10 shoes and 1 page of 5 shoes).
		/// </summary>
		/// <typeparam name="T">The type of object the collection should contain.</typeparam>
		/// <param name="superset">The collection of objects to be divided into subsets.</param>
		/// <param name="pageSize">The maximum number of items each page may contain.</param>
		/// <returns>
		/// A subset of this collection of objects, split into pages of maximum size n.
		/// </returns>
		public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> superset, int pageSize)
		{
			// Cache this to avoid evaluating it twice 
			int count = superset.Count();
			if (count < pageSize)
				yield return superset;
			else
			{
				var numberOfPages = Math.Ceiling(count / (double)pageSize);
				for (var i = 0; i < numberOfPages; i++)
					yield return superset.Skip(pageSize * i).Take(pageSize);
			}
		}

		/// <summary>
		/// Splits a collection of objects into n pages with an (for example, if I have a list of 45 shoes and say 'shoes.InGroupsOf(5)' I will now have 4 pages of 10 shoes and 1 page of 5 shoes).
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="numberOfGroups">The number of groups.</param>
		/// <returns></returns>
		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int numberOfGroups)
		{
			if (numberOfGroups < 1) return null;

			return source
				.Select((item, index) => new { index, item })
				.GroupBy(x => x.index % numberOfGroups)
				.Select(x => x.Select(y => y.item));
		}

	}
}
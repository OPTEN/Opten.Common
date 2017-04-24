using System;
using System.Linq;
using System.Linq.Expressions;

namespace Opten.Common.Page
{
	/// <summary>
	/// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <seealso cref="Opten.Common.Page.PaginationBase{T}" />
	public class PaginationQuery<T, TKey> : PaginationBase<T>
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginationQuery{T, TKey}" /> class.
		/// </summary>
		/// <param name="superset">The superset.</param>
		/// <param name="keySelector">The key selector.</param>
		/// <param name="isAscending">if set to <c>true</c> [is ascending].</param>
		/// <param name="currentPage">The current page.</param>
		/// <param name="itemsPerPage">The items per page.</param>
		/// <param name="maxSize">The maximum size.</param>
		public PaginationQuery(
			IQueryable<T> superset,
			Expression<Func<T, TKey>> keySelector,
			bool isAscending,
			int currentPage,
			int itemsPerPage = 25,
			int maxSize = 7)
			: base(page: currentPage,
				itemsPerPage: itemsPerPage,
				maxSize: maxSize,
				totalItemCount: superset == null ? 0 : superset.Count())
		{
			if (superset != null)
			{
				if (isAscending)
					superset = superset.OrderBy(keySelector);
				else
					superset = superset.OrderByDescending(keySelector);

				this.Subset.AddRange(superset.Skip<T>(this.Skip).Take<T>(this.Take).ToList()); // execute by .ToList()
			}
		}

	}
}
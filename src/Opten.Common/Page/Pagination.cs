using System.Collections.Generic;
using System.Linq;

namespace Opten.Common.Page
{
	/// <summary>
	/// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="Opten.Common.Page.PaginationBase{T}" />
	public class Pagination<T> : PaginationBase<T>
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="Pagination{T}"/> class.
		/// </summary>
		/// <param name="superset">The superset.</param>
		/// <param name="currentPage">The current page.</param>
		/// <param name="itemsPerPage">The items per page.</param>
		/// <param name="maxSize">The maximum size.</param>
		public Pagination(
			IQueryable<T> superset,
			int currentPage,
			int itemsPerPage = 25,
			int maxSize = 7)
			: base(page: currentPage,
				   itemsPerPage: itemsPerPage,
				   maxSize: maxSize,
				   totalItemCount: superset == null ? 0 : superset.Count())
		{
			this.Subset.AddRange(superset.Skip<T>(this.Skip).Take<T>(this.Take));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pagination{T}"/> class.
		/// </summary>
		/// <param name="superset">The superset.</param>
		/// <param name="currentPage">The current page.</param>
		/// <param name="itemsPerPage">The items per page.</param>
		/// <param name="maxSize">The maximum size.</param>
		public Pagination(
			IEnumerable<T> superset,
			int currentPage,
			int itemsPerPage = 25,
			int maxSize = 7) : this(
				superset: superset.AsQueryable(),
				currentPage: currentPage,
				itemsPerPage: itemsPerPage,
				maxSize: maxSize)
		{ }

	}
}
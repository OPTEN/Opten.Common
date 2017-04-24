using System.Collections.Generic;

namespace Opten.Common.Page
{
	/// <summary>
	///  Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class StaticPagination<T> : PaginationBase<T>
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="StaticPagination{T}" /> class.
		/// </summary>
		/// <param name="subset">The subset.</param>
		/// <param name="metadata">The metadata.</param>
		public StaticPagination(IEnumerable<T> subset, IPagination metadata)
			: this(subset: subset,
				   page: metadata.CurrentPage,
				   itemsPerPage: metadata.Take,
				   maxSize: metadata.MaxSize,
				   totalItemCount: metadata.TotalItemCount) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="StaticPagination{T}" /> class.
		/// </summary>
		/// <param name="subset">The subset.</param>
		/// <param name="page">The current page.</param>
		/// <param name="itemsPerPage">The items per page.</param>
		/// <param name="maxSize">The maximum size.</param>
		/// <param name="totalItemCount">The total item count.</param>
		public StaticPagination(IEnumerable<T> subset, int page, int itemsPerPage, int maxSize, int totalItemCount)
			: base(page, itemsPerPage, maxSize, totalItemCount)
		{
			this.Subset.AddRange(subset);
		}

	}
}

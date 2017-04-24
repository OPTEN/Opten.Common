using System.Collections.Generic;

namespace Opten.Common.Page
{
	/// <summary>
	/// The Pagination Metadata.
	/// </summary>
	public class PaginationMetadata : IPagination
	{

		/// <summary>
		/// Protected constructor that allows for instantiation without passing in a separate list.
		/// </summary>
		protected PaginationMetadata() { }

		/// <summary>
		/// Non-enumerable version of the Paging class.
		/// </summary>
		/// <param name="pager">The pager.</param>
		public PaginationMetadata(IPagination pager)
		{
			this.CurrentPage = pager.CurrentPage;
			this.TotalPages = pager.TotalPages;
			this.Skip = pager.Skip;
			this.Take = pager.Take;
			this.MaxSize = pager.MaxSize;
			this.TotalItemCount = pager.TotalItemCount;
			this.Pages = pager.Pages;
			this.NoPrevious = pager.NoPrevious;
			this.PreviousPage = pager.PreviousPage;
			this.NoNext = pager.NoNext;
			this.NextPage = pager.NextPage;
			this.HasPages = pager.HasPages;
			this.FromItems = pager.FromItems;
			this.ToItems = pager.ToItems;
		}

		/// <summary>
		/// Gets the current page.
		/// </summary>
		/// <value>
		/// The current page.
		/// </value>
		public int CurrentPage { get; protected set; }

		/// <summary>
		/// Gets the total pages.
		/// </summary>
		/// <value>
		/// The total pages.
		/// </value>
		public int TotalPages { get; protected set; }

		/// <summary>
		/// Gets the page number (skip).
		/// </summary>
		/// <value>
		/// The items per page.
		/// </value>
		public int Skip { get; protected set; }

		/// <summary>
		/// Gets the items per page (take).
		/// </summary>
		/// <value>
		/// The items per page.
		/// </value>
		public int Take { get; protected set; }

		/// <summary>
		/// The maximum pages which are shown.
		/// </summary>
		/// <value>
		/// The maximum size.
		/// </value>
		public int MaxSize { get; protected set; }

		/// <summary>
		/// Gets the total item count.
		/// </summary>
		/// <value>
		/// The total item count.
		/// </value>
		public int TotalItemCount { get; protected set; }

		/// <summary>
		/// The page numbers.
		/// </summary>
		/// <value>
		/// The pages.
		/// </value>
		public IEnumerable<int> Pages { get; protected set; }

		/// <summary>
		/// Indicates if there is no previous page.
		/// </summary>
		/// <value>
		///   <c>true</c> if [no previous]; otherwise, <c>false</c>.
		/// </value>
		public bool NoPrevious { get; protected set; }

		/// <summary>
		/// Gets the previous page.
		/// </summary>
		/// <value>
		/// The previous page.
		/// </value>
		public int PreviousPage { get; protected set; }

		/// <summary>
		/// Indicates if there is no next page.
		/// </summary>
		/// <value>
		///   <c>true</c> if [no next]; otherwise, <c>false</c>.
		/// </value>
		public bool NoNext { get; protected set; }

		/// <summary>
		/// Gets the next page.
		/// </summary>
		/// <value>
		/// The next page.
		/// </value>
		public int NextPage { get; protected set; }

		/// <summary>
		/// Indicates if there are pages.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has pages; otherwise, <c>false</c>.
		/// </value>
		public bool HasPages { get; protected set; }

		/// <summary>
		/// Gets the from items (e.g. _from_ - 3 of 3 items).
		/// </summary>
		/// <value>
		/// The from items.
		/// </value>
		public int FromItems { get; protected set; }

		/// <summary>
		/// Gets the to items (e.g. 1 - _to_ of 3 items).
		/// </summary>
		/// <value>
		/// The to items.
		/// </value>
		public int ToItems { get; protected set; }

	}
}
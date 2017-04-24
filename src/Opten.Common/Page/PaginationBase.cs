using System;
using System.Collections;
using System.Collections.Generic;

namespace Opten.Common.Page
{
	/// <summary>
	/// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="Opten.Common.Page.PaginationMetadata" />
	/// <seealso cref="Opten.Common.Page.IPagination{T}" />
	public abstract class PaginationBase<T> : PaginationMetadata, IPagination<T>
	{

		/// <summary>
		/// The subset of items contained only within this one page of the superset.
		/// </summary>
		protected readonly List<T> Subset = new List<T>();

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginationBase{T}" /> class.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <param name="itemsPerPage">The items per page.</param>
		/// <param name="maxSize">The maximum size.</param>
		/// <param name="totalItemCount">The total item count.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">page;Current page cannot be below 1.
		/// or
		/// itemsPerPage;Items per page cannot be less than 1.
		/// or
		/// maxSize;Max size cannot be less than 1.</exception>
		public PaginationBase(
			int page,
			int itemsPerPage,
			int maxSize,
			int totalItemCount)
		{
			//if (page < 1)
			//	throw new ArgumentOutOfRangeException("page", page, "Current page cannot be below 1.");
			if (itemsPerPage < 1)
				throw new ArgumentOutOfRangeException("itemsPerPage", itemsPerPage, "Items per page cannot be less than 1.");
			if (maxSize < 1)
				throw new ArgumentOutOfRangeException("maxSize", maxSize, "Max size cannot be less than 1.");

			this.Take = itemsPerPage;
			this.MaxSize = maxSize;
			this.TotalItemCount = totalItemCount;

			this.TotalPages = (this.TotalItemCount > 0)
								? (int)Math.Ceiling((double)TotalItemCount / this.Take)
								: 0;

			this.CurrentPage = (page < 0)
									? 1
									: (this.TotalPages > 0 && page >= this.TotalPages)
									   ?
									   this.TotalPages
									   : page;

			this.Skip = GetSkip();

			this.NoPrevious = this.CurrentPage == 1;
			this.PreviousPage = ((this.CurrentPage) - 1) < 1 ? 1 : (this.CurrentPage - 1);
			this.NoNext = this.CurrentPage == this.TotalPages;
			this.NextPage = ((this.CurrentPage) + 1) > this.TotalPages ? this.TotalPages : (this.CurrentPage + 1);
			this.HasPages = this.TotalPages > 1;
			this.Pages = GetPages();

			this.FromItems = (this.Skip + 1);
			this.ToItems = GetToItems();
		}

		#region IPagination<T> Members 

		/// <summary>
		/// Returns an enumerator that iterates through the PaginatioBase&lt;T&gt;.
		/// </summary>
		/// <returns>
		/// A PaginatioBase&lt;T&gt;.Enumerator for the PaginatioBase&lt;T&gt;.
		/// </returns>
		public IEnumerator<T> GetEnumerator()
		{
			return Subset.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the PaginatioBase&lt;T&gt;.
		/// </summary>
		/// <returns>
		/// A PaginatioBase&lt;T&gt;.Enumerator for the PaginatioBase&lt;T&gt;.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Gets the element at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public T this[int index]
		{
			get
			{
				return Subset[index];
			}
		}

		/// <summary>
		/// Gets the number of elements contained on this page.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public virtual int Count
		{
			get { return Subset.Count; }
		}

		/// <summary>
		/// Gets a non-enumerable copy of this query pager.
		/// </summary>
		/// <returns>
		/// A non-enumerable copy of this query pager.
		/// </returns>
		public IPagination GetMetadata()
		{
			return new PaginationMetadata(pager: this);
		}

		#endregion

		#region Private helpers

		private IEnumerable<int> GetPages()
		{
			int startPage = 1, endPage = this.TotalPages;
			bool isMaxSized = this.MaxSize < this.TotalPages;

			List<int> pages = new List<int>();

			// Recompute
			if (isMaxSized)
			{
				// Current page is displayed in the middle of the visible ones
				startPage = (int)Math.Max(this.CurrentPage - Math.Floor(((double)this.MaxSize / 2)), 1);
				endPage = startPage + this.MaxSize - 1;

				// Adjust if limit is exceeded
				if (endPage > this.TotalPages)
				{
					endPage = this.TotalPages;
					startPage = endPage - this.MaxSize + 1;
				}
			}

			// Add page number links
			for (int number = startPage; number <= endPage; number++)
			{
				pages.Add(number);
			}

			return pages;
		}

		private int GetSkip()
		{
			int skip = this.CurrentPage - 1;

			// Check if skip is less than zero or more than amount of pages
			if (skip < 0)
			{
				skip = 0;
			}
			else if (this.TotalPages > 0 && skip >= this.TotalPages)
			{
				skip = (this.TotalPages - 1);
			}

			return skip * this.Take;
		}

		private int GetToItems()
		{
			int to = (this.FromItems + this.Take) - 1;

			if (to < 0)
			{
				to = 0;
			}
			else if (to > this.TotalItemCount)
			{
				to = this.TotalItemCount;
			}

			return to;
		}

		#endregion

	}
}
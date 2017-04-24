using System.Collections.Generic;

namespace Opten.Common.Page
{
	/// <summary>
	/// The Pagination.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPagination<out T> : IPagination, IEnumerable<T>
	{
		// Code copied and improved from:
		// https://github.com/kpi-ua/X.PagedList
		// https://github.com/troygoode/PagedList/
		// https://bitbucket.org/opten/sihf/src/04c50d1d6c3841c60e5ef4e88f3205d7497b573f/Staging/Scripts/angular/directives/display.directive.js?at=master
		// https://bitbucket.org/opten/sihf/src/04c50d1d6c3841c60e5ef4e88f3205d7497b573f/Staging/Partials/Utilities/Pagination.html?at=master

		///<summary> 
		/// Gets the element at the specified index. 
		///</summary> 
		///<param name="index">The zero-based index of the element to get.</param> 
		T this[int index] { get; }

		/// <summary>
		/// Gets the number of elements contained on this page.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		int Count { get; }

		///<summary> 
		/// Gets a non-enumerable copy of this paged list. 
		///</summary> 
		///<returns>A non-enumerable copy of this paged list.</returns> 
		IPagination GetMetadata();

	}

	/// <summary>
	/// The Pagination.
	/// </summary>
	public interface IPagination
	{

		/// <summary>
		/// Gets the current page.
		/// </summary>
		/// <value>
		/// The current page.
		/// </value>
		int CurrentPage { get; }

		/// <summary>
		/// Gets the total pages.
		/// </summary>
		/// <value>
		/// The total pages.
		/// </value>
		int TotalPages { get; }

		/// <summary>
		/// Gets the total item count.
		/// </summary>
		/// <value>
		/// The total item count.
		/// </value>
		int TotalItemCount { get; }

		/// <summary>
		/// Gets the skip.
		/// </summary>
		/// <value>
		/// The skip.
		/// </value>
		int Skip { get; }

		/// <summary>
		/// Gets the take.
		/// </summary>
		/// <value>
		/// The take.
		/// </value>
		int Take { get; }

		/// <summary>
		/// Gets the maximum size.
		/// </summary>
		/// <value>
		/// The maximum size.
		/// </value>
		int MaxSize { get; }

		/// <summary>
		/// Gets the pages.
		/// </summary>
		/// <value>
		/// The pages.
		/// </value>
		IEnumerable<int> Pages { get; }

		/// <summary>
		/// Gets a value indicating whether [no previous].
		/// </summary>
		/// <value>
		///   <c>true</c> if [no previous]; otherwise, <c>false</c>.
		/// </value>
		bool NoPrevious { get; }

		/// <summary>
		/// Gets the previous page.
		/// </summary>
		/// <value>
		/// The previous page.
		/// </value>
		int PreviousPage { get; }

		/// <summary>
		/// Gets a value indicating whether [no next].
		/// </summary>
		/// <value>
		///   <c>true</c> if [no next]; otherwise, <c>false</c>.
		/// </value>
		bool NoNext { get; }

		/// <summary>
		/// Gets the next page.
		/// </summary>
		/// <value>
		/// The next page.
		/// </value>
		int NextPage { get; }

		/// <summary>
		/// Gets a value indicating whether this instance has pages.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has pages; otherwise, <c>false</c>.
		/// </value>
		bool HasPages { get; }

		/// <summary>
		/// Gets the from items (e.g. _from_ - 3 of 3 items).
		/// </summary>
		/// <value>
		/// The from items.
		/// </value>
		int FromItems { get; }

		/// <summary>
		/// Gets the to items (e.g. 1 - _to_ of 3 items).
		/// </summary>
		/// <value>
		/// The to items.
		/// </value>
		int ToItems { get; }

	}

}

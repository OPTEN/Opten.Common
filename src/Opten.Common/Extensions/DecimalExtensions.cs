using System;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Decimal Extensions.
	/// </summary>
	public static class DecimalExtensions
	{

		/// <summary>
		/// Rounds to nearest five centime (20.54 > 20.55 | 20.52 > 20.50).
		/// </summary>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		public static decimal RoundToNearestFive(this decimal price)
		{
			string last;
			decimal newPrice = price.GetPriceAndLastCent(out last);

			switch (last)
			{
				case "1":
					newPrice -= 0.01m;
					break;
				case "2":
					newPrice -= 0.02m;
					break;
				case "3":
					newPrice += 0.02m;
					break;
				case "4":
					newPrice += 0.01m;
					break;
				case "6":
					newPrice -= 0.01m;
					break;
				case "7":
					newPrice -= 0.02m;
					break;
				case "8":
					newPrice += 0.02m;
					break;
				case "9":
					newPrice += 0.01m;
					break;
				default:
					break;

			}

			return newPrice;
		}

		/// <summary>
		/// Rounds up to five centime (20.54 > 20.55 | 20.51 > 20.55).
		/// </summary>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		public static decimal RoundUpToFive(this decimal price)
		{
			string last;
			decimal newPrice = price.GetPriceAndLastCent(out last);

			switch (last)
			{
				case "1":
					newPrice += 0.04m;
					break;
				case "2":
					newPrice += 0.03m;
					break;
				case "3":
					newPrice += 0.02m;
					break;
				case "4":
					newPrice += 0.01m;
					break;
				case "6":
					newPrice += 0.04m;
					break;
				case "7":
					newPrice += 0.03m;
					break;
				case "8":
					newPrice += 0.02m;
					break;
				case "9":
					newPrice += 0.01m;
					break;
				default:
					break;

			}

			return newPrice;
		}

		/// <summary>
		/// Rounds down to five centime (20.54 > 20.50 | 20.52 > 20.50).
		/// </summary>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		public static decimal RoundDownToFive(this decimal price)
		{
			string last;
			decimal newPrice = price.GetPriceAndLastCent(out last);

			switch (last)
			{
				case "1":
					newPrice -= 0.01m;
					break;
				case "2":
					newPrice -= 0.02m;
					break;
				case "3":
					newPrice -= 0.03m;
					break;
				case "4":
					newPrice -= 0.04m;
					break;
				case "6":
					newPrice -= 0.01m;
					break;
				case "7":
					newPrice -= 0.02m;
					break;
				case "8":
					newPrice -= 0.03m;
					break;
				case "9":
					newPrice -= 0.04m;
					break;
				default:
					break;

			}

			return newPrice;
		}

		/// <summary>
		/// Rounds to nearest one franc (20.54 > 21.00 | 20.10 > 20.00).
		/// </summary>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		public static decimal RoundToNearest(this decimal price)
		{
			string last;
			decimal newPrice = price.GetPriceAndLastCent(out last, 2);

			switch (last)
			{
				case "0":
					return (decimal)(int)(newPrice);
				case "5":
				case "6":
				case "7":
				case "8":
				case "9":
					return newPrice.RoundUp();
				default:
					return newPrice.RoundDown();
			}
		}

		/// <summary>
		/// Rounds up to one franc (20.54 > 21.00 | 20.52 > 21.00).
		/// </summary>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		public static decimal RoundUp(this decimal price)
		{
			return Math.Ceiling(price);
		}

		/// <summary>
		/// Rounds down to one franc (20.54 > 20.00 | 20.95 > 20.00).
		/// </summary>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		public static decimal RoundDown(this decimal price)
		{
			return price.RoundUp() - 1;
		}

		private static decimal GetPriceAndLastCent(this decimal price, out string last, int length = 1)
		{
			string strPrice = string.Format("{0:n2}", price);
			last = strPrice.Substring((strPrice.Length - length), 1);

			return Convert.ToDecimal(strPrice);
		}

	}
}
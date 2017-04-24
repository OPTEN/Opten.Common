using NUnit.Framework;
using Opten.Common.Extensions;
using System;

namespace Opten.Common.Test.Extensions
{
	[TestFixture]
	public class RoundingsTests
	{

		[Test]
		public void Round_To_Nearest_Five()
		{
			Assert.AreEqual(80.50, new Decimal(80.50).RoundToNearestFive());
			Assert.AreEqual(80.50, new Decimal(80.504).RoundToNearestFive());
			Assert.AreEqual(80.50, new Decimal(80.51).RoundToNearestFive());
			Assert.AreEqual(80.50, new Decimal(80.52).RoundToNearestFive());
			Assert.AreEqual(80.55, new Decimal(80.53).RoundToNearestFive());
			Assert.AreEqual(80.55, new Decimal(80.54).RoundToNearestFive());
			Assert.AreEqual(80.55, new Decimal(80.55).RoundToNearestFive());
			Assert.AreEqual(80.55, new Decimal(80.56).RoundToNearestFive());
			Assert.AreEqual(80.55, new Decimal(80.57).RoundToNearestFive());
			Assert.AreEqual(80.60, new Decimal(80.58).RoundToNearestFive());
			Assert.AreEqual(80.60, new Decimal(80.59).RoundToNearestFive());
			Assert.AreEqual(80.60, new Decimal(80.60).RoundToNearestFive());
		}

		[Test]
		public void Round_Up_To_Five()
		{
			Assert.AreEqual(80.50, new Decimal(80.50).RoundUpToFive());
			//Assert.AreEqual(80.55, new Decimal(80.504).RoundUpToFive()); //TODO: What is correct here? 80.50 or 80.55? What if 80.501
			Assert.AreEqual(80.55, new Decimal(80.51).RoundUpToFive());
			Assert.AreEqual(80.55, new Decimal(80.52).RoundUpToFive());
			Assert.AreEqual(80.55, new Decimal(80.53).RoundUpToFive());
			Assert.AreEqual(80.55, new Decimal(80.54).RoundUpToFive());
			Assert.AreEqual(80.55, new Decimal(80.55).RoundUpToFive());
			Assert.AreEqual(80.60, new Decimal(80.56).RoundUpToFive());
			Assert.AreEqual(80.60, new Decimal(80.57).RoundUpToFive());
			Assert.AreEqual(80.60, new Decimal(80.58).RoundUpToFive());
			Assert.AreEqual(80.60, new Decimal(80.59).RoundUpToFive());
			Assert.AreEqual(80.60, new Decimal(80.60).RoundUpToFive());

			// Test with multiple roundings
			//TODO: Do that for others
			Assert.AreEqual(80.80, new Decimal(80.77).RoundUpToFive().RoundUpToFive().RoundUpToFive());
		}

		[Test]
		public void Round_Down_To_Five()
		{
			Assert.AreEqual(80.50, new Decimal(80.50).RoundDownToFive());
			Assert.AreEqual(80.50, new Decimal(80.504).RoundDownToFive());
			Assert.AreEqual(80.50, new Decimal(80.51).RoundDownToFive());
			Assert.AreEqual(80.50, new Decimal(80.52).RoundDownToFive());
			Assert.AreEqual(80.50, new Decimal(80.53).RoundDownToFive());
			Assert.AreEqual(80.50, new Decimal(80.54).RoundDownToFive());
			Assert.AreEqual(80.55, new Decimal(80.55).RoundDownToFive());
			Assert.AreEqual(80.55, new Decimal(80.56).RoundDownToFive());
			Assert.AreEqual(80.55, new Decimal(80.57).RoundDownToFive());
			Assert.AreEqual(80.55, new Decimal(80.58).RoundDownToFive());
			Assert.AreEqual(80.55, new Decimal(80.59).RoundDownToFive());
			Assert.AreEqual(80.60, new Decimal(80.60).RoundDownToFive());
		}

		[Test]
		public void Round_To_Nearest()
		{
			Assert.AreEqual(80.00, new Decimal(80.00).RoundToNearest());
			Assert.AreEqual(80.00, new Decimal(80.10).RoundToNearest());
			Assert.AreEqual(80.00, new Decimal(80.12).RoundToNearest());
			Assert.AreEqual(80.00, new Decimal(80.20).RoundToNearest());
			Assert.AreEqual(80.00, new Decimal(80.30).RoundToNearest());
			Assert.AreEqual(80.00, new Decimal(80.40).RoundToNearest());
			Assert.AreEqual(81.00, new Decimal(80.50).RoundToNearest());
			Assert.AreEqual(81.00, new Decimal(80.504).RoundToNearest());
			Assert.AreEqual(81.00, new Decimal(80.55).RoundToNearest());
			Assert.AreEqual(81.00, new Decimal(80.60).RoundToNearest());
			Assert.AreEqual(81.00, new Decimal(80.70).RoundToNearest());
			Assert.AreEqual(81.00, new Decimal(80.80).RoundToNearest());
			Assert.AreEqual(81.00, new Decimal(80.90).RoundToNearest());
			Assert.AreEqual(81.00, new Decimal(80.99).RoundToNearest());
			Assert.AreEqual(81.00, new Decimal(81.00).RoundToNearest());
		}

		[Test]
		public void Round_Up()
		{
			Assert.AreEqual(80.00, new Decimal(80.00).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.50).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.504).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.51).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.52).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.53).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.54).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.55).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.56).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.57).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.58).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.59).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.60).RoundUp());
			Assert.AreEqual(81.00, new Decimal(80.99).RoundUp());
			Assert.AreEqual(81.00, new Decimal(81.00).RoundUp());
		}

		[Test]
		public void Round_Down()
		{
			Assert.AreEqual(79.00, new Decimal(80.00).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.50).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.504).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.51).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.52).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.53).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.54).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.55).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.56).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.57).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.58).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.59).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.60).RoundDown());
			Assert.AreEqual(80.00, new Decimal(80.99).RoundDown());
			Assert.AreEqual(80.00, new Decimal(81.00).RoundDown());
		}

	}
}
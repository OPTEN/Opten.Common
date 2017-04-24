using NUnit.Framework;
using Opten.Common.Extensions;
using System;
using System.Linq.Expressions;

namespace Opten.Common.Test.Extensions
{
	[TestFixture]
	public class ExpressionTests
	{

		[Test]
		public void GetArgumentName()
		{
			Expression<Func<CheckoutModel, AddressModel>> address = m => m.BillingAddress;
			//Expression<Func<CheckoutModel, AddressModel>> address2 = m => m.CheckoutAddress.BillingAddress;

			Assert.That(address.GetArgumentName(), Is.EqualTo("BillingAddress"));
			//TODO: Do we have to test that as well?
			//Assert.That(address2.GetArgumentName(), Is.EqualTo("CheckoutAddress.BillingAddress"));
		}

		private class CheckoutModel
		{
			public AddressModel BillingAddress { get; set; }

			//public CheckoutAddressModel CheckoutAddress { get; set; }
		}

		private class AddressModel
		{

		}

		//private class CheckoutAddressModel
		//{
			//public AddressModel BillingAddress { get; set; }
		//}

	}
}

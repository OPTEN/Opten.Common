using NUnit.Framework;
using Opten.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Common.Test.Extensions
{
	[TestFixture]
	public class EnumerableTests
	{

		[Test]
		public void Shuffle()
		{
			List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

			Assert.AreNotEqual(list, list.Shuffle());
		}

		[Test]
		public void HasValues()
		{
			List<string> list = new List<string> { null, "", null, null, null, null, null, null };
			List<List<string>> list2 = new List<List<string>> { new List<string> { null, null }, new List<string> { "", "" } };

			Assert.That(list.HasValues(), Is.False);
			Assert.That(list2.HasValues(), Is.False);

			list.Add("Test");
			list2.First().Add("Test");

			Assert.That(list.HasValues(), Is.True);
			Assert.That(list2.HasValues(), Is.True);
		}

		[Test]
		public void Distinct()
		{
			List<Car> list = new List<Car> { new Car("1", 1, DateTime.Now), new Car("2", 1, DateTime.Now) };

			Assert.That(list.Count(), Is.EqualTo(list.Distinct().Count()));
			Assert.That(list.Count(), Is.Not.EqualTo(list.Distinct(o => o.Price).Count()));
		}

		public class Car
		{
			public Car(string name, decimal price, DateTime createDate)
			{
				this.Name = name;
				this.Price = price;
				this.CreateDate = createDate;
			}

			public string Name { get; set; }

			public decimal Price { get; set; }

			public DateTime CreateDate { get; set; }
		}

	}
}
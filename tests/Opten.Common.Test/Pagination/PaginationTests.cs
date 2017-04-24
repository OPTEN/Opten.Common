using NUnit.Framework;
using Opten.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Common.Test.Pagination
{
	[TestFixture]
	public class PaginationTests
	{


		[Test]
		public void Can_Convert_To_Paged()
		{
			IEnumerable<Car> cars = new List<Car> { new Car("Audi", 10000, new DateTime(2015, 10, 12)), new Car("BMW", 999, new DateTime(2014, 10, 12)) };

			var paged = cars.ToPagedList(1);

			Assert.That(paged, Is.Not.Null);
			Assert.That(paged.Count, Is.EqualTo(2));
			Assert.That(paged.CurrentPage, Is.EqualTo(1));
			Assert.That(paged.Pages.Count(), Is.EqualTo(1));
		}


		[Test]
		public void Can_Convert_To_Paged_And_Order_By_And_Order_By_Descending()
		{
			IQueryable<Car> cars = new List<Car> { new Car("Audi", 10000, new DateTime(2015, 10, 12)), new Car("BMW", 999, new DateTime(2014, 10, 12)) }.AsQueryable();

			var paged = cars.ToPagedList(o => o.Name, true, 1);

			Assert.That(paged, Is.Not.Null);
			Assert.That(paged.Count, Is.EqualTo(2));
			Assert.That(paged.CurrentPage, Is.EqualTo(1));
			Assert.That(paged.Pages.Count(), Is.EqualTo(1));

			Assert.That(cars.OrderBy(o => o.Name).ToList(), Is.EqualTo(paged.ToList()));
			Assert.That(cars.OrderByDescending(o => o.Name).ToList(), Is.EqualTo(cars.ToPagedList(o => o.Name, false, 1).ToList()));
			Assert.That(cars.OrderBy(o => o.Price).ToList(), Is.EqualTo(cars.ToPagedList(o => o.Price, true, 1).ToList()));
			Assert.That(cars.OrderByDescending(o => o.Price).ToList(), Is.EqualTo(cars.ToPagedList(o => o.Price, false, 1).ToList()));
			Assert.That(cars.OrderBy(o => o.CreateDate).ToList(), Is.EqualTo(cars.ToPagedList(o => o.CreateDate, true, 1).ToList()));
			Assert.That(cars.OrderByDescending(o => o.CreateDate).ToList(), Is.EqualTo(cars.ToPagedList(o => o.CreateDate, false, 1).ToList()));
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
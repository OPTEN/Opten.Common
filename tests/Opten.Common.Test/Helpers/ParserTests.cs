using NUnit.Framework;
using Opten.Common.Parsers;
using System;

namespace Opten.Common.Test.Helpers
{
	[TestFixture]
	public class ParserTests
	{

		//TODO: ISO

		[Test]
		public void Is_SwissDate()
		{
			Assert.AreEqual(true, DateTimeParser.IsSwissDate("01.01.1992"));
			Assert.AreEqual(true, DateTimeParser.IsSwissDate("25.12.1992"));
			Assert.AreEqual(true, DateTimeParser.IsSwissDate("25.12.1992 22:00"));
			Assert.AreEqual(true, DateTimeParser.IsSwissDate("25.12.1992 22:14:00"));

			Assert.AreEqual(false, DateTimeParser.IsSwissDate("12.25.1992"));
			Assert.AreEqual(false, DateTimeParser.IsSwissDate("25/01/1992"));
			Assert.AreEqual(false, DateTimeParser.IsSwissDate(string.Empty));
			Assert.AreEqual(false, DateTimeParser.IsSwissDate(null));
		}

		[Test]
		public void Parse_SwissDate()
		{
			DateTime dt = new DateTime(2015, 01, 25);
			DateTime dt2 = new DateTime(2015, 01, 25, 00, 00, 00);

			Assert.That(dt, Is.EqualTo(DateTimeParser.ParseSwissDateTimeString("25.01.2015")));
			Assert.That(DateTime.MinValue, Is.EqualTo(DateTimeParser.ParseSwissDateTimeString("25/01/2015")));
			Assert.That(dt, Is.Not.EqualTo(DateTimeParser.ParseSwissDateTimeString("01.12.2015")));

			Assert.That(dt2, Is.EqualTo(DateTimeParser.ParseSwissDateTimeString("25.01.2015")));
			Assert.That(dt2, Is.Not.EqualTo(DateTimeParser.ParseSwissDateTimeString("01.12.2015")));
		}

		[Test]
		public void Parse_SwissDateTime()
		{
			DateTime dt = new DateTime(2015, 01, 25, 22, 10, 20);
			DateTime dt2 = new DateTime(2015, 01, 25, 22, 10, 00);

			Assert.That(dt, Is.EqualTo(DateTimeParser.ParseSwissDateTimeString("25.01.2015 22:10:20")));
			Assert.That(DateTime.MinValue, Is.EqualTo(DateTimeParser.ParseSwissDateTimeString("25/01/2015 22:10:20")));
			Assert.That(dt, Is.Not.EqualTo(DateTimeParser.ParseSwissDateTimeString("01.12.2015 22:10:20")));
			
			Assert.That(dt2, Is.EqualTo(DateTimeParser.ParseSwissDateTimeString("25.01.2015 22:10")));
			Assert.That(dt2, Is.Not.EqualTo(DateTimeParser.ParseSwissDateTimeString("01.12.2015 22:10")));
		}

	}
}
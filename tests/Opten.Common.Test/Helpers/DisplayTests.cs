using NUnit.Framework;
using Opten.Common.Helpers;
using System;

namespace Opten.Common.Test.Helpers
{
	[TestFixture]
	public class DisplayTests
	{

		[Test]
		public void AsCentimeters()
		{
			System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-CH");

			Assert.AreEqual("10.5cm", new decimal(10.50).AsCentimeters(true, 1));
			Assert.AreEqual("1'001cm", new decimal(1000.50).AsCentimeters(false, 0));
			Assert.AreEqual("10cm", 10.AsCentimeters(true));
			Assert.AreEqual("1'000cm", 1000.AsCentimeters(false));
		}

		[Test]
		public void AsKilograms()
		{
			System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-CH");

			Assert.AreEqual("10.5kg", new decimal(10.50).AsKilograms(true, 1));
			Assert.AreEqual("1'001kg", new decimal(1000.50).AsKilograms(false, 0));
			Assert.AreEqual("10kg", 10.AsKilograms(true));
			Assert.AreEqual("1'000kg", 1000.AsKilograms(false));
		}

		[Test]
		public void FullName()
		{
			Assert.AreEqual("Calvin Frei", DisplayHelper.FullNameWithFirstNameInFront("Calvin", "Frei"));
			Assert.AreEqual("Frei Calvin", DisplayHelper.FullNameWithLastNameInFront("Calvin", "Frei"));
			Assert.AreEqual("Calvin Frei", DisplayHelper.FullNameWithFirstNameInFront("    Calvin    ", "    Frei"));
			Assert.AreEqual("Frei Calvin", DisplayHelper.FullNameWithLastNameInFront("    Calvin    ", "    Frei"));

			Assert.AreEqual("Frei", DisplayHelper.FullNameWithFirstNameInFront(string.Empty, "Frei"));
			Assert.AreEqual("Frei", DisplayHelper.FullNameWithLastNameInFront(string.Empty, "Frei"));

			Assert.AreEqual("Calvin", DisplayHelper.FullNameWithFirstNameInFront("Calvin", null));
			Assert.AreEqual("Calvin", DisplayHelper.FullNameWithLastNameInFront("Calvin", null));
		}

		[Test]
		public void AsSwissDate()
		{
			DateTime dt = new DateTime(2015, 01, 25, 22, 10, 20);
			DateTime? dtNullable = (DateTime?)dt;
			DateTime birthday = new DateTime(1992, 01, 25, 22, 10, 20);
			DateTime? birthdayNullable = (DateTime?)birthday;
			DateTime? dtNull = null;

			Assert.That("25.01.2015", Is.EqualTo(dt.AsSwissDate()));
			Assert.That("25.01.2015 22:10", Is.EqualTo(dt.AsSwissDateTime()));
			Assert.That("25.01.2015 22:10:20", Is.EqualTo(dt.AsSwissDateTime(includeSeconds: true)));
			Assert.That("Sonntag, 25. Januar 2015", Is.EqualTo(dt.AsSwissDateLong()));
			Assert.That("25.01", Is.EqualTo(dt.AsSwissDateShort()));
			Assert.That("2015-01-25", Is.EqualTo(dt.AsUrl()));
			Assert.That("25.01.1992", Is.EqualTo(birthday.AsSwissBirthday()));
			Assert.That($"25.01.1992 ({birthday.GetAge()})", Is.EqualTo(birthday.AsSwissBirthday(showAge: true)));

			// Nullable
			Assert.That("25.01.2015", Is.EqualTo(dtNullable.AsSwissDate()));
			Assert.That("25.01.2015 22:10", Is.EqualTo(dtNullable.AsSwissDateTime()));
			Assert.That("25.01.2015 22:10:20", Is.EqualTo(dtNullable.AsSwissDateTime(includeSeconds: true)));
			Assert.That("Sonntag, 25. Januar 2015", Is.EqualTo(dtNullable.AsSwissDateLong()));
			Assert.That("25.01", Is.EqualTo(dtNullable.AsSwissDateShort()));
			Assert.That("2015-01-25", Is.EqualTo(dtNullable.AsUrl()));
			Assert.That("25.01.1992", Is.EqualTo(birthdayNullable.AsSwissBirthday()));
			Assert.That($"25.01.1992 ({birthday.GetAge()})", Is.EqualTo(birthdayNullable.AsSwissBirthday(showAge: true)));
			Assert.That(null, Is.EqualTo(dtNull.AsSwissDate()));
		}

		[Test]
		public void AsTime()
		{
			TimeSpan ts = new TimeSpan(22, 00, 11);
			TimeSpan? tsNull = null;
			
			Assert.That("22:00", Is.EqualTo(ts.AsTime()));
			Assert.That("22:00:11", Is.EqualTo(ts.AsTime(ignoreSeconds: false)));
			Assert.That(null, Is.EqualTo(tsNull.AsTime()));
		}

	}
}

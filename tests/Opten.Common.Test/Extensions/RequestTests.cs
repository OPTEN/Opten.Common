using NUnit.Framework;
using Opten.Common.Extensions;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace Opten.Common.Test.Extensions
{
	[TestFixture]
	public class RequestTests
	{

		private HttpRequestBase Request()
		{
			//TODO: Use Mocks!
			HttpRequest request = new HttpRequest("test.html", "http://www.temp.com", "string=test&integer=1&decimal=10.00&date=10/02/2015&bool=true");
			return new HttpRequestWrapper(request);
		}

		[Test]
		public void Get_String()
		{
			Assert.AreEqual(null, Request().Get<string>(key: "notExisting"));
			Assert.AreEqual("test", Request().Get<string>(key: "string"));
			Assert.AreEqual("test", Request().Get<string>(key: "notExisting", defaultValue: "test"));
		}

		[Test]
		public void Get_Integer()
		{
			Assert.AreEqual(0, Request().Get<int>(key: "notExisting"));
			Assert.AreEqual(default(int), Request().Get<int>(key: "notExisting"));
			Assert.AreEqual(1, Request().Get<int>(key: "integer"));
			Assert.AreEqual(1, Request().Get<int>(key: "notExisting", defaultValue: 1));
		}

		[Test]
		public void Get_Integer_Nullable()
		{
			Assert.AreEqual(null, Request().Get<int?>(key: "notExisting"));
			Assert.AreEqual(1, Request().Get<int?>(key: "integer"));
			Assert.AreEqual(1, Request().Get<int?>(key: "notExisting", defaultValue: 1));
		}

		[Test]
		public void Get_Decimal()
		{
			Assert.AreEqual(0, Request().Get<decimal>(key: "notExisting"));
			Assert.AreEqual(default(decimal), Request().Get<decimal>(key: "notExisting"));
			Assert.AreEqual(10.00, Request().Get<decimal>(key: "decimal"));
			Assert.AreEqual(10.00, Request().Get<decimal>(key: "notExisting", defaultValue: 10.00m));
		}

		[Test]
		public void Get_Decimal_Nullable()
		{
			Assert.AreEqual(null, Request().Get<decimal?>(key: "notExisting"));
			Assert.AreEqual(10.00, Request().Get<decimal?>(key: "decimal"));
			Assert.AreEqual(10.00, Request().Get<decimal?>(key: "notExisting", defaultValue: 10.00m));
		}

		[Test]
		public void Get_Date()
		{
			Assert.AreEqual(DateTime.MinValue, Request().Get<DateTime>(key: "notExisting"));
			Assert.AreEqual(default(DateTime), Request().Get<DateTime>(key: "notExisting"));
			Assert.AreEqual(new DateTime(2015, 10, 2), Request().Get<DateTime>(key: "date"));
			Assert.AreEqual(new DateTime(2015, 10, 2), Request().Get<DateTime>(key: "notExisting", defaultValue: new DateTime(2015, 10, 2)));
		}

		[Test]
		public void Get_Date_Nullable()
		{
			Assert.AreEqual(null, Request().Get<DateTime?>(key: "notExisting"));
			Assert.AreEqual(new DateTime(2015, 10, 2), Request().Get<DateTime?>(key: "date"));
			Assert.AreEqual(new DateTime(2015, 10, 2), Request().Get<DateTime?>(key: "notExisting", defaultValue: new DateTime(2015, 10, 2)));
		}

		[Test]
		public void Get_Bool()
		{
			Assert.AreEqual(false, Request().Get<bool>(key: "notExisting"));
			Assert.AreEqual(default(bool), Request().Get<bool>(key: "notExisting"));
			Assert.AreEqual(true, Request().Get<bool>(key: "bool"));
			Assert.AreEqual(true, Request().Get<bool>(key: "notExisting", defaultValue: true));
		}

		[Test]
		public void Get_Bool_Nullable()
		{
			Assert.AreEqual(null, Request().Get<bool?>(key: "notExisting"));
			Assert.AreEqual(true, Request().Get<bool?>(key: "bool"));
			Assert.AreEqual(true, Request().Get<bool?>(key: "notExisting", defaultValue: true));
		}

		[Test]
		public void Accept_Language()
		{
			StringWithQualityHeaderValue[] expected = new StringWithQualityHeaderValue[] {
				new StringWithQualityHeaderValue("de-CH"),
				new StringWithQualityHeaderValue("de"),
				new StringWithQualityHeaderValue("en", 0.5),
				new StringWithQualityHeaderValue("fr", 0.2)
			};

			Assert.AreEqual(expected.OrderBy(o => o.Quality), "de-CH, de, en;q=0.5, fr;q=0.2".ToAcceptLanguage().OrderBy(o => o.Quality));
		}		

		[Test]
		public void Accept_Language_From_System_Culture()
		{
			//TODO: test null or empty list?

			CultureInfo[] cultures = new CultureInfo[] { new CultureInfo("de-CH"), new CultureInfo("fr-CH") };

			StringWithQualityHeaderValue[] header = new StringWithQualityHeaderValue[] {
				new StringWithQualityHeaderValue("fr", 1.0),
				new StringWithQualityHeaderValue("de", 0.5)
			};

			Assert.AreEqual("fr", header.TryGetCultureFromAcceptLanguage(cultures).TwoLetterISOLanguageName);
		}

		[Test]
		public void Accept_Language_From_System_Culture_FireFox()
		{
			//TODO: test null or empty list?

			CultureInfo[] cultures = new CultureInfo[] { new CultureInfo("de-CH"), new CultureInfo("fr-CH") };

			StringWithQualityHeaderValue[] header = new StringWithQualityHeaderValue[] {
				new StringWithQualityHeaderValue("fr"),
				new StringWithQualityHeaderValue("de", 0.5)
			};

			Assert.AreEqual("fr", header.TryGetCultureFromAcceptLanguage(cultures).TwoLetterISOLanguageName);
			Assert.AreEqual("fr", "fr, de;q=0.5".ToAcceptLanguage().TryGetCultureFromAcceptLanguage(cultures).TwoLetterISOLanguageName);
			Assert.AreEqual("fr", "de;q=0.5, fr".ToAcceptLanguage().TryGetCultureFromAcceptLanguage(cultures).TwoLetterISOLanguageName);
		}

	}
}
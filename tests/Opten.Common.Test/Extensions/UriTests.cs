using NUnit.Framework;
using Opten.Common.Extensions;
using System;

namespace Opten.Common.Test.Extensions
{
	[TestFixture]
	public class UriTests
	{

		private readonly Uri uri = new Uri("http://www.opten.ch?test=1&test2=2");
		private readonly Uri uriUtf8 = new Uri("http://www.opten.ch?searchTerm=Häuschen+am+Meer&category=Ölfabrik");

		[Test]
		public void Get_Url_Without_Query()
		{
			Assert.AreEqual("http://www.opten.ch/", uri.GetUrl(withQuery: false, withDomain: true));
			Assert.AreEqual("/", uri.GetUrl(withQuery: false, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query()
		{
			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2", uri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?test=1&test2=2", uri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_With_Utf8()
		{
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik", uriUtf8.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik", uriUtf8.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_Without_Query_And_Add_Params()
		{
			Uri newUri = uri.AddQueryParam("test3", "3");
			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: false, withDomain: true));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: false, withDomain: false));
		}

		[Test]
		public void Get_Url_Without_Query_And_Add_Params_With_Utf8()
		{
			Uri newUri = uriUtf8.AddQueryParam("test3", "3");
			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: false, withDomain: true));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: false, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Add_Params()
		{
			Uri newUri = uri.AddQueryParam("test3", "3");
			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2&test3=3", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?test=1&test2=2&test3=3", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Add_Params_With_Utf8()
		{
			Uri newUri = uriUtf8.AddQueryParam("test3", "3");
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_Without_Query_And_Remove_First_Param()
		{
			Uri newUri = uri.RemoveQueryParam("test1");
			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: false, withDomain: true));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: false, withDomain: false));
		}

		[Test]
		public void Get_Url_Without_Query_And_Remove_First_Param_With_Utf8()
		{
			Uri newUri = uriUtf8.RemoveQueryParam("searchTerm");
			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: false, withDomain: true));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: false, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_First_Param()
		{
			Uri newUri = uri.RemoveQueryParam("test");
			Assert.AreEqual("http://www.opten.ch/?test2=2", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?test2=2", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_First_Param_With_Utf8()
		{
			Uri newUri = uriUtf8.RemoveQueryParam("searchTerm");
			Assert.AreEqual("http://www.opten.ch/?category=Ölfabrik", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?category=Ölfabrik", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_Second_Param()
		{
			Uri newUri = uri.RemoveQueryParam("test2");
			Assert.AreEqual("http://www.opten.ch/?test=1", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?test=1", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_Second_Param_With_Utf8()
		{
			Uri newUri = uriUtf8.RemoveQueryParam("category");
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_NonExisting_Param()
		{
			Uri newUri = uri.RemoveQueryParam("test3");
			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?test=1&test2=2", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_NonExisting_Param_With_Utf8()
		{
			Uri newUri = uriUtf8.RemoveQueryParam("test3");
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_All_Params()
		{
			Uri newUri = uri.RemoveQueryParam("test").RemoveQueryParam("test2");
			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_All_Params_With_Utf8()
		{
			Uri newUri = uriUtf8.RemoveQueryParam("searchTerm").RemoveQueryParam("category");
			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void ASP_Net_HttpErrors_Custom_404_Umbraco_Tread_PHP_As_Normal_Error()
		{
			Uri error = new Uri("http://www.kurtmerkijr.com/1000.aspx?404;http://www.kurtmerkijr.com:80/error-404/?");
			Assert.AreEqual("http://www.kurtmerkijr.com/1000.aspx?404;http://www.kurtmerkijr.com:80/error-404/?", error.RemoveQueryParam("switch").GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/1000.aspx?404;http://www.kurtmerkijr.com:80/error-404/?", error.RemoveQueryParam("switch").GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_First_Param()
		{
			Uri newUri = uri.UpdateQueryParam("test", "test");
			Assert.AreEqual("http://www.opten.ch/?test2=2&test=test", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?test2=2&test=test", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_First_Param_With_Utf8()
		{
			Uri newUri = uriUtf8.UpdateQueryParam("searchTerm", "Häuschen");
			Assert.AreEqual("http://www.opten.ch/?category=Ölfabrik&searchTerm=Häuschen", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?category=Ölfabrik&searchTerm=Häuschen", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_First_Param_Ignore_Case()
		{
			Uri newUri = uri.UpdateQueryParam("TEST", "test");
			Assert.AreEqual("http://www.opten.ch/?test2=2&TEST=test", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?test2=2&TEST=test", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_First_Param_Ignore_Case_With_Utf8()
		{
			Uri newUri = uriUtf8.UpdateQueryParam("CATEGORY", "ÖLFABRIK");
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_NonExisting_Param()
		{
			Uri newUri = uri.UpdateQueryParam("test4", "test4");
			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2&test4=test4", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?test=1&test2=2&test4=test4", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_NonExisting_Param_With_Utf8()
		{
			Uri newUri = uriUtf8.UpdateQueryParam("test4", "äöü");
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü", newUri.GetUrl(withQuery: true, withDomain: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü", newUri.GetUrl(withQuery: true, withDomain: false));
		}

		[Test]
		public void Is_Client_Side_Asp_Net_Request()
		{
			Assert.That(new Uri("http://www.opten.ch/").IsClientSideAspNetRequest(), Is.EqualTo(true));
			Assert.That(new Uri("http://www.opten.ch/de").IsClientSideAspNetRequest(), Is.EqualTo(true));
			Assert.That(new Uri("http://www.opten.ch/de.aspx").IsClientSideAspNetRequest(), Is.EqualTo(true));
			Assert.That(new Uri("http://www.opten.ch/de/about-us.ashx").IsClientSideAspNetRequest(), Is.EqualTo(true));
			Assert.That(new Uri("http://www.opten.ch/Home/Index").IsClientSideAspNetRequest(), Is.EqualTo(true));
			Assert.That(new Uri("http://www.opten.ch/test.svc").IsClientSideAspNetRequest(), Is.EqualTo(true));

			Assert.That(new Uri("http://www.opten.ch/test.css").IsClientSideAspNetRequest(), Is.EqualTo(false));
			Assert.That(new Uri("http://www.opten.ch/test.js").IsClientSideAspNetRequest(), Is.EqualTo(false));
			Assert.That(new Uri("http://www.opten.ch/test.jpeg").IsClientSideAspNetRequest(), Is.EqualTo(false));
			Assert.That(new Uri("http://www.opten.ch/test.mp4").IsClientSideAspNetRequest(), Is.EqualTo(false));
		}

	}
}
using NUnit.Framework;
using Opten.Common.Extensions;
using System;

namespace Opten.Common.Test.Extensions
{
	[TestFixture]
	public class UriTests
	{

		private readonly Uri uriAbsolute = new Uri("http://www.opten.ch?test=1&test2=2#fragment");
		private readonly Uri uriAbsoluteUtf8 = new Uri("http://www.opten.ch?searchTerm=Häuschen+am+Meer&category=Ölfabrik#fragment");
		private readonly Uri uriRelative = new Uri("/?test=1&test2=2#fragment", UriKind.Relative);
		private readonly Uri uriRelativeUtf8 = new Uri("/?searchTerm=Häuschen+am+Meer&category=Ölfabrik#fragment", UriKind.Relative);

		[Test]
		public void Get_Url_Without_Query()
		{
			Assert.AreEqual("http://www.opten.ch/", uriAbsolute.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", uriAbsolute.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("/", uriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", uriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/#fragment", uriAbsolute.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", uriAbsolute.GetUrl(withQuery: false, withDomain: false, withFragment: true));
			Assert.AreEqual("/#fragment", uriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", uriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query()
		{
			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2", uriAbsolute.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2", uriAbsolute.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2", uriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2", uriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2#fragment", uriAbsolute.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test=1&test2=2#fragment", uriAbsolute.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?test=1&test2=2#fragment", uriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test=1&test2=2#fragment", uriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_With_Utf8()
		{
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik", uriAbsoluteUtf8.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik", uriAbsoluteUtf8.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik", uriRelativeUtf8.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik", uriRelativeUtf8.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik#fragment", uriAbsoluteUtf8.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik#fragment", uriAbsoluteUtf8.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik#fragment", uriRelativeUtf8.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik#fragment", uriRelativeUtf8.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_Without_Query_And_Add_Params()
		{
			Uri newUri = uriAbsolute.AddQueryParam("test3", "3");
			Uri newUriRelative = uriRelative.AddQueryParam("test3", "3");

			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/#fragment", newUri.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUri.GetUrl(withQuery: false, withDomain: false, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_Without_Query_And_Add_Params_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.AddQueryParam("test3", "3");
			Uri newUriRelative = uriRelativeUtf8.AddQueryParam("test3", "3");

			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/#fragment", newUri.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUri.GetUrl(withQuery: false, withDomain: false, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Add_Params()
		{
			Uri newUri = uriAbsolute.AddQueryParam("test3", "3");
			Uri newUriRelative = uriRelative.AddQueryParam("test3", "3");
			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2&test3=3", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2&test3=3", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2&test3=3", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2&test3=3", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Add_Params_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.AddQueryParam("test3", "3");
			Uri newUriRelative = uriRelativeUtf8.AddQueryParam("test3", "3");

			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test3=3#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_Without_Query_And_Remove_First_Param()
		{
			Uri newUri = uriAbsolute.RemoveQueryParam("test1");
			Uri newUriRelative = uriRelative.RemoveQueryParam("test1");

			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/#fragment", newUri.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUri.GetUrl(withQuery: false, withDomain: false, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_Without_Query_And_Remove_First_Param_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.RemoveQueryParam("searchTerm");
			Uri newUriRelative = uriRelativeUtf8.RemoveQueryParam("searchTerm");

			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/#fragment", newUri.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUri.GetUrl(withQuery: false, withDomain: false, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: false, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: false, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_First_Param()
		{
			Uri newUri = uriAbsolute.RemoveQueryParam("test");
			Uri newUriRelative = uriRelative.RemoveQueryParam("test");

			Assert.AreEqual("http://www.opten.ch/?test2=2", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test2=2", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?test2=2", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test2=2", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?test2=2#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test2=2#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?test2=2#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test2=2#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_First_Param_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.RemoveQueryParam("searchTerm");
			Uri newUriRelative = uriRelativeUtf8.RemoveQueryParam("searchTerm");

			Assert.AreEqual("http://www.opten.ch/?category=Ölfabrik", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?category=Ölfabrik", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?category=Ölfabrik", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?category=Ölfabrik", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?category=Ölfabrik#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?category=Ölfabrik#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?category=Ölfabrik#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?category=Ölfabrik#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_Second_Param()
		{
			Uri newUri = uriAbsolute.RemoveQueryParam("test2");
			Uri newUriRelative = uriRelative.RemoveQueryParam("test2");

			Assert.AreEqual("http://www.opten.ch/?test=1", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?test=1", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?test=1#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test=1#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?test=1#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test=1#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_Second_Param_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.RemoveQueryParam("category");
			Uri newUriRelative = uriRelativeUtf8.RemoveQueryParam("category");

			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_NonExisting_Param()
		{
			Uri newUri = uriAbsolute.RemoveQueryParam("test3");
			Uri newUriRelative = uriRelative.RemoveQueryParam("test3");

			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test=1&test2=2#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?test=1&test2=2#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test=1&test2=2#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_NonExisting_Param_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.RemoveQueryParam("test3");
			Uri newUriRelative = uriRelativeUtf8.RemoveQueryParam("test3");

			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_All_Params()
		{
			Uri newUri = uriAbsolute.RemoveQueryParam("test").RemoveQueryParam("test2");
			Uri newUriRelative = uriRelative.RemoveQueryParam("test").RemoveQueryParam("test2");

			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Remove_All_Params_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.RemoveQueryParam("searchTerm").RemoveQueryParam("category");
			Uri newUriRelative = uriRelativeUtf8.RemoveQueryParam("searchTerm").RemoveQueryParam("category");

			Assert.AreEqual("http://www.opten.ch/", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void ASP_Net_HttpErrors_Custom_404_Umbraco_Tread_PHP_As_Normal_Error()
		{
			Uri error = new Uri("http://www.kurtmerkijr.com/1000.aspx?404;http://www.kurtmerkijr.com:80/error-404/?");
			Uri errorRelative = new Uri("/1000.aspx?404;http://www.kurtmerkijr.com:80/error-404/?", UriKind.Relative);

			Assert.AreEqual("http://www.kurtmerkijr.com/1000.aspx?404;http://www.kurtmerkijr.com:80/error-404/?", error.RemoveQueryParam("switch").GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/1000.aspx?404;http://www.kurtmerkijr.com:80/error-404/?", error.RemoveQueryParam("switch").GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/1000.aspx?404;http://www.kurtmerkijr.com:80/error-404/?", errorRelative.RemoveQueryParam("switch").GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/1000.aspx?404;http://www.kurtmerkijr.com:80/error-404/?", errorRelative.RemoveQueryParam("switch").GetUrl(withQuery: true, withDomain: false, withFragment: false));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_First_Param()
		{
			Uri newUri = uriAbsolute.UpdateQueryParam("test", "test");
			Uri newUriRelative = uriRelative.UpdateQueryParam("test", "test");

			Assert.AreEqual("http://www.opten.ch/?test2=2&test=test", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test2=2&test=test", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?test2=2&test=test", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test2=2&test=test", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?test2=2&test=test#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test2=2&test=test#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?test2=2&test=test#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test2=2&test=test#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_First_Param_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.UpdateQueryParam("searchTerm", "Häuschen");
			Uri newUriRelative = uriRelativeUtf8.UpdateQueryParam("searchTerm", "Häuschen");

			Assert.AreEqual("http://www.opten.ch/?category=Ölfabrik&searchTerm=Häuschen", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?category=Ölfabrik&searchTerm=Häuschen", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?category=Ölfabrik&searchTerm=Häuschen", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?category=Ölfabrik&searchTerm=Häuschen", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?category=Ölfabrik&searchTerm=Häuschen#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?category=Ölfabrik&searchTerm=Häuschen#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?category=Ölfabrik&searchTerm=Häuschen#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?category=Ölfabrik&searchTerm=Häuschen#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_First_Param_Ignore_Case()
		{
			Uri newUri = uriAbsolute.UpdateQueryParam("TEST", "test");
			Uri newUriRelative = uriRelative.UpdateQueryParam("TEST", "test");

			Assert.AreEqual("http://www.opten.ch/?test2=2&TEST=test", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test2=2&TEST=test", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?test2=2&TEST=test", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test2=2&TEST=test", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?test2=2&TEST=test#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test2=2&TEST=test#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?test2=2&TEST=test#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test2=2&TEST=test#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_First_Param_Ignore_Case_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.UpdateQueryParam("CATEGORY", "ÖLFABRIK");
			Uri newUriRelative = uriRelativeUtf8.UpdateQueryParam("CATEGORY", "ÖLFABRIK");

			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&CATEGORY=ÖLFABRIK#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_NonExisting_Param()
		{
			Uri newUri = uriAbsolute.UpdateQueryParam("test4", "test4");
			Uri newUriRelative = uriRelative.UpdateQueryParam("test4", "test4");

			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2&test4=test4", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2&test4=test4", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2&test4=test4", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?test=1&test2=2&test4=test4", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?test=1&test2=2&test4=test4#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test=1&test2=2&test4=test4#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?test=1&test2=2&test4=test4#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?test=1&test2=2&test4=test4#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Get_Url_With_Query_And_Update_NonExisting_Param_With_Utf8()
		{
			Uri newUri = uriAbsoluteUtf8.UpdateQueryParam("test4", "äöü");
			Uri newUriRelative = uriRelativeUtf8.UpdateQueryParam("test4", "äöü");

			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: false));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: false));
			Assert.AreEqual("http://www.opten.ch/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü#fragment", newUri.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü#fragment", newUri.GetUrl(withQuery: true, withDomain: false, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: true, withFragment: true));
			Assert.AreEqual("/?searchTerm=Häuschen am Meer&category=Ölfabrik&test4=äöü#fragment", newUriRelative.GetUrl(withQuery: true, withDomain: false, withFragment: true));
		}

		[Test]
		public void Is_Client_Side_Asp_Net_Request()
		{
			Assert.That(new Uri("http://www.opten.ch/").IsClientSideAspNetRequest(), Is.True);
			Assert.That(new Uri("http://www.opten.ch/de").IsClientSideAspNetRequest(), Is.True);
			Assert.That(new Uri("http://www.opten.ch/de.aspx").IsClientSideAspNetRequest(), Is.True);
			Assert.That(new Uri("http://www.opten.ch/de/about-us.ashx").IsClientSideAspNetRequest(), Is.True);
			Assert.That(new Uri("http://www.opten.ch/Home/Index").IsClientSideAspNetRequest(), Is.True);
			Assert.That(new Uri("http://www.opten.ch/test.svc").IsClientSideAspNetRequest(), Is.True);

			Assert.That(new Uri("http://www.opten.ch/test.css").IsClientSideAspNetRequest(), Is.False);
			Assert.That(new Uri("http://www.opten.ch/test.js").IsClientSideAspNetRequest(), Is.False);
			Assert.That(new Uri("http://www.opten.ch/test.jpeg").IsClientSideAspNetRequest(), Is.False);
			Assert.That(new Uri("http://www.opten.ch/test.mp4").IsClientSideAspNetRequest(), Is.False);
		}

	}
}
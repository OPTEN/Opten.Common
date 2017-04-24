using NUnit.Framework;
using Opten.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Opten.Common.Test.Helpers
{

	public class DisplayNameTestsModel
	{
		[Display(Name = "Username2")]
		public string Username { get; set; }

		[Display(Name = "Password2")]
		public string Password { get; set; }

		public string PropertyWithOutDisplayName { get; set; }
	}

	[TestFixture]
	public class DisplayAttributeTests
	{

		[Test]
		public void Get_Display_Name()
		{
			var model = new DisplayNameTestsModel();
			var usernameDisplayName = DisplayAttributeHelper.GetPropertyDisplayString<DisplayNameTestsModel>(o => o.Username);
			var passwordDisplayName = DisplayAttributeHelper.GetPropertyDisplayString<DisplayNameTestsModel>(o => o.Password);

			Assert.AreEqual("Username2", usernameDisplayName);
			Assert.AreEqual("Password2", passwordDisplayName);
		}

		[Test]
		public void Get_Display_Name_Without_Attribute()
		{
			var model = new DisplayNameTestsModel();
			var usernameDisplayName = DisplayAttributeHelper.GetPropertyDisplayString<DisplayNameTestsModel>(o => o.PropertyWithOutDisplayName);

			Assert.AreEqual("PropertyWithOutDisplayName", usernameDisplayName);
		}

	}
}
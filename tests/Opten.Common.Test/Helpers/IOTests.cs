using NUnit.Framework;
using Opten.Common.Helpers;

namespace Opten.Common.Test.Helpers
{
	[TestFixture]
	public class IOTests
	{

		[Test]
		public void Get_Path_Name()
		{
			Assert.AreEqual("Path Name", IOHelper.GetPathName("Path Name"));
			Assert.AreEqual("pathname", IOHelper.GetPathName("pathname"));
			Assert.AreEqual("1234", IOHelper.GetPathName("1234"));
			Assert.AreEqual("TEST PATH RELATIVE", IOHelper.GetPathName("TEST\\PATH\\RELATIVE"));
			Assert.AreEqual("TEST$PATH$RELATIVE", IOHelper.GetPathName("TEST\\PATH\\RELATIVE", '$'));
			Assert.AreEqual("TEST TEST", IOHelper.GetPathName("TEST:TEST"));
		}

	}
}

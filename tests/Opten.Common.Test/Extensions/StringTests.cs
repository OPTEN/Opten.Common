using NUnit.Framework;
using Opten.Common.Extensions;
using System;

namespace Opten.Common.Test.Extensions
{
	[TestFixture]
	public class StringTests
	{

		[Test]
		public void Null_Check_Trim()
		{
			string nullString = null;

			Assert.AreEqual(null, nullString.NullCheckTrim());
			Assert.AreEqual(null, nullString.NullCheckTrim(true));
			Assert.AreEqual(string.Empty, " ".NullCheckTrim());
			Assert.AreEqual(null, " ".NullCheckTrim(true));
			Assert.AreEqual("test", " test ".NullCheckTrim());
			Assert.AreEqual("test", " test ".NullCheckTrim(true));

			Assert.AreEqual(true, string.IsNullOrWhiteSpace(nullString.NullCheckTrim()));
			Assert.AreEqual(true, string.IsNullOrEmpty(nullString.NullCheckTrim()));

			Assert.That(nullString.NullCheckTrim(), Is.Null);
			Assert.That("".NullCheckTrim(), Is.Empty);
			Assert.That(nullString.NullCheckTrim(true), Is.Null);
		}

		[Test]
		public void Is_Descending()
		{
			string nullString = null;

			Assert.AreEqual(false, nullString.IsDescending());
			Assert.AreEqual(true, "1".IsDescending());
			Assert.AreEqual(true, "D".IsDescending());
			Assert.AreEqual(true, "d".IsDescending());
			Assert.AreEqual(true, "descending".IsDescending());
			Assert.AreEqual(true, "DESCENDING".IsDescending());
		}

		[Test]
		public void Convert_Comma_Separated_To_String_Array()
		{
			Assert.AreEqual(new string[] { "1", "2", "3", "4" }, "1,2,3,4".ConvertCommaSeparatedToStringArray());
			Assert.AreEqual(new string[] { "1", "2", "4" }, "1,2,,4".ConvertCommaSeparatedToStringArray());

			Assert.AreEqual(new string[] { "1", "2", "3", "4" }, "1,2,3,4".ConvertCommaSeparatedToStringArray(stringSplitOptions: StringSplitOptions.None));
			Assert.AreEqual(new string[] { "1", "2", string.Empty, "4" }, "1,2,,4".ConvertCommaSeparatedToStringArray(stringSplitOptions: StringSplitOptions.None));
		}

		[Test]
		public void Remove_Non_Printig_Chars()
		{
			Assert.AreEqual("Nachwuchsförderung", "Nachwuchs&shy;förderung".RemoveNonPrintigChars());
			Assert.AreEqual("TestTest", "Test&nbsp;Test".RemoveNonPrintigChars());
			Assert.AreEqual("Test & Test", "Test & Test".RemoveNonPrintigChars());
		}

		private class TestClass
		{
			public int Integer { get; set; }

			public string lowerCasePropertyName { get; set; }

			public string UpperCasePropertyName { get; set; }
		}
	}
}
using NUnit.Framework;
using Opten.Common.Formatters;
using Opten.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opten.Common.Test.Formatters
{
	[TestFixture]
	public class PatternReplaceFormatterTests
	{

		[Test]
		public void Can_Format()
		{
			IDictionary<string, IPatternReplacement> patterns = new Dictionary<string, IPatternReplacement>
			{
				{ "{{pattern1}}", new PatternReplacement("Test 1") },
				{ "{{pattern2}}", new PatternReplacement("Test 2") },
				{ "{{pattern3}}", new PatternReplacement("Test 3") }
			};

			PatternReplaceFormatter formatter = new PatternReplaceFormatter(
					patterns: patterns);

			Assert.That("Test 1", Is.EqualTo(formatter.Format(input: "{{pattern1}}")));
			Assert.That("Test 2 Test 3", Is.EqualTo(formatter.Format(input: "{{pattern2}} {{pattern3}}")));
			Assert.That(string.Empty, Is.EqualTo(formatter.Format(input: string.Empty)));
		}

		[Test]
		public void Can_Format_Array()
		{
			IDictionary<string, IPatternReplacement> patterns = new Dictionary<string, IPatternReplacement>
			{
				{ "{{pattern1}}", new PatternReplacement("Test 1") },
				{ "{{pattern2}}", new PatternReplacement("Test 2") },
				{ "{{pattern3}}", new PatternReplacement("Test 3") }
			};

			PatternReplaceFormatter formatter = new PatternReplaceFormatter(
					patterns: patterns);

			string[] nullableArray = null;

			Assert.That(new string[] { "Test 1" }, Is.EqualTo(formatter.Format(input: new string[] { "{{pattern1}}" })));
			Assert.That(new string[] { "Test 2", "Test 3" }, Is.EqualTo(formatter.Format(input: new string[] { "{{pattern2}}", "{{pattern3}}" })));
			Assert.That(new string[0], Is.EqualTo(formatter.Format(input: new string[0])));
			Assert.That(null, Is.EqualTo(formatter.Format(input: nullableArray)));
		}

	}
}

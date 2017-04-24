using Opten.Common.Helpers;

using NUnit.Framework;

namespace Opten.Common.Test.Helpers
{
	[TestFixture]
	public class HtmlTests
	{
		
		[Test]
		public void Replace_Link_In_Text()
		{
			string content = "<p>Das ca. 33'000 Quadratmeter grosse Grundstück beherbergt seitdem eine <a href=\"/de/mieter/\" target=\"_blank\" title=\"Mieter\">vielfältige Nutzung</a>. Die malerische Lage am Aabach, die Neugestaltung der Innenhöfe und die erhöhte Gebäudepräsenz der ehemaligen Fabrikhallen sorgen gleichermassen für geschäftliches Treiben und entspannte Atmosphäre. Die Aussenmöblierung lädt zum Verweilen ein und zeigt so das Innenleben der Gebäude auch unter freiem Himmel.</p>";
			string expected = "<p>Das ca. 33'000 Quadratmeter grosse Grundstück beherbergt seitdem eine <a href=\"javascript:\" title=\"vielfältige Nutzung\" data-href=\"/de/mieter/\" class=\"js-theme\">vielfältige Nutzung</a>. Die malerische Lage am Aabach, die Neugestaltung der Innenhöfe und die erhöhte Gebäudepräsenz der ehemaligen Fabrikhallen sorgen gleichermassen für geschäftliches Treiben und entspannte Atmosphäre. Die Aussenmöblierung lädt zum Verweilen ein und zeigt so das Innenleben der Gebäude auch unter freiem Himmel.</p>";
			string format = "<a href=\"javascript:\" title=\"{4}\" data-href=\"{0}\" class=\"js-theme\">{4}</a>";

			string actual = content.Links(format);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Replace_Links_In_Text()
		{
			string content = "<a href=\"/de/mieter/\" target=\"_blank\" title=\"Mieter\">vielfältige Nutzung</a>.<a href=\"/de/mieter/\" target=\"_blank\" title=\"Mieter\">vielfältige Nutzung</a>.";
			string expected = "<a href=\"javascript:\" title=\"vielfältige Nutzung\" data-href=\"/de/mieter/\" class=\"js-theme\">vielfältige Nutzung</a>.<a href=\"javascript:\" title=\"vielfältige Nutzung\" data-href=\"/de/mieter/\" class=\"js-theme\">vielfältige Nutzung</a>.";
			string format = "<a href=\"javascript:\" title=\"{4}\" data-href=\"{0}\" class=\"js-theme\">{4}</a>";

			string actual = content.Links(format);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Replace_Link_In_Text_Without_Links()
		{
			string content = "<p>Das ca. 33'000 Quadratmeter grosse Grundstück beherbergt seitdem eine vielfältige Nutzung. Die malerische Lage am Aabach, die Neugestaltung der Innenhöfe und die erhöhte Gebäudepräsenz der ehemaligen Fabrikhallen sorgen gleichermassen für geschäftliches Treiben und entspannte Atmosphäre. Die Aussenmöblierung lädt zum Verweilen ein und zeigt so das Innenleben der Gebäude auch unter freiem Himmel.</p>";
			string expected = "<p>Das ca. 33'000 Quadratmeter grosse Grundstück beherbergt seitdem eine vielfältige Nutzung. Die malerische Lage am Aabach, die Neugestaltung der Innenhöfe und die erhöhte Gebäudepräsenz der ehemaligen Fabrikhallen sorgen gleichermassen für geschäftliches Treiben und entspannte Atmosphäre. Die Aussenmöblierung lädt zum Verweilen ein und zeigt so das Innenleben der Gebäude auch unter freiem Himmel.</p>";
			string format = "<a href=\"javascript:\" title=\"{4}\" data-href=\"{0}\" class=\"js-theme\">{4}</a>";

			string actual = content.Links(format);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Replace_Bad_Formatted_Link_In_Text()
		{
			string content = "<A HREF=\"/de/mieter/\" TARGET=\"_blank\" TITLE=\"Mieter\">vielfältige Nutzung</A>.";
			string expected = "<a href=\"javascript:\" title=\"vielfältige Nutzung\" data-href=\"/de/mieter/\" class=\"js-theme\">vielfältige Nutzung</a>.";
			string format = "<a href=\"javascript:\" title=\"{4}\" data-href=\"{0}\" class=\"js-theme\">{4}</a>";

			string actual = content.Links(format);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Replace_Multiple_Links()
		{
			var content = "<p>Als Auftaktprojekt dieser Vision wurde im Jahr 2013 die <a href=\"/\" target=\"_blank\" title=\"www.spinnerei-aathal.ch\"><strong>Spinnerei Aathal</strong></a> – auch unter dem Namen Projekt Leuchtturm bekannt – denkmalschutzgerecht aufgefrischt und den neusten technologischen Standards angepasst. Seit Mai 2014 ist die Spinnerei wieder in Vollbetrieb und wird im Erdgeschoss von diversen Verkaufsgeschäften und einem regionalen Restaurant belebt. Der lauschige Innenhof, welcher zum Aabach und ins Grüne gerichtet ist, lädt zum Verweilen ein. Die Obergeschosse der Spinnerei stellen Büros und Ateliers zur Miete zur Verfügung. Für die <a href=\"/objekte/floos/\" title=\"Floos\"><strong>Spinnerei Floos</strong></a> ist ein privater Gestaltungsplan in Arbeit. Nach der Neupositionierung soll der Standort für Wohnen, Arbeiten, Gastronomie und Einkaufen dienen.</p><p>Auch künftig sollen die industriellen Wurzeln in Szene gesetzt werden und der Charme der ehemaligen Industriebauten erhalten bleiben. Die geplante Mischnutzung wird in die bestehenden Strukturen integriert. Um gleichzeitig mittels Ergänzungsbauten die noch ungenutzten Baulandreserven bespielen zu können, wurde im Frühjahr 2014 ein <a href=\"//staedtebaulicher-wettbewerb/\" target=\"_top\" title=\"Städtebaulicher Wettbewerb\"><strong>städtebaulicher Wettbewerb</strong></a> durchgeführt. Ziel dieses Wettbewerbs war es, die realisierten oder angestossenen Projekte in einen Zusammenhang mit geplanten Neubauten zu bringen. Ein grosser Stellenwert wird dabei der natürlichen Umgebung beigemessen. Diese gesamtheitliche Planung ist auf einen Horizont von rund 20 Jahren ausgerichtet und ermöglicht eine sorgfältig geplante und strukturierte Umnutzung in enger Abstimmung mit den beteiligten Gemeinden und dem Kanton.</p>";
			var expected = "<p>Als Auftaktprojekt dieser Vision wurde im Jahr 2013 die <a href=\"javascript:\" title=\"www.spinnerei-aathal.ch\" data-href=\"/\" target=\"_blank\" class=\"js-theme \"><strong>Spinnerei Aathal</strong></a> – auch unter dem Namen Projekt Leuchtturm bekannt – denkmalschutzgerecht aufgefrischt und den neusten technologischen Standards angepasst. Seit Mai 2014 ist die Spinnerei wieder in Vollbetrieb und wird im Erdgeschoss von diversen Verkaufsgeschäften und einem regionalen Restaurant belebt. Der lauschige Innenhof, welcher zum Aabach und ins Grüne gerichtet ist, lädt zum Verweilen ein. Die Obergeschosse der Spinnerei stellen Büros und Ateliers zur Miete zur Verfügung. Für die <a href=\"javascript:\" title=\"Floos\" data-href=\"/objekte/floos/\" target=\"\" class=\"js-theme \"><strong>Spinnerei Floos</strong></a> ist ein privater Gestaltungsplan in Arbeit. Nach der Neupositionierung soll der Standort für Wohnen, Arbeiten, Gastronomie und Einkaufen dienen.</p><p>Auch künftig sollen die industriellen Wurzeln in Szene gesetzt werden und der Charme der ehemaligen Industriebauten erhalten bleiben. Die geplante Mischnutzung wird in die bestehenden Strukturen integriert. Um gleichzeitig mittels Ergänzungsbauten die noch ungenutzten Baulandreserven bespielen zu können, wurde im Frühjahr 2014 ein <a href=\"javascript:\" title=\"Städtebaulicher Wettbewerb\" data-href=\"//staedtebaulicher-wettbewerb/\" target=\"_top\" class=\"js-theme \"><strong>städtebaulicher Wettbewerb</strong></a> durchgeführt. Ziel dieses Wettbewerbs war es, die realisierten oder angestossenen Projekte in einen Zusammenhang mit geplanten Neubauten zu bringen. Ein grosser Stellenwert wird dabei der natürlichen Umgebung beigemessen. Diese gesamtheitliche Planung ist auf einen Horizont von rund 20 Jahren ausgerichtet und ermöglicht eine sorgfältig geplante und strukturierte Umnutzung in enger Abstimmung mit den beteiligten Gemeinden und dem Kanton.</p>";

			string format = "<a href=\"javascript:\" title=\"{1}\" data-href=\"{0}\" target=\"{2}\" class=\"js-theme {3}\">{4}</a>";

			string actual = content.Links(format);
			Assert.AreEqual(expected, actual);
		}
	}
}

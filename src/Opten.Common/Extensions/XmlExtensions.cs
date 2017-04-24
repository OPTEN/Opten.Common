using System.ComponentModel;
using System.Xml;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The XML Extensions.
	/// </summary>
	public static class XmlExtensions
	{

		/// <summary>
		/// Gets the inner text.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="xpath">The xpath.</param>
		/// <returns></returns>
		public static string GetInnerText(this XmlDocument document, string xpath)
		{
			return document.SelectSingleNode(xpath).InnerText.Trim();
		}

		/// <summary>
		/// Gets the inner XML.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="xpath">The xpath.</param>
		/// <returns></returns>
		public static string GetInnerXml(this XmlDocument document, string xpath)
		{
			return document.SelectSingleNode(xpath).InnerXml;
		}

		/// <summary>
		/// Gets the child nodes.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="xpath">The xpath.</param>
		/// <returns></returns>
		public static XmlNodeList GetChildNodes(this XmlDocument document, string xpath)
		{
			return document.SelectSingleNode(xpath).ChildNodes;
		}

		/// <summary>
		/// Gets the attribute inner text.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="xpath">The xpath.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns></returns>
		public static string GetAttributeInnerText(this XmlDocument document, string xpath, string attributeName)
		{
			string value = document.SelectSingleNode(xpath).GetAttributeValue<string>(attributeName);
			if (string.IsNullOrWhiteSpace(value)) return value;
			return value.Trim();
		}

		/// <summary>
		/// Gets the attribute.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns></returns>
		public static XmlNode GetAttribute(this XmlNode node, string attributeName)
		{
			if (node == null) return null;
			return node.Attributes[attributeName];
		}

		/// <summary>
		/// Gets the attribute value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="node">The node.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns></returns>
		public static T GetAttributeValue<T>(this XmlNode node, string attributeName)
		{
			XmlNode attribute = node.GetAttribute(attributeName);

			if(attribute == null)
			{
				return default(T);
			}

			if(string.IsNullOrWhiteSpace(attribute.InnerText))
			{
				return default(T);
			}
			
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

			if(converter == null)
			{
				return default(T);
			}

			return (T)converter.ConvertFromString(attribute.InnerText.Trim());
		}

	}
}
using System;

namespace Opten.Common.Attributes
{
	/// <summary>
	/// Property to generate .d.ts.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class TypeScriptPropertyAttribute : Attribute
	{

		/// <summary>
		/// Gets the name of the property.
		/// </summary>
		/// <value>
		/// The name of the property.
		/// </value>
		public string PropertyName { get; private set; }

		/// <summary>
		/// Gets the name of the property type.
		/// </summary>
		/// <value>
		/// The name of the property type.
		/// </value>
		public string PropertyTypeName { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this instance is required.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is required; otherwise, <c>false</c>.
		/// </value>
		public bool IsRequired { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeScriptPropertyAttribute"/> class.
		/// </summary>
		/// <param name="propertyTypeName">Name of the property type.</param>
		/// <param name="isRequired">if set to <c>true</c> [is required].</param>
		public TypeScriptPropertyAttribute(string propertyTypeName, bool isRequired = true)
		{
			this.PropertyTypeName = propertyTypeName.Trim();
			this.IsRequired = isRequired;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeScriptPropertyAttribute"/> class.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="propertyTypeName">Name of the property type.</param>
		/// <param name="isRequired">if set to <c>true</c> [is required].</param>
		public TypeScriptPropertyAttribute(string propertyName, string propertyTypeName, bool isRequired = true)
		{
			this.PropertyName = propertyName.Trim();
			this.PropertyTypeName = propertyTypeName.Trim();
			this.IsRequired = isRequired;
		}

	}
}
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Opten.Web.Attributes
{
	/// <summary>
	/// Required if [dependentProperty] has [targetValue]
	/// </summary>
	/// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
	public class RequiredIfAttribute : ValidationAttribute
	{

		private readonly RequiredAttribute _innerAttribute = new RequiredAttribute();
		private readonly string _dependentProperty;
		private readonly object _targetValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequiredIfAttribute"/> class.
		/// </summary>
		/// <param name="dependentProperty">The dependent property.</param>
		/// <param name="targetValue">The target value.</param>
		public RequiredIfAttribute(string dependentProperty, object targetValue)
		{
			this._dependentProperty = dependentProperty;
			this._targetValue = targetValue;
		}

		/// <summary>
		/// Returns true if [targetValue] is equal to [value].
		/// </summary>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context information about the validation operation.</param>
		/// <returns>
		/// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
		/// </returns>
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			PropertyInfo property = validationContext.ObjectType.GetProperty(_dependentProperty);

			if (property == null)
			{
				return new ValidationResult(FormatErrorMessage(_dependentProperty));
			}
			else
			{
				object dependentValue = property.GetValue(validationContext.ObjectInstance, null);

				if ((dependentValue == null && _targetValue == null) || (dependentValue.Equals(_targetValue)))
				{
					if (_innerAttribute.IsValid(value) == false)
					{
						return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
					}
				}

				return ValidationResult.Success;
			}
		}

	}
}
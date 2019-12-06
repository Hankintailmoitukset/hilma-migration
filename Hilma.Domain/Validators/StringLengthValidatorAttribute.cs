using System;
using System.Linq;

namespace Hilma.Domain.Validators
{
    public class StringMaxLengthAttribute 
    {
    public int MaxLength { get; set; }

    public StringMaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }

        //protected override ValidationResult IsValid(
        //    object value, ValidationContext validationContext)
        //{
        //    var isValid = false;
        //    if (value is string)
        //    {
        //        var simpleValue = value as string;
        //        isValid = simpleValue?.Length > MaxLength;
        //    }else if( value is string[] )
        //    {
        //        var multilineValue = value as string[];
        //        isValid = multilineValue?.Sum( l => l.Length) > MaxLength;
        //    }else
        //    {
        //        throw new NotSupportedException($"Value of type {value.GetType()} is not supported in {nameof(StringMaxLengthAttribute)}");
        //    }

        //    if( isValid )
        //    {
        //        return ValidationResult.Success;
        //    }

        //    return new ValidationResult(GetErrorMessage());

        //}

        public string GetErrorMessage()
        {
            return $"String content should not be longer than {MaxLength}.";
        }

    }
}

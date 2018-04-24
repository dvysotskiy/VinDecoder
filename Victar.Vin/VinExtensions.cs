using System;
using System.Collections.Generic;
using System.Text;
using Victar.Vin.Validator;

namespace Victar.Vin
{
    public static class VinExtensions
    {
        private static IVinValidator defaultValidator = new DefaultVinValidator();

        public static  bool IsValidVin(this string value)
        {
            return IsValidVin(value, defaultValidator);
        }

        public static ValidationResult ValidateVin(this string value)
        {
            return ValidateVin(value, defaultValidator);
        }

        public static  bool IsValidVin(this string value, IVinValidator validator)
        {
            return validator.IsValid(value);
        }

        public static ValidationResult ValidateVin(this string value, IVinValidator validator)
        {
            return validator.Validate(value);
        }
    }
}

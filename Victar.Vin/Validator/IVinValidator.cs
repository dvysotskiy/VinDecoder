using System;
using System.Collections.Generic;
using System.Text;

namespace Victar.Vin.Validator
{
    public interface IVinValidator
    {
        ValidationResult Validate(string vin);

        bool IsValid(string vin);
    }
}

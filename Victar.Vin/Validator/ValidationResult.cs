using System;
using System.Collections.Generic;
using System.Text;

namespace Victar.Vin.Validator
{
    public class ValidationResult
    {

        public bool IsValid { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}

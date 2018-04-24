using System;
using Victar.Vin.Decoder;
using Victar.Vin.Validator;

namespace Victar.Vin
{

    public class Vin
    {
        public Vin(IVinValidator validator = null, IVinDecoder decoder = null)
        {
            Validator = validator?? new DefaultVinValidator();
            Decoder = decoder?? new DefaultVinDecoder();
        }

        public string Value { get; set; }

        public IVinDecoder Decoder { get; set; }

        public IVinValidator Validator { get; set; }

        public bool IsValid
        {
            get
            {
                return Validate().IsValid;
            }
        }

        public ValidationResult Validate()
        {
            return Validator.Validate(Value);
        }

    }
}

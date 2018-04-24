using System;
using System.Collections.Generic;
using System.Text;

namespace Victar.Vin.Decoder
{
    public class DecoderResult
    {
        public bool IsValid { get; set; }

        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public string SerialNumber { get; set; }

        public override string ToString()
        {
            return "Is Valid: " + IsValid + ", Year: " + Year + ", Make: " + Make + ", Model: " + Model + ", Serial #: " + SerialNumber + ", Manufacurer: [" + Manufacturer + "]";
        }
    }
}

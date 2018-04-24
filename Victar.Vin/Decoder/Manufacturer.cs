using System;
using System.Collections.Generic;
using System.Text;

namespace Victar.Vin.Decoder
{
    public class Manufacturer
    {
        public string Wmi { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string StateProvince { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public string VehicleType { get; set; }

        public override string ToString()
        {
            return "WMI: " + Wmi + ", Name: " + Name + ", Address: " + Address + ", City: " + City + ", State/Province: " + StateProvince + ", Postal Code: " + PostalCode + ", Phone: " + Phone + ", Vehicle Type: " + VehicleType;
        }
    }
}

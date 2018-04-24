using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Victar.Vin.Validator
{
    /// <summary>
    /// Starting with 1981, valid VIN should be 17 digits long. VIN cannot contain letters I, O and Q and the 9th digit of the VIN must be either a numeric digit (0 through 9) or the letter X.
    /// </summary>
    public class DefaultVinValidator : IVinValidator
    {
        private const int VIN_LENGTH = 17;

        private KeyValuePair<int, string> lengthRule = new KeyValuePair<int, string>(VIN_LENGTH, "VIN must be " + VIN_LENGTH + " characters long");

        private KeyValuePair<string, string> illegalCharRule = new KeyValuePair<string, string>("[A-HJ-NPR-Z0-9]{13}[0-9]{4}", "VIN should be alpha-numeric and cannot contain letters I, O, and Q");

        private Dictionary<char, int> transliterationTable = new Dictionary<char, int>() { { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'E', 5 }, { 'F', 6 }, { 'G', 7 }, { 'H', 8 }, { 'J', 1 }, { 'K', 2 }, { 'L', 3 }, { 'M', 4 }, { 'N', 5 }, { 'P', 7 }, { 'R', 9 }, { 'S', 2 }, { 'T', 3 }, { 'U', 4 }, { 'V', 5 }, { 'W', 6 }, { 'X', 7 }, { 'Y', 8 }, { 'Z', 9 }, { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 } };

        //Weight dictionary is indexed-based
        private Dictionary<int, int> weightTable = new Dictionary<int, int>() { {0, 8}, {1, 7}, {2, 6}, {3, 5}, {4, 4}, {5, 3}, {6, 2}, {7, 10}, {8, 0}, {9, 9}, {10, 8}, {11, 7}, {12, 6}, {13, 5}, {14, 4}, {15, 3}, {16, 2} };


        public bool IsValid(string vin)
        {
            return Validate(vin).IsValid;
        }


        public ValidationResult Validate(string vin)
        {
            var errors = new List<string>();
            vin = string.IsNullOrWhiteSpace(vin) ? string.Empty : vin.Trim().ToUpper();

            if (vin.Length != lengthRule.Key)
            {
                errors.Add(lengthRule.Value);
            }

            if(!PerformChecksumValidation(vin))
            {
                errors.Add("Checksum validation failed");
            }

            return new ValidationResult() { IsValid = !errors.Any(), Errors = errors };
        }

        /// <summary>
        /// US and Canada checksum algorithm
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        private bool PerformChecksumValidation(string vin)
        {
            var chars = vin.ToArray();
            if (chars.Length != VIN_LENGTH)
                return false;
            int sum = 0;
            for(int index = 0; index < chars.Length; index++)
            {
                sum += transliterationTable.GetValueOrDefault(chars[index], 0) * weightTable.GetValueOrDefault(index, 0);
            }
            int remainder = sum % 11;
            return (remainder >= 0 && remainder < 10) ? chars[8].Equals(char.Parse(remainder.ToString())) : char.Equals(chars[8], 'X');
                
        }
    }
}

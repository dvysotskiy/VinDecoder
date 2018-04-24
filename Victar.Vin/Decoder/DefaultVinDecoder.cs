using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Victar.Vin.Validator;

namespace Victar.Vin.Decoder
{
    public class DefaultVinDecoder : IVinDecoder
    {//https://www.nsai.ie/Our-Services-(1)/Certification/1--Automotive-files/4--WMI-VIN.aspx
     //https://vpic.nhtsa.dot.gov/

        public DefaultVinDecoder(string connectionString = null, IVinValidator validator = null)
        {
            ConnectionString = connectionString;
            Validator = validator ?? new DefaultVinValidator();
        }

        public string ConnectionString { get; set; }

        public IVinValidator Validator { get; set; }


        public DecoderResult Decode(string vin)
        {
            if(string.IsNullOrWhiteSpace(ConnectionString))
            {
                throw new InvalidOperationException("Connection string has not been initialized");
            }

            if(string.IsNullOrWhiteSpace(vin))
            {
                throw new ArgumentException("Value cannot be null or empty", "vin");
            }

            vin = vin.Trim().ToUpper();

            if(!Validator.IsValid(vin))
            {
                return new DecoderResult();
            }


            using (var connection = new SqliteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    return new DecoderResult() { IsValid = true, Manufacturer = GetManufacturer(vin, connection).Result, SerialNumber = GetSerialNumber(vin), Year = GetYear(vin, connection).Result };
                }
                finally
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="vin"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private async Task<Manufacturer> GetManufacturer(string vin, SqliteConnection connection)
        {
            Manufacturer manufacturer = null;
            var wmi = vin.Substring(0, 3);
            var command = connection.CreateCommand();
            command.CommandText = "SELECT [Code], [Manufacturer], [Address] ,[City] ,[StateProvince] ,[PostalCode] ,[Country] ,[Phone], [VehicleType] FROM [WMI] WHERE [Code] = @wmi;";
            command.Parameters.AddWithValue("@wmi", wmi);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    manufacturer = new Manufacturer()
                    {
                        Wmi = await reader.IsDBNullAsync(0) ? null : reader.GetString(0),
                        Name = await reader.IsDBNullAsync(1) ? null : reader.GetString(1),
                        Address = await reader.IsDBNullAsync(2)? null : reader.GetString(2),
                        City = await reader.IsDBNullAsync(3) ? null : reader.GetString(3),
                        StateProvince = await reader.IsDBNullAsync(4) ? null : reader.GetString(4),
                        PostalCode = await reader.IsDBNullAsync(5) ? null : reader.GetString(5),
                        Country = await reader.IsDBNullAsync(6) ? null : reader.GetString(6),
                        Phone = await reader.IsDBNullAsync(7) ? null : reader.GetString(7),
                        VehicleType = await reader.IsDBNullAsync(8) ? null : reader.GetString(8)
                    };
                }
            }
            return manufacturer;
        }


        /// <summary>
        /// If the 7th character is numeric, the model year is between 1981 and 2009; otherwise, 2010 and later. In addition to restricted characters, U, Z and 0 (zero) are not permitted in 10th position used to look up vehicle year.
        /// </summary>
        /// <param name="vin">Vehicle identification number</param>
        /// <param name="connection">Open database connection</param>
        /// <returns></returns>
        private async Task<int> GetYear(string vin, SqliteConnection connection)
        {
            int year = 0;
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Year FROM [VEHICLE_YEAR] WHERE [Code] = @code AND [Before2010] = @indicator;";

            Console.WriteLine("Using " + vin[9] + ", before 2010 is " + (char.IsDigit(vin[6]) ? 1 : 0));

            command.Parameters.AddWithValue("@code", vin[9].ToString());
            command.Parameters.AddWithValue("@indicator", char.IsDigit(vin[6])? 1 : 0);
            using (var reader = await command.ExecuteReaderAsync())
            {
                while(await reader.ReadAsync())
                {
                    year = await reader.IsDBNullAsync(0) ? 0 : reader.GetInt32(0);
                }
            }
            return year;
        }

        /// <summary>
        /// Sequential number assigned by the manufacturer is stored in position 12 - 17. Small manufacturers producing less than 500 vehicles annually can be identified by digits in position 12 - 14.
        /// </summary>
        /// <param name="vin">Vehicle identification number</param>
        /// <returns>Vehicle serial number</returns>
        private string GetSerialNumber(string vin)
        {
            return vin.Substring(11, 6);
        }
    }
}

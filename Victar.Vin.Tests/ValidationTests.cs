using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Victar.Vin;

namespace Victar.Vin.Tests
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void TestValid()
        {
            string vin = "1FTRX12V69FA11242";
            var result = vin.ValidateVin();

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void TestInvalid()
        {
            string vin = "1FTRX12V69FA1124";
            var result = vin.ValidateVin();
            Console.WriteLine(string.Join(';', result.Errors));
            Assert.IsFalse(result.IsValid);
        }
    }
}

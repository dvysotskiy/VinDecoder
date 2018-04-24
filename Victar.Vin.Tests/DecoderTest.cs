using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Victar.Vin;
using Victar.Vin.Decoder;

namespace Victar.Vin.Tests
{
    [TestClass]
    public class DecoderTest
    {
        IVinDecoder decoder = new DefaultVinDecoder(@"PATH TO SQLITE DB FILE");


        [TestMethod]
        public void Decode()
        {
            string vin = "1FTRX12V69FA11242";
            var result = decoder.Decode(vin);

            Console.WriteLine(result);
            Assert.IsNotNull(result);
        }
    }
}

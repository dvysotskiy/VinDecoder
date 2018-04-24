using System;
using System.Collections.Generic;
using System.Text;

namespace Victar.Vin.Decoder
{
    public interface IVinDecoder
    {
        DecoderResult Decode(string vin);
    }
}

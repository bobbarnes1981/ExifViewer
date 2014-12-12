using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Data Format Enumeration
    /// </summary>
    public enum DataFormat
    {
        None = 0,
        UnsignedByte = 1, // 1
        AsciiString = 2, // 1
        UnsignedShort = 3, // 2
        UnsignedLong = 4, // 4
        UnsignedRational = 5, // 8
        SignedByte = 6, // 1
        Undefined = 7, // 1
        SignedShort = 8, // 2
        SignedLong = 9, // 4
        SignedRational = 10, // 8
        SingleFloat = 11, // 4
        DoubleFloat = 12  // 8
    }

}

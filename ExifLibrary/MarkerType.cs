using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Marker Type Enumeration
    /// </summary>
    public enum MarkerType
    {
        None = 0xFF00,

        StartOfFrame0 = 0xFFC0,

        DefineHuffmanTables = 0xFFC4,

        StartOfScan = 0xFFDA,
        DefineQuantizationTable = 0xFFDB,

        StartOfImage = 0xFFD8,
        EndOfImage = 0xFFD9,

        App0 = 0xFFE0,
        App1 = 0xFFE1,

        App12 = 0xFFEC,

        App14 = 0xFFEE,

        Comment = 0xFFFE
    }
}

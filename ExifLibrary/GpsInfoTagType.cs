using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Gps Info Tag Type Enumeration
    /// </summary>
    public enum GpsInfoTagType
    {
        GPSVersionID = 0x0000,
        GPSLatitudeRef = 0x0001,
        GPSLatitude = 0x0002,
        GPSLongitudeRef = 0x0003,
        GPSLongitude = 0x0004,
        GPSAltitudeRef = 0x0005,
        GPSAltitude = 0x0006,
        GPSTimeStamp = 0x0007,

        GPSDateStamp = 0x001D
    }
}

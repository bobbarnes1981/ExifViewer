using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Metering Mode Enumeration
    /// </summary>
    public enum MeteringMode
    {
        Unknown = 0,
        Average = 1,
        CenterWeightedAverage = 2,
        Spot = 3,
        MultiSpot = 4, 
        MultiSegment = 5,
        Partial = 6,
        Other = 255
    }
}

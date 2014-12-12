using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Light Source Enumeration
    /// </summary>
    public enum LightSource
    {
        Unknown = 0,
        Daylight = 1,
        Fluorescent = 2,
        TungstenIncandescent = 3,
        Flash = 4,
        FineWeather = 9,
        Cloudy = 10,
        Shade = 11,
        DaylightFluorescent = 12,
        DawyWhiteFluorescent = 13,
        CoolWhiteFluorescent = 14,
        WhiteFluorescent = 15,
        WarmWhiteFluorescent = 16,
        StandardLightA = 17,
        StandardLightB = 18,
        StandardLightC = 19,
        D55 = 20,
        D65 = 21,
        D75 = 22,
        D50 = 23,
        IsoStudioTungsten = 24,
        Other = 255
    }
}

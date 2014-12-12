using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Flash Enumeration
    /// </summary>
    public enum Flash
    {
        NoFlash = 0x0,
        Fired = 0x1,
        FiredReturnNotDetected = 0x5,
        FiredReturnDetected = 0x7,
        OnDidNotFire = 0x8,
        OnFired = 0x9,
        OnReturnNotDetected = 0xD,
        OnReturnDetected = 0xF,
        OffDidNotFire = 0x10,
        OffDidNotFireReturnNotDetected = 0x14,
        AutoDidNotFire = 0x18,
        AutoFired = 0x19,
        AutoFiredReturnNotDetected = 0x1D,
        AutoFiredReturnDetected = 0x1F,
        NoflashFunction = 0x20,
        OffNoFlashFunction = 0x30,
        FiredRedEyeReduction = 0x41,
        FiredRedEyeReductionReturnNotDetected = 0x45,
        FiredRedEyeReductionReturnDetected = 0x47,
        OnRedEyeReduction = 0x49,
        OnRedEyeReductionReturnNotDetected = 0x4D,
        OnRedEyeReductionReturnDetected = 0x4F,
        OffRedEyeReduction = 0x50,
        AutoDidNotFireRedEyeReduction = 0x58,
        AutoFiredRedEyeReduction = 0x59,
        AutoFiredRedEyeReductionReturnNotDetected = 0x5D,
        AutoFiredRedEyeReductionReturnDetected = 0x5F
    }
}

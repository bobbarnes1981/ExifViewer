using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Tiff Tag Type Enumeration
    /// </summary>
    public enum TiffTagType
    {
        InteropIndex = 0x0001,
        InteropVersion = 0x0002,
        ProcessingSoftware = 0x000B,
        SubfileType = 0x00FE,
        OldSubfileType = 0x00FF,
        ImageWidth = 0x0100,
        ImageHeight = 0x0101,
        BitsPerSample = 0x0102,
        Compression = 0x0103,
        PhotometricInterpretation = 0x0106,
        Thresholding = 0x0107,
        CellWidth = 0x0108,
        CellLength = 0x0109,
        FillOrder = 0x010A,
        DocumentName = 0x010D,
        ImageDescription = 0x010E,
        Make = 0x010F,
        Model = 0x0110,
        StripOffsets = 0x0111,
        Orientation = 0x0112,
        SamplesPerPixel = 0x0115,
        RowsPerStrip = 0x0116,
        StripByteCounts = 0x0117,
        MinSampleValue = 0x0118,
        MaxSampleValue = 0x0119,
        XResolution = 0x011A,
        YResolution = 0x011B,
        PlanarConfiguration = 0x011C,
        PageName = 0x011D,
        XPosition = 0x011E,
        YPosition = 0x011F,
        FreeOffsets = 0x0120,
        FreeByteCounts = 0x0121,
        GrayResponseUnit = 0x0122,
        GrayResponseCurve = 0x0123,
        T4Options = 0x0124,
        T6Options = 0x0125,
        ResolutionUnit = 0x0128,
        PageNumber = 0x0129,
        ColorResponseUnit = 0x012C,
        TransferFunction = 0x012D,
        Software = 0x0131,
        ModifyDate = 0x0132,

        ThumbnailOffset = 0x0201,
        ThumbnailLength = 0x0202,

        YCbCrPositioning = 0x0213,

        ExifOffset = 0x8769,

        GPSInfo = 0x8825,

        ISO = 0x8827,

        ExifVersion = 0x9000,

        DateTimeOriginal = 0x9003,
        CreateDate = 0x9004,

        ComponentConfiguration = 0x9101,

        MeteringMode = 0x9207,
        LightSource = 0x9208,
        Flash = 0x9209,
        FocalLength = 0x920A,

        FlashPixVersion = 0xA000,

        ColorSpace = 0xA001,
        ExifImageWidth = 0xA002,
        ExifImageHeight = 0xA003,

        ExifInteroperabilityOffset = 0xA005
    }
}

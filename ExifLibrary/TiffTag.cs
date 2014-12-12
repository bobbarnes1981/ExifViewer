using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Tiff Tag
    /// </summary>
    public class TiffTag : Tag
    {
        /// <summary>
        /// Exif tag Formats
        /// </summary>
        public static readonly Dictionary<TiffTagType, DataFormat> ExifTagFormats = new Dictionary<TiffTagType, DataFormat>
        {
            { TiffTagType.ExifVersion, DataFormat.AsciiString },
            { TiffTagType.ComponentConfiguration, DataFormat.UnsignedByte },
            { TiffTagType.Flash, DataFormat.UnsignedShort },
            { TiffTagType.FlashPixVersion, DataFormat.AsciiString },
            { TiffTagType.ExifInteroperabilityOffset, DataFormat.UnsignedLong },
            { TiffTagType.InteropVersion, DataFormat.AsciiString }
        };

        /// <summary>
        /// Gets Tag Name
        /// </summary>
        public override string TagName
        {
            get { return this.TagType.ToString(); }
        }

        /// <summary>
        /// Gets Tag Id
        /// </summary>
        public override int TagId
        {
            get { return (int)this.TagType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TiffTag"/> class.
        /// </summary>
        /// <param name="tagType"></param>
        /// <param name="dataFormat"></param>
        /// <param name="components"></param>
        /// <param name="bitsPerComponent"></param>
        /// <param name="streamPosition"></param>
        /// <param name="data"></param>
        protected TiffTag(TiffTagType tagType, DataFormat dataFormat, uint components, int bitsPerComponent, long streamPosition, ComponentCollection data)
            : base(dataFormat, components, bitsPerComponent, streamPosition, data)
        {
            this.TagType = tagType;
            this.SubTags = null;
        }

        /// <summary>
        /// Gets Tiff Tag Type
        /// </summary>
        public TiffTagType TagType { get; private set; }

        /// <summary>
        /// Gets Sub Tag Collection
        /// </summary>
        public TagCollection SubTags { get; private set; }

        /// <summary>
        /// Set Sub Tags
        /// </summary>
        /// <param name="tags"></param>
        public void SetSubTags(TagCollection tags)
        {
            this.SubTags = tags;
        }

        /// <summary>
        /// Read Tiff Tags
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static TiffTag[] ReadTiffTags(FileReader fileReader, long start)
        {
            int entries = fileReader.ReadUnsignedShort();

            TiffTag[] tags = new TiffTag[entries];

            for (int i = 0; i < entries; i++)
            {
                tags[i] = TiffTag.ReadTiffTag(fileReader, start);
            }

            return tags;
        }

        /// <summary>
        /// Read Tiff Tag
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static TiffTag ReadTiffTag(FileReader fileReader, long start)
        {
            TiffTagType tagType = (TiffTagType)fileReader.ReadUnsignedShort();
            TiffTag t;

            switch (tagType)
            {
                case TiffTagType.YCbCrPositioning:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);
                    t.Data.Items[0] = (YCbCrPositioning)(ushort)t.Data.Items[0];
                    break;
                case TiffTagType.Orientation:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);
                    t.Data.Items[0] = (Orientation)(ushort)t.Data.Items[0];
                    break;
                case TiffTagType.ResolutionUnit:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);
                    t.Data.Items[0] = (ResolutionUnit)(ushort)t.Data.Items[0];
                    break;
                case TiffTagType.Flash:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);
                    t.Data.Items[0] = (Flash)(ushort)t.Data.Items[0];
                    break;
                case TiffTagType.MeteringMode:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);
                    t.Data.Items[0] = (MeteringMode)(ushort)t.Data.Items[0];
                    break;
                case TiffTagType.LightSource:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);
                    t.Data.Items[0] = (LightSource)(ushort)t.Data.Items[0];
                    break;
                case TiffTagType.ExifVersion:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);
                    break;
                case TiffTagType.ExifOffset:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);

                    // return location
                    long backPositionExif = fileReader.Position;

                    // offset stream position
                    fileReader.Position = (uint)t.Data.Items[0] + (uint)start;

                    t.SetSubTags(Exif.ReadExif(fileReader, (uint)start));

                    // reset location
                    fileReader.Position = backPositionExif;
                    break;
                case TiffTagType.ExifInteroperabilityOffset:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);

                    // return location
                    long backPositionInterop = fileReader.Position;

                    // offset stream position
                    fileReader.Position = (uint)t.Data.Items[0] + (uint)start;

                    t.SetSubTags(Exif.ReadExif(fileReader, (uint)start));

                    // reset location
                    fileReader.Position = backPositionInterop;
                    break;
                case TiffTagType.GPSInfo:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, start);

                    // return location
                    long backPositionGps = fileReader.Position;

                    // offset stream position
                    fileReader.Position = (uint)t.Data.Items[0] + (uint)start;

                    t.SetSubTags(GpsInfo.ReadGpsInfo(fileReader, (uint)start));

                    // reset location
                    fileReader.Position = backPositionGps;
                    break;
                default:
                    t = TiffTag.ReadDefaultTag(fileReader, tagType, (uint)start);
                    break;
            }

            return t;
        }

        /// <summary>
        /// Read Default Tag
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="tagType"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        private static TiffTag ReadDefaultTag(FileReader fileReader, TiffTagType tagType, long start)
        {
            // get format (must match tag required format?)
            DataFormat format = (DataFormat)fileReader.ReadUnsignedShort();

            if (format == DataFormat.Undefined)
            {
                format = ExifTagFormats[tagType];
            }

            // get number of components
            uint components = fileReader.ReadUnsignedLong();

            // get bytes per component
            int bpc = ComponentCollection.BytesPerComponent[format];

            // data offset
            long dataOffset = fileReader.Position;

            // return location
            long backPosition = fileReader.Position + 4;

            // too big for data section
            if (components * bpc > 4)
            {
                // read as offset
                dataOffset = fileReader.ReadUnsignedLong() + start;
            }

            // offset stream position
            fileReader.Position = dataOffset;

            // read to buffer
            ComponentCollection obj = ComponentCollection.ReadFormat(fileReader, format, (int)components);

            // reset location
            fileReader.Position = backPosition;

            // return
            return new TiffTag(tagType, format, components, bpc, dataOffset, obj);
        }
    }
}

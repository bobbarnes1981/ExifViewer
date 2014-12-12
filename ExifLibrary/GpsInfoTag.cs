using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Gps Info Tag
    /// </summary>
    public class GpsInfoTag : Tag
    {
        /// <summary>
        /// Gps Tag Formats
        /// </summary>
        public static readonly Dictionary<GpsInfoTagType, DataFormat> GpsTagFormats = new Dictionary<GpsInfoTagType, DataFormat>
        {
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
        /// Initializes a new instance of the <see cref="GpsInfoTag"/> class.
        /// </summary>
        /// <param name="tagType"></param>
        /// <param name="dataFormat"></param>
        /// <param name="components"></param>
        /// <param name="bitsPerComponent"></param>
        /// <param name="streamPosition"></param>
        /// <param name="data"></param>
        public GpsInfoTag(GpsInfoTagType tagType, DataFormat dataFormat, uint components, int bitsPerComponent, long streamPosition, ComponentCollection data)
            : base(dataFormat, components, bitsPerComponent, streamPosition, data)
        {
            this.TagType = tagType;
        }

        /// <summary>
        /// Gets Gps Info Tag Type
        /// </summary>
        public GpsInfoTagType TagType { get; private set; }

        /// <summary>
        /// Read Gps Info Tags
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static GpsInfoTag[] ReadGpsInfoTags(FileReader fileReader, long start)
        {
            int entries = fileReader.ReadUnsignedShort();

            GpsInfoTag[] tags = new GpsInfoTag[entries];

            for (int i = 0; i < entries; i++)
            {
                // read tag
                tags[i] = GpsInfoTag.ReadGpsInfoTag(fileReader, start);
            }

            return tags;
        }

        /// <summary>
        /// Read Gps Info tag
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static GpsInfoTag ReadGpsInfoTag(FileReader fileReader, long start)
        {
            GpsInfoTagType tagType = (GpsInfoTagType)fileReader.ReadUnsignedShort();

            GpsInfoTag t;

            switch(tagType)
            {
                case GpsInfoTagType.GPSAltitudeRef:
                    t = GpsInfoTag.ReadDefaultTag(fileReader, tagType, start);
                    t.Data.Items[0] = (AltitudeRef)(byte)t.Data.Items[0];
                    break;
                default:
                    t = GpsInfoTag.ReadDefaultTag(fileReader, tagType, start);
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
        private static GpsInfoTag ReadDefaultTag(FileReader fileReader, GpsInfoTagType tagType, long start)
        {
            // get format (must match tag required format)
            DataFormat format = (DataFormat)fileReader.ReadUnsignedShort();

            if (format == DataFormat.Undefined)
            {
                format = GpsInfoTag.GpsTagFormats[tagType];
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
            return new GpsInfoTag(tagType, format, components, bpc, dataOffset, obj);
        }
    }

}

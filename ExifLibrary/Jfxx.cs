using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Jfxx
    /// </summary>
    public class Jfxx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Jfxx"/> class.
        /// </summary>
        /// <param name="data"></param>
        protected Jfxx(ThumbnailFormat format, object data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Gets Format
        /// </summary>
        public ThumbnailFormat Format { get; private set; }

        /// <summary>
        /// Gets Data
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// Read Jfxx
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static Jfxx ReadJfxx(FileReader fileReader)
        {
            ThumbnailFormat format = (ThumbnailFormat)fileReader.ReadByte();

            object data;

            switch (format)
            {
                case ThumbnailFormat.Jpeg:
                    data = Jfif.ReadJfif(fileReader);
                    break;
                case ThumbnailFormat.Palettised:
                    data = RgbPaletteImage.ReadRgbPaletteImage(fileReader);
                    break;
                case ThumbnailFormat.Rgb:
                    data = RgbImage.ReadRgbImage(fileReader);
                    break;
                default:
                    throw new Exception(string.Format("Unhandled ThumbnailFormat {0x2}", (byte)format));
            }

            return new Jfxx(format, data);
        }
    }
}

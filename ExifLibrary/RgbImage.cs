using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Rgb Image
    /// </summary>
    public class RgbImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RgbImage"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="data"></param>
        protected RgbImage(byte width, byte height, RgbColour[,] data)
        {
            this.Width = width;
            this.Height = height;
            this.Data = data;
        }

        /// <summary>
        /// Gets Width
        /// </summary>
        public byte Width { get; private set; }

        /// <summary>
        /// Gets Height
        /// </summary>
        public byte Height { get; private set; }

        /// <summary>
        /// Gets Data
        /// </summary>
        public RgbColour[,] Data { get; private set; }

        /// <summary>
        /// Read Rgb Image
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static RgbImage ReadRgbImage(FileReader fileReader)
        {
            byte width = fileReader.ReadByte();
            byte height = fileReader.ReadByte();

            RgbColour[,] data = new RgbColour[width, height];
            for (byte y = 0; y < height; y++)
            {
                for (byte x = 0; x < width; x++)
                {
                    data[x, y] = RgbColour.ReadRgbColour(fileReader);
                }
            }

            return new RgbImage(width, height, data);
        }
    }
}

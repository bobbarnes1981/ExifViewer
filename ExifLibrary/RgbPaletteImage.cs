using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Rgb Palette Image
    /// </summary>
    public class RgbPaletteImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RgbPaletteImage"/> class.
        /// </summary>
        /// <param name="palette"></param>
        /// <param name="data"></param>
        protected RgbPaletteImage(RgbColour[] palette, byte[,] data)
        {
            this.Palette = palette;
            this.Data = data;
        }

        /// <summary>
        /// Gets Palette
        /// </summary>
        public RgbColour[] Palette { get; private set; }

        /// <summary>
        /// Get Data
        /// </summary>
        public byte[,] Data { get; private set; }

        /// <summary>
        /// Read Rgb Palette Image
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static RgbPaletteImage ReadRgbPaletteImage(FileReader fileReader)
        {
            byte width = fileReader.ReadByte();
            byte height = fileReader.ReadByte();
            
            RgbColour[] palette = new RgbColour[256];
            for (int i = 0; i < 256; i++)
            {
                palette[i] = RgbColour.ReadRgbColour(fileReader);
            }

            byte[,] data = new byte[width, height];
            for (byte y = 0; y < height; y++)
            {
                for (byte x = 0; x < width; x++)
                {
                    data[x, y] = fileReader.ReadByte();
                }
            }

            return new RgbPaletteImage(palette, data);
        }
    }
}

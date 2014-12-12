using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Rgb Colour
    /// </summary>
    public class RgbColour
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RgbColour"/> class.
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        protected RgbColour(byte red, byte green, byte blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        /// <summary>
        /// Gets Red
        /// </summary>
        public byte Red { get; private set; }
        
        /// <summary>
        /// Gets Green
        /// </summary>
        public byte Green { get; private set; }

        /// <summary>
        /// Gets Blue
        /// </summary>
        public byte Blue { get; private set; }

        /// <summary>
        /// Read Rgb Colour
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static RgbColour ReadRgbColour(FileReader fileReader)
        {
            return new RgbColour(fileReader.ReadByte(), fileReader.ReadByte(), fileReader.ReadByte());
        }
    }
}

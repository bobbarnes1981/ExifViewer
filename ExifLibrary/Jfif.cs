using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Jfif
    /// </summary>
    public class Jfif
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Jfif"/> class.
        /// </summary>
        /// <param name="versionMaj"></param>
        /// <param name="versionMin"></param>
        /// <param name="densityUnits"></param>
        /// <param name="xDensity"></param>
        /// <param name="yDensity"></param>
        /// <param name="thumbnail"></param>
        protected Jfif(byte versionMaj, byte versionMin, byte densityUnits, ushort xDensity, ushort yDensity, RgbImage thumbnail)
        {
            this.VersionMaj = versionMaj;
            this.VersionMin = versionMin;
            this.DensityUnits = densityUnits;
            this.XDensity = xDensity;
            this.YDensity = yDensity;
            this.Thumbnail = thumbnail;
        }

        /// <summary>
        /// Gets Version Major Number
        /// </summary>
        public byte VersionMaj { get; private set; }

        /// <summary>
        /// Gets Version Minor Number
        /// </summary>
        public byte VersionMin { get; private set; }

        /// <summary>
        /// Gets Density Units
        /// </summary>
        public byte DensityUnits { get; private set; }

        /// <summary>
        /// Gets X Density 
        /// </summary>
        public ushort XDensity { get; private set; }

        /// <summary>
        /// Gets Y Density
        /// </summary>
        public ushort YDensity { get; private set; }
        
        /// <summary>
        /// Gets Thumbnail
        /// </summary>
        public RgbImage Thumbnail { get; private set; }

        /// <summary>
        /// Read Jfif
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static Jfif ReadJfif(FileReader fileReader)
        {
            byte versionMaj = fileReader.ReadByte();
            byte versionMin = fileReader.ReadByte();
            byte densityUnits = fileReader.ReadByte();
            ushort xDensity = fileReader.ReadUnsignedShort();
            ushort yDensity = fileReader.ReadUnsignedShort();
            RgbImage rgbImage = RgbImage.ReadRgbImage(fileReader);
            return new Jfif(versionMaj, versionMin, densityUnits, xDensity, yDensity, rgbImage);
        }
    }
}

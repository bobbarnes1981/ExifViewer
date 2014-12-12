using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Tiff
    /// </summary>
    public class Tiff
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tiff"/> class.
        /// </summary>
        /// <param name="byteAlignment"></param>
        /// <param name="imageFileDirectories"></param>
        protected Tiff(ByteAlignment byteAlignment, ImageFileDirectory[] imageFileDirectories)
        {
            this.ByteAlignment = byteAlignment;
            this.ImageFileDirectories = imageFileDirectories;
        }

        /// <summary>
        /// Gets Byte Alignment
        /// </summary>
        public ByteAlignment ByteAlignment { get; private set; }

        /// <summary>
        /// Gets Image File Directories
        /// </summary>
        public ImageFileDirectory[] ImageFileDirectories { get; private set; }
        
        /// <summary>
        /// Read Tiff
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static Tiff ReadTiff(FileReader fileReader)
        {
            long start = fileReader.Position;

            // read alignment
            ByteAlignment alignment = (ByteAlignment)fileReader.ReadUnsignedShort();

            ByteAlignment oldAlign = fileReader.ByteAlignment;
            fileReader.ByteAlignment = alignment;

            // read tag mark thingy
            int tagMark = fileReader.ReadUnsignedShort();
            if (tagMark != 0x002A)
            {
                throw new Exception("Missing or incorrect tag mark");
            }

            // read Image File Directories
            Tiff tiff = new Tiff(alignment, ImageFileDirectory.ReadIfds(fileReader, start));

            fileReader.ByteAlignment = oldAlign;

            return tiff;
        }
    }
}

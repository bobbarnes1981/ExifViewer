using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Image File Directory
    /// </summary>
    public class ImageFileDirectory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFileDirectory"/> class.
        /// </summary>
        /// <param name="tags"></param>
        protected ImageFileDirectory(TiffTag[] tags)
        {
            this.Tags = tags;
        }

        /// <summary>
        /// Gets Tags
        /// </summary>
        public TiffTag[] Tags { get; private set; }
        
        /// <summary>
        /// Read Image File Directories
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ImageFileDirectory[] ReadIfds(FileReader fileReader, long start)
        {
            List<ImageFileDirectory> ifds = new List<ImageFileDirectory>();

            uint offset;
            // read offset
            while ((offset = fileReader.ReadUnsignedLong()) != 0x00000000)
            {
                // set position
                fileReader.Position = start + offset;

                // read exif/tiff tags
                ifds.Add(ImageFileDirectory.ReadIfd(fileReader, start));
            }

            return ifds.ToArray();
        }

        /// <summary>
        /// Read Image File Directory
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ImageFileDirectory ReadIfd(FileReader fileReader, long start)
        {
            return new ImageFileDirectory(TiffTag.ReadTiffTags(fileReader, start));
        }
    }
}

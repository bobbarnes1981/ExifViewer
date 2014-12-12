using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Exif
    /// </summary>
    public class Exif : TagCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Exif"/> class.
        /// </summary>
        /// <param name="tags"></param>
        protected Exif(TiffTag[] tags)
        {
            this.Tags = tags;
        }

        /// <summary>
        /// Read Exif
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static Exif ReadExif(FileReader fileReader, uint start)
        {
            return new Exif(TiffTag.ReadTiffTags(fileReader, start));
        }
    }
}

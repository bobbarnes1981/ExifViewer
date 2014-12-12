using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Gps Info
    /// </summary>
    public class GpsInfo : TagCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpsInfo"/> class.
        /// </summary>
        /// <param name="tags"></param>
        protected GpsInfo(GpsInfoTag[] tags)
        {
            this.Tags = tags;
        }

        /// <summary>
        /// Read Gps Info
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static GpsInfo ReadGpsInfo(FileReader fileReader, uint start)
        {
            return new GpsInfo(GpsInfoTag.ReadGpsInfoTags(fileReader, start));
        }
    }
}

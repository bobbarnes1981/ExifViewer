using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Jpef File
    /// </summary>
    public class JpegFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JpegFile"/> class.
        /// </summary>
        /// <param name="path"></param>
        public JpegFile(string path)
        {
            this.Path = path;
            FileReader reader = new FileReader(path);

            if ((MarkerType)reader.ReadUnsignedShort() != MarkerType.StartOfImage)
            {
                throw new Exception("Not Jpeg");
            }
            reader.Position = 0;

            this.Markers = Marker.ReadMarkers(reader);
            reader.Close();
        }

        /// <summary>
        /// Gets Path
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets Markers
        /// </summary>
        public Marker[] Markers { get; private set; }
    }
}

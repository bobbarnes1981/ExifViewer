using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Xmp
    /// </summary>
    public class Xmp
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Xmp"/> class.
        /// </summary>
        /// <param name="xmlString"></param>
        protected Xmp(string xmlString)
        {
            this.XmlString = xmlString;
        }

        /// <summary>
        /// Xml String
        /// </summary>
        public string XmlString { get; private set; }

        /// <summary>
        /// Read Xmp
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Xmp ReadXmp(FileReader fileReader, int length)
        {
            return new Xmp(ComponentCollection.ReadComponents<char>(fileReader.ReadAsciiString, length).ToString());
        }
    }
}

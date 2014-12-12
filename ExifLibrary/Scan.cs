using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Scan
    /// </summary>
    public class Scan
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Scan"/> class.
        /// </summary>
        public Scan()
        {

        }

        /// <summary>
        /// Read Scan
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static Scan ReadScan(FileReader fileReader)
        {
            byte components = fileReader.ReadByte();
            if (components > 4 || components < 1)
            {
                throw new Exception(string.Format("Invalid component count {0}", components));
            }

            for (int i = 0; i < components; i++)
            {
                ScanComponentType componentType = (ScanComponentType)fileReader.ReadByte();
                // bit 0-3 AC table, bit 4-7 DC table
                byte huffmanTable = fileReader.ReadByte();
            }

            fileReader.ReadByte();
            fileReader.ReadByte();
            fileReader.ReadByte();

            // read until end?

            //List<byte> bytes = new List<byte>();
            //byte lastByte = 0x00;
            //byte currByte = 0x00;
            //do
            //{
            //    lastByte = currByte;
            //    currByte = (byte)fileReader.ReadByte();
            //    bytes.Add(currByte);
            //} while (lastByte != 0xFF || currByte != 0xD9);

            return new Scan();
        }
    }
}

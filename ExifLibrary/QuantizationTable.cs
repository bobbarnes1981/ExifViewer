using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Quantization Table
    /// </summary>
    public class QuantizationTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuantizationTable"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        public QuantizationTable(byte id, ushort[] data)
        {
            this.Id = id;
            this.Data = data;
        }

        /// <summary>
        /// Gets Id
        /// </summary>
        public byte Id { get; private set; }

        /// <summary>
        /// Gets Data
        /// </summary>
        public ushort[] Data { get; private set; }

        /// <summary>
        /// Read Quantization Table
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static QuantizationTable ReadQuantizationTable(FileReader fileReader)
        {
            byte b = fileReader.ReadByte();

            byte valueSize = (byte)((b & 0xF0) >> 4);

            switch (valueSize)
            {
                case 0x00:
                    break;
                case 0x01:
                    break;
                default:
                    throw new Exception(string.Format("Invalid Quantization Value Size {0x2}", valueSize));
            }

            byte tableId = (byte)(b & 0x0F);

            ushort[] data = new ushort[64];

            // TODO: Jpeg ZigZag order
            for (int i = 0; i < 64; i++)
            {
                data[i] = fileReader.ReadByte();
                if (valueSize == 0x01)
                {
                    data[i] |= (ushort)(fileReader.ReadByte() << 8);
                }
            }

            return new QuantizationTable(tableId, data);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Component Collection
    /// </summary>
    public class ComponentCollection
    {
        /// <summary>
        /// Bytes Per Component
        /// </summary>
        public static readonly Dictionary<DataFormat, int> BytesPerComponent = new Dictionary<DataFormat, int>
        {
            { DataFormat.UnsignedByte, 1},
            { DataFormat.AsciiString, 1},
            { DataFormat.UnsignedShort, 2},
            { DataFormat.UnsignedLong, 4},
            { DataFormat.UnsignedRational, 8},
            { DataFormat.SignedByte, 1},
            { DataFormat.Undefined, 1},
            { DataFormat.SignedShort, 2},
            { DataFormat.SignedLong, 4},
            { DataFormat.SignedRational, 8},
            { DataFormat.SingleFloat, 4},
            { DataFormat.DoubleFloat, 8},
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentCollection"/> class.
        /// </summary>
        /// <param name="length"></param>
        public ComponentCollection(int length)
        {
            this.Items = new object[length];
        }

        /// <summary>
        /// Get or Sets Items
        /// </summary>
        public object[] Items { get; set; }

        /// <summary>
        /// Read Format
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="format"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public static ComponentCollection ReadFormat(FileReader fileReader, DataFormat format, int components)
        {
            switch (format)
            {
                case DataFormat.AsciiString:
                    return ComponentCollection.ReadComponents<char>(fileReader.ReadAsciiString, components);
                case DataFormat.SignedByte:
                    return ComponentCollection.ReadComponents<byte>(fileReader.ReadByte, components);
                case DataFormat.SignedShort:
                    return ComponentCollection.ReadComponents<short>(fileReader.ReadShort, components);
                case DataFormat.SignedLong:
                    return ComponentCollection.ReadComponents<int>(fileReader.ReadLong, components);
                case DataFormat.SignedRational:
                    return ComponentCollection.ReadComponents<Rational>(fileReader.ReadRational, components);
                case DataFormat.UnsignedByte:
                    return ComponentCollection.ReadComponents<byte>(fileReader.ReadUnsignedByte, components);
                case DataFormat.UnsignedShort:
                    return ComponentCollection.ReadComponents<ushort>(fileReader.ReadUnsignedShort, components);
                case DataFormat.UnsignedLong:
                    return ComponentCollection.ReadComponents<uint>(fileReader.ReadUnsignedLong, components);
                case DataFormat.UnsignedRational:
                    return ComponentCollection.ReadComponents<URational>(fileReader.ReadUnsignedRational, components);
                default:
                    throw new Exception(string.Format("Unhandled format {0}", format));
            }
        }

        /// <summary>
        /// Read Components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public static ComponentCollection ReadComponents<T>(Func<T> func, int components)
        {
            ComponentCollection output = new ComponentCollection(components);

            for (int i = 0; i < components; i++)
            {
                output.Items[i] = func();
            }

            return output;
        }

        public override string ToString()
        {
            string output = string.Empty;
            foreach (object item in this.Items)
            {
                output += item.ToString();
            }
            return output;
        }
    }
}

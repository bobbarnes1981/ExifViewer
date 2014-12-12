using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Marker
    /// </summary>
    public class Marker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Marker"/> class.
        /// </summary>
        /// <param name="markerType"></param>
        protected Marker(MarkerType markerType)
        {
            this.MarkerType = markerType;
        }

        /// <summary>
        /// Gets Marker Type
        /// </summary>
        public MarkerType MarkerType { get; protected set; }

        /// <summary>
        /// Gets or Sets Data
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Read Markers
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static Marker[] ReadMarkers(FileReader fileReader)
        {
            List<Marker> markers = new List<Marker>();
            do
            {
                markers.Add(Marker.ReadMarker(fileReader));
            } while (!fileReader.Finished);
            return markers.ToArray();
        }

        /// <summary>
        /// Read Marker
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static Marker ReadMarker(FileReader fileReader)
        {
            MarkerType markerType;

            bool found = false;
            // should this 'seek' for markers?
            do
            {
                markerType = (MarkerType)fileReader.ReadUnsignedShort();
                if ((0xFF00 & (ushort)markerType) == 0xFF00 && markerType != MarkerType.None)
                {
                    found = true;
                }
                else
                {
                    fileReader.Position -= 1;
                }
            } while (!found);

            Marker marker = new Marker(markerType);

            switch (markerType)
            {
                case MarkerType.App0:
                    marker.Data = Marker.ReadApp0(fileReader);
                    break;
                case MarkerType.App1:
                    marker.Data = Marker.ReadApp1(fileReader);
                    break;
                case MarkerType.App12:
                    marker.Data = Marker.ReadApp12(fileReader);
                    break;
                case MarkerType.App14:
                    marker.Data = Marker.ReadApp14(fileReader);
                    break;
                case MarkerType.DefineQuantizationTable:
                    marker.Data = Marker.ReadDefineQuantizationTable(fileReader);
                    break;
                case MarkerType.StartOfFrame0:
                    marker.Data = Marker.ReadStartOfFrame(fileReader);
                    break;
                case MarkerType.DefineHuffmanTables:
                    marker.Data = Marker.ReadDefineHuffmanTables(fileReader);
                    break;
                case MarkerType.StartOfScan:
                    marker.Data = Marker.ReadStartOfScan(fileReader);
                    break;
                case MarkerType.Comment:
                    marker.Data = Marker.ReadComment(fileReader);
                    break;
                case MarkerType.EndOfImage:
                case MarkerType.StartOfImage:
                    // nothing
                    marker.Data = null;
                    break;
                default:
                    throw new Exception(string.Format("Unhandled marker: {0:x4} at {1} (0x{1:x8})", (int)markerType, fileReader.Position));
            }
            return marker;
        }

        /// <summary>
        /// Read String
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string ReadString(FileReader fileReader, int length)
        {
            string data = string.Empty;
            for (int i = 2; i < length; i++)
            {
                data += (char)fileReader.ReadByte();
            }

            return data;
        }

        /// <summary>
        /// Read App0 Marker
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        private static object ReadApp0(FileReader fileReader)
        {
            ushort length = fileReader.ReadUnsignedShort();

            if (ComponentCollection.ReadComponents<char>(fileReader.ReadAsciiString, 5).ToString() == "JFIF\0")
            {
                return Jfif.ReadJfif(fileReader);
            }
            fileReader.Position -= 5;

            if (ComponentCollection.ReadComponents<char>(fileReader.ReadAsciiString, 5).ToString() == "JFXX\0")
            {
                return Jfxx.ReadJfxx(fileReader);
            }
            fileReader.Position -= 5;

            return ReadString(fileReader, length);
        }

        /// <summary>
        /// Read App1 Marker
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        private static object ReadApp1(FileReader fileReader)
        {
            ushort length = fileReader.ReadUnsignedShort();

            if (ComponentCollection.ReadComponents<char>(fileReader.ReadAsciiString, 6).ToString() == "Exif\0\0")
            {
                return Tiff.ReadTiff(fileReader);
            }
            fileReader.Position -= 6;

            if (ComponentCollection.ReadComponents<char>(fileReader.ReadAsciiString, 29).ToString() == "http://ns.adobe.com/xap/1.0/\0")
            {
                return Xmp.ReadXmp(fileReader, length - (29 + 2));
            }
            fileReader.Position -= 29;

            return ReadString(fileReader, length);
        }

        /// <summary>
        /// Read App12 Marker
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        private static object ReadApp12(FileReader fileReader)
        {
            ushort length = fileReader.ReadUnsignedShort();

            if (ComponentCollection.ReadComponents<char>(fileReader.ReadAsciiString, 6).ToString() == "Ducky\0")
            {
                //return Ducky.ReadDucky(fileReader, length - (6 + 2));
            }
            fileReader.Position -= 6;

            return ReadString(fileReader, length);
        }

        /// <summary>
        /// Read App14 Marker
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        private static object ReadApp14(FileReader fileReader)
        {
            ushort length = fileReader.ReadUnsignedShort();

            if (ComponentCollection.ReadComponents<char>(fileReader.ReadAsciiString, 6).ToString() == "Adobe\0")
            {
                // DCT Filters - PS Level 2
                //return ?.Read?(fileReader, length - (6 + 2));
            }
            fileReader.Position -= 6;

            return ReadString(fileReader, length);
        }

        /// <summary>
        /// Read Quantization Table
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        private static object ReadDefineQuantizationTable(FileReader fileReader)
        {
            ushort length = fileReader.ReadUnsignedShort();

            return QuantizationTable.ReadQuantizationTable(fileReader);
        }

        /// <summary>
        /// Read Frame
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        private static object ReadStartOfFrame(FileReader fileReader)
        {
            ushort length = fileReader.ReadUnsignedShort();

            return Frame.ReadFrame(fileReader, length);
        }

        /// <summary>
        /// Read Huffman Tables
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        private static object ReadDefineHuffmanTables(FileReader fileReader)
        {
            ushort length = fileReader.ReadUnsignedShort();

            // TODO: 

            byte[] bytes = new byte[length - 2];

            for (int i = 2; i < length; i++)
            {
                bytes[i - 2] = fileReader.ReadByte();
            }

            return bytes;
        }

        /// <summary>
        /// Read Scan
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        private static object ReadStartOfScan(FileReader fileReader)
        {
            ushort length = fileReader.ReadUnsignedShort();

            return Scan.ReadScan(fileReader);
        }

        /// <summary>
        /// Read Comment
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        private static object ReadComment(FileReader fileReader)
        {
            ushort length = fileReader.ReadUnsignedShort();

            return ComponentCollection.ReadComponents<char>(fileReader.ReadAsciiString, length - 2).ToString();

            //return Comment.ReadComment(fileReader, length);
        }
    }
}

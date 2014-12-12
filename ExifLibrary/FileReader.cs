using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// File Reader
    /// </summary>
    public class FileReader
    {
        /// <summary>
        /// File stream
        /// </summary>
        private FileStream fileStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReader"/> class.
        /// </summary>
        /// <param name="path"></param>
        public FileReader(string path)
        {
            this.fileStream = new FileStream(path, FileMode.Open);
            this.ByteAlignment = ByteAlignment.Motarola;
        }

        /// <summary>
        /// Gets a value indicating whether the stream is at the end
        /// </summary>
        public bool Finished
        {
            get
            {
                return this.fileStream.Position == this.fileStream.Length;
            }
        }

        /// <summary>
        /// Gets or Sets Position
        /// </summary>
        public long Position
        {
            get
            {
                return this.fileStream.Position;
            }

            set
            {
                this.fileStream.Position = value;
            }
        }

        /// <summary>
        /// Gets or Sets Byte Alignment
        /// </summary>
        public ByteAlignment ByteAlignment { get; set; }

        /// <summary>
        /// Close stream
        /// </summary>
        public void Close()
        {
            this.fileStream.Close();
        }

        /// <summary>
        /// Read Byte
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return (byte)fileStream.ReadByte();
        }

        /// <summary>
        /// Read Unsigned Byte
        /// </summary>
        /// <returns></returns>
        public byte ReadUnsignedByte()
        {
            return (byte)fileStream.ReadByte();
        }

        /// <summary>
        /// Read Short
        /// </summary>
        /// <returns></returns>
        public short ReadShort()
        {
            return (short)this.read(2);
        }

        /// <summary>
        /// Read Unsigned Short
        /// </summary>
        /// <returns></returns>
        public ushort ReadUnsignedShort()
        {
            return (ushort)this.read(2);
        }

        /// <summary>
        /// Read Long
        /// </summary>
        /// <returns></returns>
        public int ReadLong()
        {
            return (int)this.read(4);
        }

        /// <summary>
        /// Read Unsigned Long
        /// </summary>
        /// <returns></returns>
        public uint ReadUnsignedLong()
        {
            return (uint)this.read(4);
        }

        /// <summary>
        /// Read Rational
        /// </summary>
        /// <returns></returns>
        public Rational ReadRational()
        {
            int numerator = this.ReadLong();
            int denominator = this.ReadLong();
            return new Rational(numerator, denominator);
        }

        /// <summary>
        /// Read Unsigned Rational
        /// </summary>
        /// <returns></returns>
        public URational ReadUnsignedRational()
        {
            uint numerator = this.ReadUnsignedLong();
            uint denominator = this.ReadUnsignedLong();
            return new URational(numerator, denominator);
        }

        /// <summary>
        /// Read Ascii String
        /// </summary>
        /// <returns></returns>
        public char ReadAsciiString()
        {
            return (char)fileStream.ReadByte();
        }

        /// <summary>
        /// Read Bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private uint read(int bytes)
        {
            if (this.ByteAlignment == ByteAlignment.Motarola)
            {
                return this.readMotarola(bytes);
            }
            else
            {
                return this.readIntel(bytes);
            }
        }

        /// <summary>
        /// Read Bytes in Intel Order (BE)
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private uint readIntel(int bytes)
        {
            uint output = 0x00000000;
            for (int i = 0; i < bytes * 8; i += 8)
            {
                output |= (uint)this.fileStream.ReadByte() << i;
            }
            return output;
        }

        /// <summary>
        /// Read Bytes in Motarola Order (LE)
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private uint readMotarola(int bytes)
        {
            uint output = 0x00000000;
            for (int i = (bytes - 1) * 8; i >= 0; i -= 8)
            {
                output |= (uint)this.fileStream.ReadByte() << i;
            }
            return output;
        }
    }
}

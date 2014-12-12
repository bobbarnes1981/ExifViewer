using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Abstract Tag
    /// </summary>
    public abstract class Tag
    {
        /// <summary>
        /// Gets Tag Name
        /// </summary>
        public abstract string TagName { get; }

        /// <summary>
        /// Gets Tag Id
        /// </summary>
        public abstract int TagId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="dataFormat"></param>
        /// <param name="components"></param>
        /// <param name="bitsPerComponent"></param>
        /// <param name="streamPosition"></param>
        /// <param name="data"></param>
        public Tag(DataFormat dataFormat, uint components, int bitsPerComponent, long streamPosition, ComponentCollection data)
        {
            this.DataFormat = dataFormat;
            this.Components = components;
            this.BitsPerComponent = bitsPerComponent;
            this.StreamPosition = streamPosition;
            this.Data = data;
        }

        /// <summary>
        /// Gets Data Format
        /// </summary>
        public DataFormat DataFormat { get; private set; }

        /// <summary>
        /// Gets Components Count
        /// </summary>
        public uint Components { get; private set; }

        /// <summary>
        /// Gets Bits Per Component
        /// </summary>
        public int BitsPerComponent { get; private set; }

        /// <summary>
        /// Gets Stream Position
        /// </summary>
        public long StreamPosition { get; private set; }

        /// <summary>
        /// Gets Components
        /// </summary>
        public ComponentCollection Data { get; set; }

        /// <summary>
        /// Gets Data String
        /// </summary>
        public string DataString
        {
            get
            {
                string data = string.Empty;

                foreach (object obj in this.Data.Items)
                {
                    data += obj.ToString() + (obj.GetType() == typeof(char)?"":" ");
                }

                return data;
            }
        }
    }
}

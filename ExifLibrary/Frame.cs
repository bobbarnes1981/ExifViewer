using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Frame
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        protected Frame()
        {

        }

        /// <summary>
        /// ReadFrame
        /// </summary>
        /// <param name="fileReader"></param>
        /// <returns></returns>
        public static Frame ReadFrame(FileReader fileReader, ushort length)
        {
            // 1 byte - sample precision 8 or 12
            // 2 bytes - image height
            // 2 bytes - image width
            // 1 byte - number of components

            // component
            // 1 byte - identifier jpeg 0-255 jfif 1-3
            // 1 byte - hi h-sampling, lo v-sampling (1-4)
            // 1byte - quantization table id 0-3

            byte[] bytes = new byte[length - 2];

            for (int i = 2; i < length; i++)
            {
                bytes[i - 2] = fileReader.ReadByte();
            }

            return new Frame();
        }
    }
}

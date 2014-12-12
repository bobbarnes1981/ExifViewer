using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Tag Collection
    /// </summary>
    public abstract class TagCollection
    {
        /// <summary>
        /// Gets Tags
        /// </summary>
        public Tag[] Tags { get; protected set; }
    }
}

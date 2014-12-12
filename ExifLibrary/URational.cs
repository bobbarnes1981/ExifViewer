using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Unsigned Rational
    /// </summary>
    public class URational : RationalNumber<uint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="URational"/> class.
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        public URational(uint numerator, uint denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Signed Rational Number
    /// </summary>
    public class Rational : RationalNumber<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rational"/> class.
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        public Rational(int numerator, int denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }
    }
}

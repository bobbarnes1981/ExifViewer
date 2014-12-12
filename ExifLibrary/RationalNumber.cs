using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifLibrary
{
    /// <summary>
    /// Abstract Rational Number
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RationalNumber<T>
    {
        /// <summary>
        /// Numerator
        /// </summary>
        public T Numerator { get; set; }

        /// <summary>
        /// Denominator
        /// </summary>
        public T Denominator { get; set; }
        
        public override string ToString()
        {
            return string.Format("{0}^{1}", Numerator, Denominator);
        }
    }
}

using System;

namespace WS.Core.Toolbox.Utilities
{
    public static class DateCompareUtilities
    {
        /// <summary>
        /// Compare two dates within a given range expressed in milliseconds.
        /// The dates are compared within a default margin/range of 100 milliseconds.
        /// </summary>
        /// <returns></returns>
        public static bool CompareWithinRange(DateTime one, DateTime two, int milliseconds = 1500)
        {
            var diff = one.Subtract(two).TotalMilliseconds;
            
            if (Math.Abs(diff) < milliseconds)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

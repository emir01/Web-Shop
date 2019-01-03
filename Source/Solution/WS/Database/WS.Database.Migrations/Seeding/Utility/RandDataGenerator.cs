using System;
using System.IO;
using System.Threading;

namespace WS.Database.Bootstrap.Seeding.Utility
{
    /// <summary>
    /// Contains utilities for random data generation for the 
    /// data seeding process
    /// </summary>
    public static class RandDataGenerator
    {

        #region Product

        #region Price Generation

        public static double GetRandomPrice(double minPrice = 9000, double maxPrice = 20000)
        {
            if (minPrice > maxPrice)
            {
                throw new InvalidDataException("Min price cannot be bigger then max price in GetRandomPrice RandDataGenerator Method");
            }

            var randomGen = new Random((int)maxPrice);

            var price = minPrice + (randomGen.NextDouble() * (maxPrice - minPrice));

            return price;
        }

        #endregion

        #endregion

    }
}

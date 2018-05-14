using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.App
{
    public static class Extensions
    {
        /// <summary>
        /// Transforms a float value representing _units_ into its equivalent _meters_ in game.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The amount of meters after conversion</returns>
        public static int ToMeters(this float value)
        {
            return (int)(value * Constants.UNIT_TO_METERS_FACTOR);
        }
    }
}

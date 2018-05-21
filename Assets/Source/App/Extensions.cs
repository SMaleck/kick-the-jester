using System;

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
            return (int)(value / Constants.UNIT_TO_METERS_FACTOR);
        }

        public static int ToMeters(this int value)
        {
            return (int)(value / Constants.UNIT_TO_METERS_FACTOR);
        }

        /// <summary>
        /// Returns the representation of this float in percentage relative to the max value given.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int AsPercent(this float value, float max)
        {
            return (int)((value / max) * 100);
        }

        /// <summary>
        /// Returns the representation of this float in relative percentage relative to the max value given (between 0 and 1)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float AsRelativeTo1(this float value, float max)
        {
            return (value / max);
        }

        /// <summary>
        /// Cast to int using <see cref="Constants.FLOAT_PRECISION_FACTOR"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this float value)
        {
            return (int)(value * Constants.FLOAT_PRECISION_FACTOR);
        }

        /// <summary>
        /// Compares values using their integer representation.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool IsNearlyEqual(this float value1, float value2)
        {
            int val1 = value1.ToInt();
            int val2 = value2.ToInt();

            return val1 == val2;
        }

        /// <summary>
        /// Calculates the absolute difference between to a value using integer precision.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static float Difference(this float value1, float value2)
        {
            int val1 = value1.ToInt();
            int val2 = value2.ToInt();

            int result = Math.Abs(Math.Abs(val1) - Math.Abs(val2));

            return (float)result / Constants.FLOAT_PRECISION_FACTOR;
        }
    }
}

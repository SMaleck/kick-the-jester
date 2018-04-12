using System;

namespace Assets.Source.App
{
    public static class MathUtil
    {
        public static float CappedFloat(float value)
        {
            return (float)Math.Round(value, Constants.FLOAT_PRECISION_DIGITS);            
        }

        public static int FloatToInt(float value)
        {
            return (int)(value * Constants.FLOAT_PRECISION_FACTOR);
        }

        public static float IntToFloat(int value)
        {
            return (float)value / Constants.FLOAT_PRECISION_FACTOR;
        }

        public static float Difference(float value1, float value2)
        {
            int val1 = FloatToInt(value1);
            int val2 = FloatToInt(value2);

            int result = Math.Abs(Math.Abs(val1) - Math.Abs(val2));

            return IntToFloat(result);
        }

        public static bool NearlyEqual(float value1, float value2)
        {
            int val1 = FloatToInt(value1);
            int val2 = FloatToInt(value2);

            return val1 == val2;
        }

        public static int AsPercent(float value, float max)
        {
            return (int)((value / max) * 100);
        }
    }
}

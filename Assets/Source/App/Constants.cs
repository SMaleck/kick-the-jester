namespace Assets.Source.App
{
    public static class Constants
    {
        public const string UMenuRoot = "KTJ/";
        public const string UMenuInstallers = UMenuRoot + "Installer/";

        public const string PREFS_KEY_LANGUAGE = "LanguagePref";

        // FLOAT PRECISION FACTORS
        public const int FLOAT_PRECISION_FACTOR = 100;

        // The Jester is 3x3 units big
        // We assume here that the jester is around 1,5m high
        public const float UNIT_TO_METERS_FACTOR = 2f;
    }
}
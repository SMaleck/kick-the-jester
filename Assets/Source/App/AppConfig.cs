namespace Assets.Source.App
{
    public class AppConfig
    {
        private static AppConfig Self;

        public readonly bool IsDebugEnabled = true;


        private AppConfig() { }

        public static AppConfig Get()
        {
            if (Self == null)
            {
                Self = new AppConfig();
            }

            return Self;
        }
    }
}
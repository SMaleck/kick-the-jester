using UnityEngine;

namespace Assets.Source.App
{
    public static class Logger
    {
        private static string BuildMessage(object payload = null, params object[] args)
        {
            string message = payload?.ToString() ?? "";

            if (args != null && args.Length >= 0)
            {
                message = string.Format(message, args);
            }

            return message;
        }

#if UNITY_EDITOR       

        public static void Log(object payload = null, params object[] args)
        {
            Debug.Log(BuildMessage(payload, args));
        }

        public static void Warn(object payload = null, params object[] args)
        {            
            Debug.LogWarning(BuildMessage(payload, args));
        }

        public static void Error(object payload = null, params object[] args)
        {            
            Debug.LogError(BuildMessage(payload, args));
        }

#endif


#if !UNITY_EDITOR
        
        public static void Log(object payload = null, object[] args = null) { }
        public static void Warn(object payload = null, object[] args = null) { }
        public static void Error(object payload = null, object[] args = null) { }

#endif
    }
}

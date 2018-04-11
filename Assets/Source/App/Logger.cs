using UnityEngine;

namespace Assets.Source.App
{
    public class Logger
    {
        public static void Info(object Message)
        {
            WriteLog("INFO: " + Message.ToString());
        }

        public static void Error(object Message)
        {
            WriteLog("ERROR: " + Message.ToString());
        }

        private static void WriteLog(string Message)
        {
            if (AppConfig.Get().IsDebugEnabled)
            {
                Debug.Log(Message);
            }
        }
    }
}
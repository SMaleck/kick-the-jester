using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Assets.Source.Util.DataStorageStrategies
{
    public class JsonDataStorageStrategy : IDataStorageStrategy
    {
        private readonly string _basePath;

        public JsonDataStorageStrategy()
        {
            _basePath = Application.persistentDataPath;
        }

        public T Load<T>(string filename) where T : class
        {
            var filePath = GetFilePath(filename);
            App.Logger.Log($"Loading savegame from {filePath}");

            if (!File.Exists(filePath))
            {
                return null;
            }

            string json = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Save<T>(string filename, T data)
        {
            var filePath = GetFilePath(filename);
            App.Logger.Log($"Saving savegame to {filePath}");

            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, json);
        }

        private string GetFilePath(string filename)
        {
            return Path.Combine(_basePath, $"{filename}");
        }
    }
}

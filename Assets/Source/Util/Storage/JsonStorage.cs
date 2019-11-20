using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Source.Util.Storage
{
    public class JsonStorage
    {
        private readonly string name;
        private readonly string path;

        public JsonStorage(string filename)
        {
            this.name = filename;
            this.path = Application.persistentDataPath 
                        + (name.StartsWith("/") ? name : "/" + name);
        }


        public T Load<T>() where T : class
        {
            App.Logger.Log($"Loading savegame from {path}");

            if (!File.Exists(path))
            {
                return null;
            }

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(json);
        }


        public void Save<T>(T data)
        {
            App.Logger.Log($"Saving savegame to {path}");

            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, json);
        }
    }
}

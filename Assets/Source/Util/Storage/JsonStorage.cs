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


        public T Load<T>() where T : new()
        {
            if (!File.Exists(path))
            {
                return new T();
            }

            string json = File.ReadAllText(path);
            T result = JsonConvert.DeserializeObject<T>(json);

            return (result != null) ? result : new T();
        }


        public void Save<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, json);
        }
    }
}

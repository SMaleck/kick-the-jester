using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Source.App
{
    public class FileDataStorage<T> where T : Serializable, new()
    {
        private string name;
        private string path;

        public FileDataStorage(string filename)
        {
            this.name = filename;
            this.path = Application.persistentDataPath + 
                (name.StartsWith("/") ? name : "/" + name);
        }

        public T LoadFromJson()
        {
            if (!File.Exists(path))
            {
                return new T();
            }

            string json = File.ReadAllText(path);
            Debug.Log("Loaded JSON object: " + json);
            return JsonUtility.FromJson<T>(json);
        }

        public void SaveAsJson(T data)
        {
            string json = JsonUtility.ToJson(data);
            Debug.Log("Saving JSON object: " + json);
            File.WriteAllText(path, json);
        }
    }
}

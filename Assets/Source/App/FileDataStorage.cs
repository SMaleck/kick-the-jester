using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Source.App
{
    /// <summary>
    /// <para>Class that can be used to store any object into a file in the system
    /// </para>
    /// <para>It uses <see cref="Application.persistentDataPath"/> as the folder</para>
    /// <para>For the formats available, see <see cref="DataFormat"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileDataStorage<T> where T : Serializable, new()
    {
        public enum DataFormat { JSON }

        private string name;
        private string path;

        public FileDataStorage(string filename)
        {
            this.name = filename;
            this.path = Application.persistentDataPath + 
                (name.StartsWith("/") ? name : "/" + name);
        }

        /// <summary>
        /// Loads persisted data from file using format given.
        /// Formats supported, see <see cref="DataFormat"/> values
        /// </summary>
        public T Load(DataFormat format = DataFormat.JSON)
        {
            switch (format)
            {
                case DataFormat.JSON:
                    return LoadFromJson();
                default:
                    throw new FormatException("Format requested is not available: " + format);
            }
        }

        /// <summary>
        /// Saves data to a file using the format given.
        /// Formats supported, see <see cref="DataFormat"/> values
        /// </summary>
        public void Save(T data, DataFormat format = DataFormat.JSON)
        {
            switch (format)
            {
                case DataFormat.JSON:
                    SaveAsJson(data);
                    break;
                default:
                    throw new FormatException("Format requested is not available: " + format);
            }
        }

        private T LoadFromJson()
        {
            if (!File.Exists(path))
            {
                return new T();
            }

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }

        private void SaveAsJson(T data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        }
    }
}

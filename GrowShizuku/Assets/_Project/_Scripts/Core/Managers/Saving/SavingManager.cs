using System.IO;
using UnityEngine;

namespace Shizuku.Managers
{
    public static class SavingManager
    {
        private static string BasePath => Application.persistentDataPath;

        private static string GetPath(string key) => Path.Combine(BasePath, $"{key}.json");

        public static void Save<T>(string key, T data)
        {
            string path = GetPath(key);
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }

        public static bool TryLoad<T>(string key, out T data)
        {
            string path = GetPath(key);

            if (!File.Exists(path))
            {
                data = default;
                return false;
            }

            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<T>(json);
            
            return true;
        }

        public static void Delete(string key)
        {
            string path = GetPath(key);

            if (File.Exists(path))
            { 
                File.Delete(path); 
            }
        }

        public static void DeleteAll()
        {
            foreach (var file in Directory.GetFiles(BasePath, "*.json"))
            {
                File.Delete(file);
            }
        }
    }
}
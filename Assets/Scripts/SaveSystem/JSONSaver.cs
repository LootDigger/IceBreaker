using System.IO;
using UnityEngine;

public class JSONSaver : ISaver
{
    public void SaveData<T>(T obj, string path) where T : class
    {
        string data = JsonUtility.ToJson(obj);
        File.WriteAllText(path,data);
    }

    public T LoadData<T>(string path) where T : class
    {
        if (!File.Exists(path)) return null;
        string data = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(data);
    }
}

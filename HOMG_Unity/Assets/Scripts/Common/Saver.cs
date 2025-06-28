using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

public static class Saver
{
    public static bool Save<T>(T obj, string path, bool toApplicationPersistentDataPath = true) where T : class
    {
        if (obj == null)
        {
            Debug.LogError("对象为空，无法保存");
            return false;
        }

        string fullPath = path;
        if (toApplicationPersistentDataPath)
        {
            fullPath = Application.persistentDataPath + "/" + path;
        }

        byte[] data = null;
        try
        {
            // 使用 BinaryFormatter 将对象序列化为字节流
            using (var memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, obj);
                data = memoryStream.ToArray();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"序列化对象时出错: {ex}");
            return false;
        }

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); // 确保目录存在
            File.WriteAllBytes(fullPath, data); // 将字节流写入文件
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"保存文件时发生错误: {fullPath}\n{ex}");
            return false;
        }
    }
}

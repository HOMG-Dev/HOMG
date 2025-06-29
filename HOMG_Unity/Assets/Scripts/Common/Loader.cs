using System;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Networking;

public static class Loader
{
    public static T Load<T>(string path, bool fromApplicationPersistentDataPath = true) where T : class
    {
        string fullPath = path;
        if (fromApplicationPersistentDataPath)
        {
            fullPath = Application.persistentDataPath + "/" + path;
        }

        if (!File.Exists(fullPath))
        {
            Debug.LogError($"文件不存在: {fullPath}");
            return null;
        }

        byte[] data = null;
        try
        {
            data = File.ReadAllBytes(fullPath); // 读取字节流
        }
        catch (Exception ex)
        {
            Debug.LogError($"读取文件时出错: {fullPath}\n{ex}");
            return null;
        }

        try
        {
            using (var memoryStream = new MemoryStream(data)) // 使用内存流反序列化
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(memoryStream); // 反序列化为对象
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"反序列化文件时出错: {fullPath}\n{ex}");
            return null;
        }
    }

    public static void LoadFromUrlAsync<T>(string url, System.Action<T> onSuccess, System.Action<string> onError = null) where T : class
    {
        CoroutineStarter.Coroutine(InternalLoadFromUrlAsync<T>(url, onSuccess, onError));
    }

    private static IEnumerator InternalLoadFromUrlAsync<T>(string url, System.Action<T> onSuccess, System.Action<string> onError) where T : class
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    byte[] data = www.downloadHandler.data; // 获取字节流

                    using (var memoryStream = new MemoryStream(data))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        T obj = (T)binaryFormatter.Deserialize(memoryStream); // 反序列化为对象
                        if (onSuccess != null)
                        {
                            onSuccess(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"反序列化对象时发生错误: {ex}");
                    if (onError != null)
                    {
                        onError($"反序列化对象时发生错误: {ex.Message}");
                    }
                }
            }
            else
            {
                Debug.LogError($"从 URL 加载文件时发生错误: {www.error}");
                if (onError != null)
                {
                    onError($"从 URL 加载文件时发生错误: {www.error}");
                }
            }
        }
    }
}


using System.Collections;
using UnityEngine;

public class CoroutineStarter : MonoBehaviour
{
    private static CoroutineStarter instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Coroutine(IEnumerator coroutine)
    {
        if (instance == null)
        {
            GameObject go = new GameObject("CoroutineManager");
            instance = go.AddComponent<CoroutineStarter>();
        }
        instance.StartCoroutine(coroutine);
    }
}

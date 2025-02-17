using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.IO.LowLevel.Unsafe;

public class Singleton<T>
{
    private static readonly T instance = Activator.CreateInstance<T>();

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    public virtual void Init() { }
    
    public virtual void Update(float deltaTime) { }

    public virtual void OnDestroy() { }
}

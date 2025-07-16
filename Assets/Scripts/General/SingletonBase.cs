using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton")]
    [SerializeField] private bool _doNotDestroyOnLoad;

    public static T instance { get; private set; }

    protected virtual void Awake()
    {
        if (instance != null)
        { 
            Destroy(instance);
            return;
        }

        instance = this as T;

        if (_doNotDestroyOnLoad)
        {
            DontDestroyOnLoad(instance);
        }

    }
}

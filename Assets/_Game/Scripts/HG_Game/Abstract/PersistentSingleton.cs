using System;
using UnityEngine;
namespace HG
{
    public abstract class PersistentSingleton<T> : Singleton<T>
        where T : MonoBehaviour
    {
        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}

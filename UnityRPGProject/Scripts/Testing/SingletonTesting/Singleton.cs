using System;
using UnityEditor;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            Debug.Log("Singleton Get Called");

            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name + "123";
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
            
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            Debug.Log("DontDestroyOnLoad");

            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("Singleton Awake Called");
    }

}


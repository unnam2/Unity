using System;
using UnityEngine;

public abstract class SingleTon<T> where T : class
{
    private static T s_instance = null;

    public static T Instance
    {
        get
        {
            if (null == s_instance)
            {
                lock (new object())
                {
                    if (null == s_instance)
                    {
                        Type t = typeof(T);

                        s_instance = (T)Activator.CreateInstance(t, true);
                    }
                }
            }

            return s_instance;
        }
    }
}

public abstract class MonoSingleTon<T> : MonoBehaviour where T : MonoSingleTon<T>
{
    private static T s_instance = null;

    public static T Instance
    {
        get
        {
            if (null == s_instance)
            {
                lock (new object())
                {
                    if (null == s_instance)
                    {
                        s_instance = FindObjectOfType(typeof(T)) as T;

                        if (null == s_instance)
                        {
                            s_instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();
                            DontDestroyOnLoad(s_instance);
                        }
                    }
                }
            }
            return s_instance;
        }
    }
}

public static class Util
{
    public static int GetEnumNum<T>() where T : struct, IConvertible
    {
        if (false == typeof(T).IsEnum)
        {
            Debug.LogWarning("no enum");
            return 0;
        }
        return Enum.GetValues(typeof(T)).Length;
    }

    public static T GetTop<T>(GameObject col) where T : Component
    {
        Transform parent = col.transform;

        while (true)
        {
            T t = parent.GetComponent<T>();
            if (t != null)
            {
                return t;
            }

            parent = parent.parent;
            if (parent == null)
            {
                return null;
            }
        }
    }
}

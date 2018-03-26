using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolingList<T> where T : Pooling
{
    private List<T> m_list;
    private Transform m_owner;

    public PoolingList()
    {
        m_list = new List<T>();
    }

    public void SetTransform(Transform parent)
    {
        GameObject gam = new GameObject();
        gam.name = typeof(T).ToString();
        gam.transform.SetParent(parent);

        if (typeof(T) == typeof(UIWorld))
        {
            gam.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            gam.AddComponent<CanvasScaler>();
            gam.AddComponent<GraphicRaycaster>();
        }
        else if (typeof(T) == typeof(UIHud))
        {
            gam.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            gam.AddComponent<CanvasScaler>();
            gam.AddComponent<GraphicRaycaster>();
        }

        m_owner = gam.transform;
    }

    public T Create(string text)
    {
        T val = Resources.Load<T>(typeof(T).ToString() + "/" + text);

        if (val == null)
        {
            Debug.LogError(text + " is null");
            return null;
        }

        for (int i = 0; i < m_list.Count; ++i)
        {
            T temp = m_list[i];
            if (temp.name == val.name && temp.isActiveAndEnabled == false)
            {
                temp.gameObject.SetActive(true);
                return temp;
            }
        }

        T obj = Object.Instantiate(val);
        m_list.Add(obj);
        obj.transform.SetParent(m_owner, false);
        obj.name = val.name;
        return obj;
    }

    public T Create(string text, Vector3 position)
    {
        T obj = Create(text);
        obj.transform.position = position;
        return obj;
    }

    public List<T> GetList()
    {
        List<T> list = new List<T>();
        for (int i = 0; i < m_list.Count; ++i)
        {
            T temp = m_list[i];
            if (temp.isActiveAndEnabled == true)
            {
                list.Add(temp);
            }
        }
        return list;
    }

    public void Clear()
    {
        for (int i = 0; i < m_list.Count; ++i)
        {
            Object.Destroy(m_list[i].gameObject);
        }
        m_list.Clear();
    }

    public U[] GetList<U>() where U : T
    {
        List<U> list = new List<U>();

        for (int i = 0; i < m_list.Count; ++i)
        {
            T temp = m_list[i];
            if (temp.isActiveAndEnabled == true && temp is U)
            {
                list.Add((U)temp);
            }
        }
        return list.ToArray();
    }
}

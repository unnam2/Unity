using System;

public class Data<T> where T : IConvertible, IComparable, IComparable<T>, IEquatable<T>
{
    private event Action m_func;
    private T m_data;

    public Data()
    {
        m_func = null;
        m_data = default(T);
    }

    public T SetData
    {
        set
        {
            m_func?.Invoke();
            m_data = value;
        }
    }

    public Action SetFunc
    {
        set
        {
            m_func += value;
        }
    }

    public static implicit operator T(Data<T> v)
    {
        return v.m_data;
    }
}

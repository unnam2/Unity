using System.Collections.Generic;

public class GlobalData : SingleTon<GlobalData>
{
    public List<Control> m_list;

    private GlobalData()
    {
        m_list = new List<Control>();
    }
}

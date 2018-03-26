using UnityEngine;
using UnityEngine.UI;

public class UIPing : UIHud
{
    private Text m_text;
    private float m_beforeTime;

    protected override void OnAwake()
    {
        base.OnAwake();

        m_text = GetComponent<Text>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        m_beforeTime = Time.time;
    }

    public void Check()
    {
        float value = Time.time - m_beforeTime;
        value *= 1000f;
        m_beforeTime = Time.time;

        m_text.text = Mathf.Round(value) + " ms";
    }
}

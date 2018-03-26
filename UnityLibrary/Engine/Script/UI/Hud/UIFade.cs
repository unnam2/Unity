using UnityEngine;
using UnityEngine.UI;

public class UIFade : UIHud
{
    public enum FadeType
    {
        In, Out
    }

    public FadeType Type;
    public float FadeTime = 1f;

    private Image m_image;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_image = GetComponentInChildren<Image>();
    }

    protected override void OnEnableAfterFrame()
    {
        base.OnEnableAfterFrame();

        if (Type == FadeType.In)
        {
            Color color = m_image.color;
            color.a = 0f;
            m_image.color = color;
        }
        else if (Type == FadeType.Out)
        {
            Color color = m_image.color;
            color.a = 1f;
            m_image.color = color;
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (Type == FadeType.In)
        {
            Color color = m_image.color;
            color.a += Time.deltaTime / FadeTime;
            m_image.color = color;
        }
        else if (Type == FadeType.Out)
        {
            Color color = m_image.color;
            color.a -= Time.deltaTime / FadeTime;
            m_image.color = color;

            if (color.a <= 0f)
            {
                Remove();
            }
        }
    }
}

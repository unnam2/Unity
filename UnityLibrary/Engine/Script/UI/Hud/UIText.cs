using UnityEngine.UI;

public class UIText : UIHud
{
    private Text m_text;

    protected override void OnAwake()
    {
        base.OnAwake();

        m_text = GetComponent<Text>();
    }

    public void SetText(string value)
    {
        m_text.text = value;
    }
}

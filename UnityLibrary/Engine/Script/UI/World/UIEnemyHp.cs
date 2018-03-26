using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHp : UIWorld
{
    public Image m_hp;
    public Text m_current;
    public Text m_origin;

    [HideInInspector]
    public Monster m_owner;

    protected override void OnEnableAfterFrame()
    {
        m_origin.text = ((long)m_owner.GetOrignHP).ToString();
    }

    protected override void OnUpdate()
    {
        m_hp.fillAmount = m_owner.GetCurrentHP / m_owner.GetOrignHP;

        Vector3 pos = m_owner.transform.position;
        pos.y += m_owner.m_capsuleCollider.height + 1f;
        transform.position = pos;

        m_current.text = ((long)m_owner.GetCurrentHP).ToString();
    }
}

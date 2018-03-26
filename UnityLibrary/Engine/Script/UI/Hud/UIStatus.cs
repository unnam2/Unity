using UnityEngine.UI;

public class UIStatus : UIHud
{
    public Image m_hp;
    public Image m_resource;

    private Player m_player;

    protected override void OnStart()
    {
        base.OnStart();

        Player[] list = PoolingManage.Character.GetList<Player>();
        if (list.Length == 1)
        {
            m_player = list[0];
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (m_player != null)
        {
            m_hp.fillAmount = m_player.m_hp / m_player.m_status.hp;
            m_resource.fillAmount = m_player.m_resource / m_player.m_status.resource;
        }
    }
}

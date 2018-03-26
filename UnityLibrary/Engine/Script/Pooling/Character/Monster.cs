public class Monster : Character
{
    [System.Serializable]
    public struct Status
    {
        public float hp;
        public float damage;
        public float moveSpeed;
    }

    public Status m_status;

    private Status m_statusApplyLevel;
    private UIEnemyHp m_uiHP;

    public float GetOrignHP { get { return m_statusApplyLevel.hp; } }
    public float GetCurrentHP { get { return m_hp; } }

    private AIDefault m_ai;

    protected override void OnEnableAfterFrame()
    {
        m_uiHP = (UIEnemyHp)PoolingManage.UIWorld.Create("EnemyHP");
        m_uiHP.transform.position = transform.position;
        m_uiHP.m_owner = this;
        m_hp = m_statusApplyLevel.hp;
        m_damage = m_statusApplyLevel.damage;
        m_moveSpeed = m_statusApplyLevel.moveSpeed;

        m_ai = new AIDefault(this);
        m_ai.OnStart();
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        m_ai.OnUpdate();
    }
    protected override void OnRemove()
    {
        m_uiHP.Remove();
    }

    public void ApplyLevel(float hp, float damage)
    {
        m_statusApplyLevel.hp = m_status.hp * hp;
        m_statusApplyLevel.damage = m_status.damage * damage;
        m_statusApplyLevel.moveSpeed = m_status.moveSpeed;
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
public class Character : Pooling
{
    public SkillManage m_skillManage;

    public float m_hp;
    public float m_damage;
    public float m_moveSpeed;
    public Transform m_target;

    public NavMeshManage m_navMeshManage;
    public CapsuleCollider m_capsuleCollider;

    private Coroutine m_coroutineMove;

    protected override void OnAwake()
    {
        m_capsuleCollider = GetComponent<CapsuleCollider>();
        m_capsuleCollider.isTrigger = true;
        m_navMeshManage = new NavMeshManage(this, m_capsuleCollider);
    }

    protected override void OnStart()
    {
        base.OnStart();
        m_skillManage.Init(this);
        m_navMeshManage.Start();
        m_coroutineMove = null;
        m_target = null;
    }

    protected override void OnUpdate()
    {
        m_skillManage.OnUpdate();
        m_navMeshManage.ArriveCheck();
        m_navMeshManage.Update();
    }

    public void MoveStart(Vector3 pos)
    {
        m_navMeshManage.MoveStart(pos);

        if (null != m_coroutineMove)
        {
            StopCoroutine(m_coroutineMove);
        }
        m_coroutineMove = StartCoroutine(MoveCheck());
    }

    public void Arrive()
    {
        if (null != m_coroutineMove)
        {
            StopCoroutine(m_coroutineMove);
        }
    }

    public void Attacked(float damage)
    {
        m_hp -= damage;

        UIDamage ui = (UIDamage)PoolingManage.UIWorld.Create("Damage");

        Vector3 pos = transform.position;
        pos.y += m_capsuleCollider.height + 1f;
        ui.transform.position = pos;

        ui.m_damage.text = ((int)damage).ToString();

        if (m_hp <= 0f)
        {
            m_hp = 0f;
            Remove();
        }
    }

    private IEnumerator MoveCheck()
    {
        const float DESTINATION_CHECK_TIME = 1f; //체크할 다음 시간
        const float DESTINATION_CHECK_DISTANCE = 0.5f; //체크할 거리

        Vector3 m_beforePos;

        while (m_navMeshManage.GetNavMeshAgentEnable) //이동 중
        {
            m_beforePos = transform.position;
            yield return new WaitForSeconds(DESTINATION_CHECK_TIME);
            if ((m_beforePos - transform.position).sqrMagnitude < DESTINATION_CHECK_DISTANCE * DESTINATION_CHECK_DISTANCE)
            {
                Arrive();
            }
        }

        m_coroutineMove = null;
    }
}

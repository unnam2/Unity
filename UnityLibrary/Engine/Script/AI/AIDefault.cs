using UnityEngine;
using System.Collections;

internal class AIDefault
{
    private bool m_stop;
    private Monster m_owner;

    public AIDefault(Monster monster)
    {
        m_owner = monster;
    }

    public void OnStart()
    {
        m_stop = false;
    }

    public void OnUpdate()
    {
        if (!m_owner.m_navMeshManage.GetNavMeshAgentEnable && !m_stop)
        {
            m_stop = true;
            m_owner.StartCoroutine(MoveNext());
        }
    }

    private IEnumerator MoveNext()
    {
        yield return new WaitForSeconds(2f);
        Vector3 next = m_owner.transform.position;
        next.x += Random.Range(-10f, 10f);
        next.z += Random.Range(-10f, 10f);
        m_owner.MoveStart(next);
        m_stop = false;
    }
}

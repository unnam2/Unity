using System.Collections;
using UnityEngine;

public class ProjectileMove
{
    private Projectile m_owner;

    public ProjectileMove(Projectile owner)
    {
        m_owner = owner;
    }

    public void Straight(float speed, Vector3 forward)
    {
        m_owner.transform.forward = forward;
        m_owner.StartCoroutine(IStraight(speed));
    }

    public void Straight(float speed, Transform target)
    {
        m_owner.transform.LookAt(target);
        m_owner.StartCoroutine(IStraight(speed));
    }

    public void Straight(float speed, Quaternion rotation)
    {
        m_owner.transform.rotation = rotation;
        m_owner.StartCoroutine(IStraight(speed));
    }

    private IEnumerator IStraight(float speed)
    {
        Transform tra = m_owner.transform;
        while (m_owner.gameObject.activeSelf)
        {
            tra.position += tra.forward * speed * Time.deltaTime;
            yield return null;
        }
    }
}

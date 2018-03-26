using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Projectile : Pooling
{
    public delegate void CollisionCallback(Projectile my, Pooling col);

    private event CollisionCallback m_collisionCallback;

    private Team m_avoidTeam;
    private Rigidbody m_rigidbody;
    private SphereCollider m_collider;

    public Pooling Owner { get; private set; }
    public ProjectileMove Move { get; private set; }

    public void Set(float liveTime, Pooling owner = null, CollisionCallback call = null, Team avoid = Team.None)
    {
        StartCoroutine(LiveTime(liveTime));
        Owner = owner;
        m_collisionCallback += call;
        m_avoidTeam = avoid;
    }

    protected override void OnAwake()
    {
        m_collider = GetComponent<SphereCollider>();
        m_rigidbody = gameObject.AddComponent<Rigidbody>();
        Move = new ProjectileMove(this);
    }

    protected override void OnStart()
    {
        m_collider.isTrigger = true;
        m_collisionCallback = null;
        m_rigidbody.useGravity = false;
        m_avoidTeam = Team.None;
    }

    private IEnumerator LiveTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (null != m_collisionCallback)
            m_collisionCallback(this, null);
        else
            Remove();
    }

    private void OnTriggerEnter(Collider other)
    {
        Pooling poo = other.GetComponent<Pooling>();

        if (poo == Owner || poo != null && m_avoidTeam.Include(poo.Team))
            return;

        if (null != m_collisionCallback)
            m_collisionCallback(this, poo);
        else
            Remove();
    }
}

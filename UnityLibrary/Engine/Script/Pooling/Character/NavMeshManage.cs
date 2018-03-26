using UnityEngine;
using UnityEngine.AI;

public class NavMeshManage
{
    public NavMeshAgent m_navMeshAgent;
    public NavMeshObstacle m_navMeshObstacle;

    public Character Owner { get; private set; }
    public bool GetNavMeshAgentEnable { get { return m_navMeshAgent.enabled; } }

    public NavMeshManage(Character owner, CapsuleCollider collider)
    {
        Owner = owner;

        m_navMeshAgent = Owner.gameObject.AddComponent<NavMeshAgent>();
        m_navMeshAgent.enabled = false;
        m_navMeshAgent.radius = collider.radius;
        m_navMeshAgent.height = collider.height;
        m_navMeshAgent.baseOffset = 0f;
        m_navMeshAgent.angularSpeed = float.MaxValue;
        m_navMeshAgent.acceleration = float.MaxValue;

        m_navMeshObstacle = Owner.gameObject.AddComponent<NavMeshObstacle>();
        m_navMeshObstacle.enabled = false;
        m_navMeshObstacle.shape = NavMeshObstacleShape.Capsule;
        m_navMeshObstacle.center = collider.center;
        m_navMeshObstacle.radius = collider.radius;
        m_navMeshObstacle.height = collider.height;
    }

    public void Start()
    {
        m_navMeshObstacle.enabled = false;
        m_navMeshAgent.enabled = true;
    }

    public void Update()
    {
        if (m_navMeshAgent.enabled == true)
        {
            m_navMeshAgent.speed = Owner.m_moveSpeed;
        }
    }

    public void MoveStart(Vector3 pos)
    {
        m_navMeshObstacle.enabled = false;

        m_navMeshAgent.enabled = true;
        m_navMeshAgent.SetDestination(pos);
    }

    public void Arrive()
    {
        m_navMeshAgent.enabled = false;
        m_navMeshObstacle.enabled = true;
    }

    public void ArriveCheck()
    {
        if (true == m_navMeshAgent.enabled && 0f == m_navMeshAgent.remainingDistance)
        {
            Arrive();
        }
    }
}

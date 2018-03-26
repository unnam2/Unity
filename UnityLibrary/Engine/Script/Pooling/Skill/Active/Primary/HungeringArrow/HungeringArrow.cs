using UnityEngine;

public class HungeringArrow : SkillActive
{
    //public const float DAMAGE_RATIO = 1.55f;
    //public const float PIERCE_CHANCE = 0.35f;

    //private const float REMOVE_TIME = 3f;
    //private const float RETARGET_RANGE = 5f;
    //private const float PROJECTILE_SPEED = 20f;
    //private const float RETARGET_TIME = (RETARGET_RANGE - 1f) / PROJECTILE_SPEED;

    //private float m_currentTime;
    //private float m_retargetTime;
    //private Projectile m_projectile;

    //protected override void OnEnableAfterFrame()
    //{
    //    m_currentTime = 0f;
    //    m_retargetTime = 0f;

    //    m_projectile = PoolingManage.Projectile.Create("Arrow");
    //    m_projectile.m_collisionCallback += CollisionCallback;
    //    ProjectileAddForce(m_projectile, "Fire", PlayerGetTarget(), PROJECTILE_SPEED);
    //}

    //protected override void OnUpdate()
    //{
    //    if (m_currentTime >= REMOVE_TIME)
    //    {
    //        Remove();
    //    }
    //    m_currentTime += Time.deltaTime;
    //    if (m_retargetTime >= RETARGET_TIME)
    //    {
    //        ReTarget();
    //        m_retargetTime = 0f;
    //    }
    //    m_retargetTime += Time.deltaTime;
    //}

    //protected override void OnRemove()
    //{
    //    m_projectile.Remove();
    //}

    //private void CollisionCallback(Character character)
    //{
    //    float damage = DAMAGE_RATIO * m_owner.m_damage;

    //    if (character is Monster)
    //    {
    //        character.Attacked(damage);
    //        PoolingManage.Effect.Create("Explosion").transform.position = m_projectile.transform.position;

    //        if (Random.Range(0f, 1f) <= PIERCE_CHANCE)
    //        {
    //            m_currentTime = 0f;
    //            m_retargetTime = 0f;
    //        }
    //        else
    //        {
    //            Remove();
    //        }
    //    }
    //}

    //private void ReTarget()
    //{
    //    Monster target = PoolingManage.Character.GetList<Monster>()[0];
    //    if (target != null)
    //    {
    //        float distanceSqr = (target.transform.position - m_projectile.transform.position).sqrMagnitude;
    //        if (distanceSqr < RETARGET_RANGE * RETARGET_RANGE)
    //        {
    //            m_projectile.InitPhysics();
    //            ProjectileAddForce(m_projectile, m_projectile.transform.position, target.transform.position, PROJECTILE_SPEED);
    //        }
    //    }
    //}
}

using UnityEngine;

public abstract class SkillActive : Pooling
{
    [System.Serializable]
    public class Data
    {
        public int fireNum = 1;
        public bool charge = false;
        public float range = 1f;
        public float cooldown = 1f;
        public string name;
        public float resource;
    }

    public Data m_data;

    [HideInInspector]
    public Character m_owner;

    protected Vector3 PlayerGetTarget()
    {
        return m_owner.m_target.position;
    }

    protected Transform GetFirePosition(string str)
    {
        Transform[] transforms = m_owner.GetComponentsInChildren<Transform>();
        Transform value = null;
        for (int i = 0; i < transforms.Length; ++i)
        {
            if (transforms[i].name == str)
            {
                if (value != null)
                {
                    Debug.LogError("발사 위치 중복");
                    return null;
                }
                value = transforms[i];
            }
        }
        return value;
    }

    //protected void ProjectileAddForce(Projectile projectile, string firePosition, Vector3 targetPosition, float power)
    //{
    //    ProjectileAddForce(projectile, GetFirePosition(firePosition).position, targetPosition, power);
    //}
    //protected void ProjectileAddForce(Projectile projectile, Vector3 firePosition, Vector3 targetPosition, float power)
    //{
    //    projectile.transform.position = firePosition;
    //    Vector3 direction = targetPosition - projectile.transform.position;
    //    direction.Normalize();
    //    direction.y = 0f;
    //    ProjectileAddForce(projectile, direction * power);
    //}
    //protected void ProjectileAddForce(Projectile projectile, Vector3 velocity)
    //{
    //    projectile.m_rigidbody.AddForce(velocity, ForceMode.VelocityChange);
    //    projectile.transform.LookAt(projectile.transform.position + velocity);
    //}
}

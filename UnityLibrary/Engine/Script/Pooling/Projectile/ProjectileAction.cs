using UnityEngine;

public class ProjectileAction
{
    public static void Hit(Projectile pro, Pooling other)
    {
        pro.Remove();
    }
}

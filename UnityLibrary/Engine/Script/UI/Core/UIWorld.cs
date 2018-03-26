using UnityEngine;

public class UIWorld : Pooling
{
    protected override void OnStart()
    {
        base.OnStart();
        transform.rotation = Camera.main.transform.rotation;
    }
}

using UnityEngine;
using System.Collections;

public abstract class Pooling : MonoBehaviour
{
    public Team Team { get; set; }
    public string ID { get; set; }

    private void Awake() { OnAwake(); }
    private void OnEnable()
    {
        if (PoolingManage.IsLoad)
            return;

        StartCoroutine(OneFrame());
    }
    public void Remove()
    {
        if (PoolingManage.IsLoad)
            return;

        OnRemove();

        if (!(this is Effect))
            gameObject.SetActive(false);
    }

    protected virtual void OnAwake() { Team = Team.None; }
    protected virtual void OnStart() { }
    protected virtual void OnEnableAfterFrame() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnRemove() { }

    private IEnumerator OneFrame()
    {
        OnStart();
        yield return null;
        OnEnableAfterFrame();
        while (isActiveAndEnabled)
        {
            OnUpdate();
            yield return null;
        }
    }

    //On으로만 사용
    protected void Start() { }
    protected void Update() { }
}

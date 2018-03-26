using System.Collections;
using UnityEngine;

public class Title : MonoBehaviour
{
    private void Start()
    {
        PoolingManage.Instance.Init();
        GameManager.Instance.Init();
        Client.Instance.Init();

        StartCoroutine(OneFrame());
    }

    private IEnumerator OneFrame()
    {
        yield return null;
        PoolingManage.UIHud.Create("UIPing");
    }
}

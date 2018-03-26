using UnityEngine;
using System.Collections;

public class UIHI : UIHud
{
    private const float PLAYSCENE_TIME = 0.5f;

    protected override void OnStart()
    {
        base.OnStart();

        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(PLAYSCENE_TIME);
        GameManager.LoadScene(GameManager.SceneString.Ingame);
        //Remove();
    }
}

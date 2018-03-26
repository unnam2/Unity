using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingleTon<GameManager>
{
    public enum SceneString
    {
        Title, Ingame
    }

    private GameManager()
    {
        Application.runInBackground = true;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        PoolingManage.ClearAll();
        UIFade fade = (UIFade)PoolingManage.UIHud.Create("UIFade");
        fade.Type = UIFade.FadeType.Out;
        PoolingManage.PreLoad();
    }

    public static void LoadScene(SceneString value)
    {
        if (SceneManager.sceneCount == 1)
        {
            PoolingManage.Instance.StartCoroutine(ILoadScene(value.ToString()));
        }
    }

    private static IEnumerator ILoadScene(string value)
    {
        PoolingManage.UIHud.Clear();
        PoolingManage.UIWorld.Clear();

        UIFade fade = (UIFade)PoolingManage.UIHud.Create("UIFade");
        fade.Type = UIFade.FadeType.In;
        yield return new WaitForSeconds(fade.FadeTime);

        AsyncOperation ao = SceneManager.LoadSceneAsync(value.ToString());
        ao.allowSceneActivation = false;

        UIloading load = (UIloading)PoolingManage.UIHud.Create("Loading");

        float timer = 0f;

        while (!ao.isDone)
        {
            if (ao.progress >= 0.9f)
            {
                load.Slider.value = Mathf.Lerp(load.Slider.value, 1f, timer);
                if (load.Slider.value >= 1f)
                {
                    ao.allowSceneActivation = true;
                }
            }
            else
            {
                load.Slider.value = Mathf.Lerp(load.Slider.value, ao.progress, timer);
                if (load.Slider.value >= ao.progress)
                {
                    timer = 0f;
                }
            }

            timer += Time.deltaTime;

            yield return null;
        }
    }
    public void Init()
    {
        SceneManager_sceneLoaded(default(Scene), LoadSceneMode.Single);
    }
}

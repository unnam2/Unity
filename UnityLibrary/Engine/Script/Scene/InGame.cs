using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    private void Start()
    {
        PoolingManage.UIHud.Create("Text").GetComponentInChildren<Text>().text = "InGame";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            GameManager.LoadScene(GameManager.SceneString.Title);
        }
    }
}

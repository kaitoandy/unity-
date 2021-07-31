using UnityEngine;
using UnityEngine.SceneManagement;  //引用 場景管理API

public class SceneController : MonoBehaviour
{
    //Unity 按鈕如何跟腳本溝通
    //1.公開的方法
    //2.需要實體物件掛此腳本

    /// <summary>
    /// 載入遊戲場景
    /// </summary>
    public void LoadGameScene()
    {
        SceneManager.LoadScene("遊戲場景");
    }

    /// <summary>
    /// 離開遊戲場景
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();   //應用程式.離開() - 離開遊戲
        print("離開遊戲");     //Quit 在編輯器內不會執行
    }

}

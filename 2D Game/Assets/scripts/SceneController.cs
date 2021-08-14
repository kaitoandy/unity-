using UnityEngine;
using UnityEngine.SceneManagement;  //引用 場景管理API

public class SceneController : MonoBehaviour
{
    //Unity 按鈕如何跟腳本溝通
    //1.公開的方法
    //2.需要實體物件掛此腳本
    //3.按鈕 On Click 設定點擊事件為此物件及要呼叫的方法

    /// <summary>
    /// 載入遊戲場景
    /// </summary>
    public void LoadGameScene()
    {
        //等待2秒再載入場景
        //延遲呼叫(方法名稱.延遲時間)
        //作用: 等待指定時間後再呼叫指定方法
        Invoke("DelayLoadGameScene", 2);
        
    }

    //等待一段時間,再執行方法
    //Invoke 延遲呼叫
    /// <summary>
    /// 延遲載入場景
    /// </summary>
    private void DelayLoadGameScene()
    {
        SceneManager.LoadScene("遊戲場景");
    }
    /// <summary>
    /// 離開遊戲場景
    /// </summary>
    public void QuitGame()
    {
        Invoke("DelayQuitGame", 2);
    }

    /// <summary>
    /// 延遲離開遊戲
    /// </summary>
    private void DelayQuitGame()
    {
        Application.Quit();   //應用程式.離開() - 離開遊戲
        print("離開遊戲");     //Quit 在編輯器內不會執行
    }
}

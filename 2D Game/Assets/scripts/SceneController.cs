using UnityEngine;
using UnityEngine.SceneManagement;  //引用 場景管理API

public class SceneController : MonoBehaviour
{
    //Unity無法掛上腳本原因
    //1. 腳本內有紅色蚯蚓,任何一本
    //2. 類別與檔案名稱不同


    //Unity 按鈕如何跟腳本溝通
    //1.公開的方法
    //2.需要實體物件掛此腳本
    //3.按鈕 On Click 設定點擊事件為此物件及要呼叫的方法

    /// <summary>
    /// 載入遊戲場景
    /// </summary>
    public void LoadGameScene()
    {
        SceneManager.LoadScene("遊戲場景");

        //等2秒再載入
        //延遲呼叫(方法名稱,延遲時間)
        //作用等待指定時間再呼叫指定方法
        Invoke("DelayLoadGameScene", 2);

    }

    //等待一段時間再執行呼叫
    //Invoke 延遲呼叫
    /// <summary>
    /// 延遲載入場景
    /// </summary>

    private void DelayLoadGameScene()
    {
        //場景管理,載入場景 - 載入遊戲場景
        SceneManager.LoadScene("遊戲場景");
    }


    /// <summary>
    /// 離開遊戲場景
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();   //應用程式.離開() - 離開遊戲
        print("離開遊戲");     //Quit 在編輯器內不會執行

        Invoke("DelayQuiteGame", 2);
    }

    /// <summary>
    /// 延遲離開遊戲
    /// </summary>
    private void DelayQuitGame()
    { 
    
        //場景管理,載入場景 - 載入遊戲場景
        Application.Quit();
        print("離開遊戲");
        
    }

}

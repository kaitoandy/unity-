using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenController2 : MonoBehaviour
{
    public void LoadGameScene()
    {
        Invoke("DelayLoadGameScene", 2);
    }

    private void DelayLoadGameScene()
    {
        SceneManager.LoadScene("¦a«°³õ´º");
    }

    public void QuitGame()
    {
        Invoke("DelayQuit", 2);
    }

    private void DelayQuit()
    {
        Application.Quit();
        
    }
}

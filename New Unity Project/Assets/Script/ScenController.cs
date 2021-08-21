using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenController : MonoBehaviour
{
   public void LoadGameScene()
    {
        Invoke("DelayLoadGameScene", 2);
    }

    private void DelayLoadGameScene()
    {
        SceneManager.LoadScene("�C������");
    }

    public void QuitGame()
    {
        Invoke("DelayQuitGame",2);
    }

    private void DelayQuitGame()
    {
        Application.Quit();
        print("���}�C��");
    }

}

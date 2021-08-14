using UnityEngine;
using UnityEngine.SceneManagement;  //�ޥ� �����޲zAPI

public class SceneController : MonoBehaviour
{
    //Unity ���s�p���}�����q
    //1.���}����k
    //2.�ݭn���骫�󱾦��}��
    //3.���s On Click �]�w�I���ƥ󬰦�����έn�I�s����k

    /// <summary>
    /// ���J�C������
    /// </summary>
    public void LoadGameScene()
    {
        //����2��A���J����
        //����I�s(��k�W��.����ɶ�)
        //�@��: ���ݫ��w�ɶ���A�I�s���w��k
        Invoke("DelayLoadGameScene", 2);
        
    }

    //���ݤ@�q�ɶ�,�A�����k
    //Invoke ����I�s
    /// <summary>
    /// ������J����
    /// </summary>
    private void DelayLoadGameScene()
    {
        SceneManager.LoadScene("�C������");
    }
    /// <summary>
    /// ���}�C������
    /// </summary>
    public void QuitGame()
    {
        Invoke("DelayQuitGame", 2);
    }

    /// <summary>
    /// �������}�C��
    /// </summary>
    private void DelayQuitGame()
    {
        Application.Quit();   //���ε{��.���}() - ���}�C��
        print("���}�C��");     //Quit �b�s�边�����|����
    }
}

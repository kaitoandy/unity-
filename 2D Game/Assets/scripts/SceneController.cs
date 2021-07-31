using UnityEngine;
using UnityEngine.SceneManagement;  //�ޥ� �����޲zAPI

public class SceneController : MonoBehaviour
{
    //Unity�L�k���W�}����]
    //1. �}����������L�C,����@��
    //2. ���O�P�ɮצW�٤��P


    //Unity ���s�p���}�����q
    //1.���}����k
    //2.�ݭn���骫�󱾦��}��
    //3.���s On Click �]�w�I���ƥ󬰦�����έn�I�s����k

    /// <summary>
    /// ���J�C������
    /// </summary>
    public void LoadGameScene()
    {
        SceneManager.LoadScene("�C������");

        //��2��A���J
        //����I�s(��k�W��,����ɶ�)
        //�@�ε��ݫ��w�ɶ��A�I�s���w��k
        Invoke("DelayLoadGameScene", 2);

    }

    //���ݤ@�q�ɶ��A����I�s
    //Invoke ����I�s
    /// <summary>
    /// ������J����
    /// </summary>

    private void DelayLoadGameScene()
    {
        //�����޲z,���J���� - ���J�C������
        SceneManager.LoadScene("�C������");
    }


    /// <summary>
    /// ���}�C������
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();   //���ε{��.���}() - ���}�C��
        print("���}�C��");     //Quit �b�s�边�����|����

        Invoke("DelayQuiteGame", 2);
    }

    /// <summary>
    /// �������}�C��
    /// </summary>
    private void DelayQuitGame()
    { 
    
        //�����޲z,���J���� - ���J�C������
        Application.Quit();
        print("���}�C��");
        
    }

}

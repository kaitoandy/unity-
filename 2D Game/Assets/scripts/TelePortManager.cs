using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �ǰe���޲z
/// </summary>
public class TelePortManager : MonoBehaviour
{
    //1.�R�A�������O�Ҧ�����@�θ�� , �ƼƤ��O�X���R�A
    //2.�R�A�b���J�����ᤣ�|�^�_�w�]��

    /// <summary>
    /// �ҥH�Ǫ��ƶq
    /// </summary>
    public static int countAllEnemy;

    /// <summary>
    /// ���s�ƥ�۩w�q�覡
    /// 1.�ޥ�UnityEngine.Events API
    /// 2.�w�qUnityEvents���
    /// 3.�n���檺�a��ϥ�Invoke()�I�s
    /// </summary>
    [Header("�L���ƥ�")]
    public UnityEvent onPass;
     
    private void Start()
    {
        countAllEnemy = GameObject.FindGameObjectsWithTag("�Ǫ�").Length;
    }


    //Ĳ�o�ƥ�: Trigger
    //1.��ӸI�����󳣦�    Collider
    //2.�åB�䤤�@�Ӧ�      Rigiddbody
    //3.��Ө䤤�@�Ӧ��Ŀ�  Is Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "���a" && countAllEnemy == 0)
        {
            onPass.Invoke();
        }
    }
}

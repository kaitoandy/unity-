using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �I�������ǰe��a�����������
/// </summary>
public class TeleportManger : MonoBehaviour
{

    [Header("�ǰe�ƥ�")]
    public UnityEvent telepor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "���a")
        {
            telepor.Invoke();
        }
    }
}

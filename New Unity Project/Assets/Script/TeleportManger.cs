using UnityEngine;

/// <summary>
/// �I�������ǰe��a�����������
/// </summary>
public class TeleportManger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "���a")
        {
            print("�ǰe");
        }
    }
}

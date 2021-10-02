using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 碰到門之後傳送到地城場景的控制器
/// </summary>
public class TeleportManger : MonoBehaviour
{

    [Header("傳送事件")]
    public UnityEvent telepor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "玩家")
        {
            telepor.Invoke();
        }
    }
}

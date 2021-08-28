using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 傳送門管理
/// </summary>
public class TelePortManager : MonoBehaviour
{
    //1.靜態為此類別所有物件共用資料 , 複數不是合用靜態
    //2.靜態在載入場景後不會回復預設值

    /// <summary>
    /// 所以怪物數量
    /// </summary>
    public static int countAllEnemy;

    /// <summary>
    /// 按鈕事件自定義方式
    /// 1.引用UnityEngine.Events API
    /// 2.定義UnityEvents欄位
    /// 3.要執行的地方使用Invoke()呼叫
    /// </summary>
    [Header("過關事件")]
    public UnityEvent onPass;
     
    private void Start()
    {
        countAllEnemy = GameObject.FindGameObjectsWithTag("怪物").Length;
    }


    //觸發事件: Trigger
    //1.兩個碰撞物件都有    Collider
    //2.並且其中一個有      Rigiddbody
    //3.兩個其中一個有勾選  Is Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "玩家" && countAllEnemy == 0)
        {
            onPass.Invoke();
        }
    }
}

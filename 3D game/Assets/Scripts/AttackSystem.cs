using UnityEngine;

/// <summary>
/// 連段攻擊與集氣
/// </summary>
public class AttackSystem : MonoBehaviour
{
    #region 欄位:公開
    [Header("參數名稱")]
    public string parAttackPart = "普攻段數";
    public string parAttackGather = "集氣攻擊";
    [Header("連擊間隔時間"), Range(0, 2)]
    public float[] intervalBetweenAttackPart = { 0.4f, 0.5f, 0.9f };
    [Header("擊氣時間"), Range(0, 2)]
    public float timeToAttackGather = 1;
    [Header("攻擊段數"), Range(0, 10)]
    public int countAttackParMax = 3;

    #endregion

    #region 欄位:私人
    private Animator ani;
    /// <summary>
    /// 玩家按下左鍵的時間
    /// </summary>
    private float timerAttackGather;

    /// <summary>
    /// 連段計時器
    /// </summary>
    private float timerAttackPart;
    /// <summary>
    /// 攻擊段數
    /// </summary>
    private int countAttackPart;

    #endregion

    #region 事件
    //喚醒事件:遊戲播放後以及Start執行之前執行一次
    private void Awake()
    {
        ani.GetComponent<Animator>();
    }

    //開始事件:遊戲播放之後以及Awake執行之後執行一次
    private void Start()
    {

    }

    private void Update()
    {
        ClickTime();
    }

    #endregion

    #region 方法:私人
    /// <summary>
    /// 點擊後的時間累加
    /// </summary>
    private void ClickTime()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))                                 //按住左鍵
        {
            timerAttackGather += Time.deltaTime;                              //累加 計時器集氣
            timerAttackPart += Time.deltaTime;                                //累加 計時器段數
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))                              //放開左鍵
        {
            if (timerAttackGather >= timeToAttackGather)                      //如果 計時器 >= 擊氣時間
            {
                AttackGather();
            }
            else
            {
                AttackPart();
            }
            timerAttackGather = 0;                                             //計時器 歸零
        }

        

    }

    /// <summary>
    /// 擊氣攻擊
    /// </summary>
    private void AttackGather()
    {
        
        ani.SetTrigger(parAttackGather);
    }

    /// <summary>
    /// 攻擊段數
    /// </summary>
    private void AttackPart()
    {
        if (timerAttackPart <= intervalBetweenAttackPart[countAttackPart])                       //如果計時器段數 <= 段數間隔
        {
            
            CancelInvoke();
            Invoke("RestorAttackParCountToZero", intervalBetweenAttackPart[countAttackPart]);
            countAttackPart++;                                                                   //增加段數
        }
        else
        {
            countAttackPart = 0;                                                                 //段數歸零
        }
        timerAttackPart = 0;                                                                      //計時器歸零
        ani.SetInteger(parAttackPart,countAttackPart);
        if (countAttackPart == countAttackParMax) countAttackPart = 0;
    }
    
    private void RestorAttackParCountToZero()
    {
        countAttackPart = 0;
        ani.SetInteger(parAttackPart, countAttackPart);
    }

    #endregion
}

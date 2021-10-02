using UnityEngine;
using System.Collections;
using Invector.vCharacterController;
/// <summary>
/// 連段攻擊與集氣
/// </summary>
public class AttackSystem : MonoBehaviour
{
    #region 欄位:公開
    [Header("參數名稱")]
    public string parAttackPart = "普攻段數";
    public string parAttackGather = "普攻集氣";
    [Header("連擊間隔時間"), Range(0, 2)]
    public float[] intervalBetweenAttackPart = { 0.4f, 0.5f, 0.9f };
    [Header("擊氣時間"), Range(0, 2)]
    public float timeToAttackGather = 1;
    [Header("攻擊段數"), Range(0, 10)]
    public int countAttackParMax = 3;
    [Header("攻擊相關資料:攻擊力 尺寸 位移"),Range(0,500)]
    public float[] attack= {10,20,30,40};
    public Vector3[] areaAttackSize; 
    public Vector3[] areaAttackOffst;
    public Color[] areaAttackColor;
    public float[] delaySendAttackToTarget;

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
        ani = GetComponent<Animator>();
    }

    //開始事件:遊戲播放之後以及Awake執行之後執行一次
    private void Start()
    {

    }

    public vThirdPersonCamera v;
    public AvatarMask am;


    private void Update()
    {
        ClickTime();
    }

    private void OnDrawGizmos()
    {
        #region 攻擊範圍
        for (int i = 0; i < attack.Length; i++)
        {
            Gizmos.color = areaAttackColor[i];
            Gizmos.matrix = Matrix4x4.TRS(transform.position +
                transform.right * areaAttackOffst[i].x +
                transform.up * areaAttackOffst[i].y +
                transform.forward * areaAttackOffst[i].z,
                transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, areaAttackSize[i]);
        }
        

        #endregion
    }

    #endregion

    #region 方法:私人
    /// <summary>
    /// 點擊後的時間累加
    /// </summary>
    private void ClickTime()
    {
        //變身後.攻擊模式為變身攻擊
        //取得其他腳本資訊的資料
        //1. bool isTransform = GameObject.Find("變身系統").GetComponent<TransformSystem>().isTransform;
        //2. 將要取得資料改為靜態
         bool isTransform = TransformSystem.isTransform;

        if (isTransform && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("變身後攻擊");
            return;
        }



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
        StartCoroutine(AttackAreaCheck(3));
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
            StartCoroutine(AttackAreaCheck(countAttackPart));
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
    /// <summary>
    /// 攻擊區域檢查:檢查是否有擊中目標
    /// </summary>
    /// <param name="indexAttack"></param>
    /// <returns></returns>
    private IEnumerator AttackAreaCheck(int indexAttack)
    {
        yield return new WaitForSeconds(delaySendAttackToTarget[indexAttack]);

        Collider[] hits = Physics.OverlapBox(transform.position +
            transform.right * areaAttackOffst[indexAttack].x +
            transform.up * areaAttackOffst[indexAttack].y +
            transform.forward * areaAttackOffst[indexAttack].z,
            areaAttackSize[indexAttack] / 2, Quaternion.identity, 1 << 6);         //更換要攻擊的圖層

        hits[0].GetComponent<DamageSystem>().Damage(attack[indexAttack]);
    }

    private void RestorAttackParCountToZero()
    {
        countAttackPart = 0;
        ani.SetInteger(parAttackPart, countAttackPart);
    }

    #endregion
}

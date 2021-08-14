using UnityEngine;
/// <summary>
/// 敵人基底類別
/// 功能: 隨機走動 等待 追蹤玩家 受傷 死亡 - 狀態檢查
/// 狀態機: 列舉Enum, 判斷式, switch (基礎語法)
/// </summary>
public class BaseEnemy : MonoBehaviour
{
    #region 欄位
    [Header("基本能力")]
    [Range(50, 5000)]
    public float hp = 100;
    [Range(5, 1000)]
    public float attack = 20;
    [Range(1, 1000)]
    public float speed = 1.5f;

    //將私人欄位顯示在屬性面板上
    [SerializeField]
    private StateEnemy state;

    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;

    /// <summary>
    /// 隨機等待範圍與隨機走路範圍
    /// </summary>
    public Vector2 v2RandomIdle = new Vector2(1, 5);
    public Vector2 v2RandomWalk = new Vector2(3, 6);

    /// <summary>
    /// 等待時間隨機
    /// </summary>
    private float timeIdle;

    /// <summary>
    /// 等待用計時器
    /// </summary>
    private float timeridle;

    /// <summary>
    /// 走路時間隨機
    /// </summary>
    private float timeWalk;
    /// <summary>
    /// 走路計時器
    /// </summary>
    private float timerWalk;

   






    #endregion

    #region 事件
    private void Start()
    {
        #region 取得元件
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        #endregion

        #region 初始值設定
        timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
        #endregion
    }

    private void Update()
    {
        ChackForward();
        CheckState();
    }

    private void FixedUpdate()
    {
        WalkInFixedUpdate();
    }

    #endregion

    [Header("檢查前發是否有障礙或是地板球體")]
    public Vector3 checkForwardOffest;
    [Range(0, 1)]
    public float checkForwardRadius = 0.3f;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.3f, 0.3f, 0.3f);
        //transform.right 當前物件的右方 (2D模式為前方 , 紅色箭頭)
        //transform.up 當前物件的上方 (綠色箭頭)
        Gizmos.DrawSphere(transform.position +  transform.right * checkForwardOffest.x + transform.up * checkForwardOffest.y, checkForwardRadius); 
    }

    public int[] scores;
    public Collider2D[] hits;

    #region 方法
    /// <summary>
    /// 檢查前方: 是否有地板或是障礙物
    /// </summary>
    private void ChackForward()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position + transform.right * checkForwardOffest.x + transform.up * checkForwardOffest.y, checkForwardRadius);
        print("前方碰到的物件" + hit.name);
    }
    /// <summary>
    /// 檢查狀態
    /// </summary>
    private void CheckState()
    {
        switch (state)
        {
            case StateEnemy.idle:
                Idle();
                break;
            case StateEnemy.walk:
                Walk();
                break;
            case StateEnemy.track:
                break;
            case StateEnemy.attack:
                break;
            case StateEnemy.dead:
                break;
            
        }
    }

    /// <summary>
    /// 等待 : 隨機數秒後進入走路狀態
    /// 判斷後切至走路狀態
    /// </summary>
    private void Idle()
    {
        if (timeridle < timeIdle)                                        //如果計時器 < 等待時間
        {
            timeridle += Time.deltaTime;                                 //累加時間
            ani.SetBool("走路開關", false);                              //關閉走路開關:等待動畫
           
        }

        else                                                             //否則  
        {
            RandomDirection();                                           //隨機方向
            state = StateEnemy.walk;                                     //切換狀態
            timeWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);     // 取得走路時間
            timeridle = 0;                                               //計時器歸0
        }
    }

    /// <summary>
    /// 隨機走路
    /// </summary>
    private void Walk()
    {
        
        if (timerWalk < timeWalk)
        {
           
            timeridle += Time.deltaTime;
            ani.SetBool("走路開關", true);                             //關閉走路開關: 走路動畫
        }
        else
        {
            
            state = StateEnemy.idle;
            rig.velocity = Vector2.zero;
            timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
            timerWalk = 0;
        }
    }
    /// <summary>
    /// 將物理行為單獨處理並在FixedUpdate呼叫
    /// </summary>
    private void WalkInFixedUpdate()
    {
        //如果目前狀態是移動,就鋼體.加速度 = 右邊 * 速度 * 1/50 + 上方 * 地心引力
        if (state == StateEnemy.walk) rig.velocity = transform.right * speed  * Time.deltaTime  + Vector3.up * rig.velocity.y; 
    }
    /// <summary>
    /// 隨機方向 : 隨機面向左邊或右邊
    /// 右邊: 0 , 0, 0
    /// 左邊: 0, 180, 0
    /// </summary>
    private void RandomDirection()
    {
        int random = Random.Range(0, 2);                  //隨機.範圍(最小 , 最大) - 整數時不包含最大值(0 , 2) - 隨機取得(0 , 1)
        if (random == 0) transform.eulerAngles = Vector2.up * 180;
        else transform.eulerAngles = Vector2.zero;
    }

    #endregion
}

//定義列舉
//1. 使用關鍵字 enum 定義列舉以及包含的選項,可以在額外定義
//2. 需要有一個欄位定義為此列舉類型
//語法:修飾詞 enum 列舉名稱(選項1 選項2......選項N))

public enum StateEnemy
{
    idle,walk,track,attack,dead
}

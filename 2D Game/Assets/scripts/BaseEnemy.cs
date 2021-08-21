using UnityEngine;
using System.Linq;

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
    protected StateEnemy state;

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
    /// <summary>
    /// 玩家類別
    /// </summary>
    protected Player player;

    /// <summary>
    /// 攻擊區域的碰撞:保存玩家是否進入以及玩家碰撞資訊
    /// </summary>
    protected Collider2D hit;

    [Header("掉落道具資料:道具 機率")]
    public GameObject goProp;
    [Range(0, 1)]
    public float propProbability = 0.3f;

    #region 事件
    private void Start()
    {
        #region 取得元件
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        player = GameObject.Find("玩家").GetComponent<Player>();
        #endregion

        #region 初始值設定
        timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
        #endregion
    }

    protected virtual void Update()
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

    //陣列:保存相同類型資料表格,擁有編號與值2份資料
    //陣列語法:類型[]
    [Header("攻擊延遲 可自行設定數量"), Range(0, 5)]
    public float[] attacksDelay;
    [Header("攻擊多久回復原本狀態"), Range(0, 5)]
    public float afterAttackRestoreOriginal = 1;

    //父類別的成員如果希望子類別複寫必須遵守:
    //1.修飾詞必須是 public 或 protected - 保護 允許子類別存取
    //2.添加關鍵字 virtual 虛擬 - 允許子類別複寫
    //3.子類別使用 override 複寫

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.3f, 0.3f, 0.3f);
        //transform.right 當前物件的右方 (2D模式為前方 , 紅色箭頭)
        //transform.up 當前物件的上方 (綠色箭頭)
        Gizmos.DrawSphere(transform.position +  transform.right * checkForwardOffest.x + transform.up * checkForwardOffest.y, checkForwardRadius); 
    }

    //認識陣列
    //語法: 類型後方加上中誇號,例如 int[] float[] string[] Vector2[]
    public int[] scores;
    
    public Collider2D[] hits;
    /// <summary>
    /// 存放前方是否有不包含地板或跳台的物件
    /// </summary>
    public Collider2D[] hitsResult;

    #region 方法
    /// <summary>
    /// 檢查前方: 是否有地板或是障礙物
    /// </summary>
    private void ChackForward()
    {
        hits = Physics2D.OverlapCircleAll(transform.position + transform.right * checkForwardOffest.x + transform.up * checkForwardOffest.y, checkForwardRadius);


        //兩種情況都要轉向,避免撞到障礙物以及掉落
        //1.列陣內有 不是地板 並且 不是跳台的物件 = 有障礙物
        //陣列是空的 = 沒有地方站,會掉落
        //查詢語言 LinQ : 可查詢列陣資料,例如:是否包含地板,是否有資料.......
        hitsResult = hits.Where(x => x.name !="地板" && x.name !="跳台" && x.name !="玩家" && x.name !="可穿越跳台測試").ToArray();

        //列陣為空值:列陣數量為0
        //如果 碰撞數量為0 (前方沒有地方站立) 或著 碰撞結果大於0 (前方有障礙物) 都要轉向
        if (hits.Length ==0 || hitsResult.Length > 0)
        {
            TurnDirection();
        }
    }


    /// <summary>
    /// 轉向
    /// </summary>
    private void TurnDirection()
    {
        float y = transform.eulerAngles.y;
        if (y == 0) transform.eulerAngles = Vector3.up * 180;
        else transform.eulerAngles = Vector3.zero;
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
                Attack();
                break;
            case StateEnemy.dead:
                break;
            
        }
    }

    [Range(0.5f , 5)]
    /// <summary>
    /// 攻擊冷卻時間
    /// </summary>
    public float cdAttack = 3;
    private float timerAttack;
    /// <summary>
    /// 攻擊狀態:執行攻擊並添加冷卻
    /// </summary>
    private void Attack()
    {
        if (timerAttack < cdAttack)
        {
            timerAttack += Time.deltaTime;

        }
        else
        {
            AttackMethod();
        }
    }

    /// <summary>
    /// 子類別可以決定該如何攻擊的方法
    /// </summary>
    protected virtual void AttackMethod()
    {
        timerAttack = 0;
        ani.SetTrigger("攻擊觸發");
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
           
            timerWalk += Time.deltaTime;
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

    #region 方法:公開
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage"></param>
    public void Hurt(float damage)
    {
        hp -= damage;
        ani.SetTrigger("受傷觸發");
        if (hp <= 0) Dead();
    }
    /// <summary>
    /// 死亡
    /// </summary>
    public void Dead()
    {
        hp = 0;
        ani.SetBool("死亡", true);
        state = StateEnemy.dead;
        GetComponent<CapsuleCollider2D>().enabled = false;   //關閉碰撞器
        rig.velocity = Vector3.zero;                         //加速度歸零
        rig.constraints = RigidbodyConstraints2D.FreezeAll;  //鋼體凍結全部
        Dropprop();

        enabled = false;

    }

    private void Dropprop()
    {
        if(Random.value <= propProbability)
        {
            Instantiate(goProp, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        }
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

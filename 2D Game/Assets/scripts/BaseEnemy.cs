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
        
    }

    #endregion

    #region 方法
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
           
        }
        else                                                             //否則  
        {
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
            
        }
        else
        {
            state = StateEnemy.idle;
            timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
            timerWalk = 0;
        }
    }

    private void WalkInFixedUpdate()
    {
        if(state == StateEnemy.walk) rig.velocity = transform.right * speed * Time.deltaTime; 
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

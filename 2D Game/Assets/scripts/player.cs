using UnityEngine;
using UnityEngine.UI;                          //引用 介面 API
public class Player : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("跳躍高度"), Range(0, 3000)]
    public int hight = 100;
    [Header("血量"), Range(0, 100)]
    public float HP = 100;
    [Header("是否在地面上"), Tooltip("用來儲存腳色是否在地板上的資訊 在地板上 true 不在地板上 false")]
    public bool isGround;

    //私人欄位不顯示
    //開啟屬性面板除錯模式 Debug 可以看到私人欄位
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;

    #endregion


    #region 事件
    /// <summary>
    /// 文字血量
    /// </summary>
    private Text textHP;
    /// <summary>
    /// 血條
    /// </summary>
    private Image imgHP;
    /// <summary>
    /// 血量最大值:保存最大血量
    /// </summary>
    private float Hpmax;

    private void Start()
    {
        //GetComponent <類型> 泛行方法.可以指定任何類型

        //作用:取得此物件的2D鋼體元件
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        textHP = GameObject.Find("文字血量").GetComponent<Text>();
        imgHP = GameObject.Find("血條").GetComponent<Image>();
    }

    #endregion

    #region 方法
    //一秒約執行60次
    private void Update()
    {
        GetplayerInputHorizontal();
        TurnDirection();
        Jump();
        Attack();
    }


    //固定更新事件
    //一秒鐘固定執行50次    *官方建議有使用到物理 API 要在此事件內執行
    private void FixedUpdate()
    {
        Move(hValue);
    }

    [Header("檢查地板半徑: 位移與半徑")]
    public Vector3 groundoffest;
    [Range(0, 2)]
    public float groundRadius = 0.5f;

    //繪製圖示事件 : 輔助開發者用,僅會顯示在編輯器 Unity 內
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);     //半透明紅色
        Gizmos.DrawSphere(transform.position + groundoffest,groundRadius); //繪製球體(中心點.半徑)
    }
    /// <summary>
    /// 玩家水平輸入值
    /// </summary>
    private float hValue;

    /// <summary>
    /// 取得玩家輸入水平軸值:A,D,左,右
    /// </summary>
    private void GetplayerInputHorizontal()
    {

        //水平值 = 輸入.取得軸向(軸向名稱)
        //作用:取得玩家按下水平按鍵的值 , 按右為1 , 按左為-1 , 不按為0
        hValue = Input.GetAxis("Horizontal");
        //print("玩家水平值" + hValue);  註解:用於了解是否取得資訊,可以刪除

    }

    [Header("重力"), Range(0.01f, 1)]
    public float gravity = 1;

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="horizontal">左右水平值</param>
    private void Move(float horizontal)
    {
        /** 自訂重力的移動方式
        //區域變數:在方法內的欄位,有區域性,僅限此方法內存取
        //transform: 此物件的 transform 變形元件
        //posMove: 角色當前座標 + 玩家輸入的水平值
        //鋼體.移動座標(要前往的座標)
        //Time.fixedDeltaTime 指 1/50 秒
        Vector2 posMove = transform.position + new Vector3(horizontal, -gravity, 0) * speed * Time.fixedDeltaTime;

       
        rig.MovePosition(posMove);
        */

        rig.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rig.velocity.y);

        ani.SetBool("走路開關", horizontal !=0);
    }
    /// <summary>
    /// 選轉方向 :處理角色面向問題 , 按右角度 0 按左角度 180
    /// </summary>
    private void TurnDirection()
    {
        print("玩家按下右" + Input.GetKeyDown(KeyCode.D));

        //如果按 D 就將角度設為(0,0,0)
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = Vector3.zero;
        }

        //如果按 A 就將角度設為(0,180,0)
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        //Vector2 參數可以使用 Vector3 代入,程式會自動把Z軸取消
        // << 位移運算子
        //指定圖層語法 : 1 << 圖層編號
        Collider2D hit =  Physics2D.OverlapCircle(transform.position + groundoffest, groundRadius, 1 << 6);

        //如果碰到物件存在就代表在地面上 , 否則 代表不在地面上
        //判斷式如果只有一個結束符號 ; 可以省略大括號
        //簡寫 if(hit) isGround = true;
        //     else isGround = false;
        if (hit)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        //設定動畫參數,與是否在地板上相反
        ani.SetBool("跳躍開關", !isGround);

        //如果玩家按下 空白鍵 角色就往上跳躍
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(new Vector2(0, hight));
        }
    }

    [Header("攻擊冷卻"), Range(0, 2)]
    public float cd = 0.8f;

    /// <summary>
    /// 攻擊計時器
    /// </summary>
    private float timer;

    /// <summary>
    /// 是否攻擊
    /// </summary>
    private bool isAttack;

    


    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        //如果不是攻擊中 並且 按下左鍵 才可以攻擊 啟動觸發參數
        if (!isAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttack = true;
            ani.SetTrigger("攻擊觸發");

            
        }

        //如果按下攻擊左鍵 則開始累加時間
        if (isAttack)
        {
            if (timer < cd)
            {
                timer += Time.deltaTime;
            }

            else
            {
                timer = 0;
                isAttack = false;

            }
            
        }
    }
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">損傷值</param>
    public void Hurt(float damage)
    {
        HP -= damage;          //血量扣除傷害值

        if (HP <= 0) Death();  //如果血量<= 0 就會死

        textHP.text = "HP " + HP;        //文字血量.文字內容 = "HP" + 血量
        imgHP.fillAmount = HP / Hpmax;   //血條.填滿數值 = HP / hpmax
    }
    /// <summary>
    /// 死亡
    /// </summary>
    private void Death()
    {
        HP = 0;                        //血量歸零
        ani.SetBool("死亡", true);     //死亡動畫
        enabled = false;               //關閉此腳本
    }
    /// <summary>
    /// 吃道具
    /// </summary>
    /// <param name="prop">道具名稱</param>
    private void EatProp(string prop)
    {

    }
    #endregion


}

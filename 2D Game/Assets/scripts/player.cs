using UnityEngine;

public class player : MonoBehaviour
{
#region 欄位
[Header("移動速度"),Range(0,100)]
public float speed = 10.5f;
[Header("跳躍高度"),Range(0,3000)]
public int hight = 100;
[Header("血量"), Range(0,100)]
public float HP = 100;
[Header("是否在地面上"), Tooltip("用來儲存腳色是否在地板上的資訊 在地板上 true 不在地板上 false")]
public bool isGround;

private AudioSource aud;
private Rigidbody2D rig;
private Animator ani;




    #endregion


    #region 事件

    private void Start()
    {
        //GetComponent <類型> 泛行方法.可以指定任何類型
        //作用:取得此物件的2D鋼體元件
        rig = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region 方法
    //一秒約執行60次
    private void Update()
    {
        GetplayerInputHorizontal();
        TurnDirection();
        Jump();
    }

    //固定更新事件
    //一秒鐘固定執行50次    *官方建議有使用到物理 API 要在此事件內執行
    private void FixedUpdate()
    {
        Move(hValue);
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
        //區域變數:在方法內的欄位,有區域性,僅限此方法內存取
        //transform: 此物件的 transform 變形元件
        //posMove: 角色當前座標 + 玩家輸入的水平值
        //鋼體.移動座標(要前往的座標)
        //Time.fixedDeltaTime 指 1/50 秒
        Vector2 posMove = transform.position + new Vector3(horizontal, -gravity, 0) * speed * Time.fixedDeltaTime;
        
        rig.MovePosition(posMove);

    }

    private void TurnDirection()
    {
        print("玩家按下右" + Input.GetKeyDown(KeyCode.D));

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        //如果玩家按下 空白鍵 角色就往上跳躍
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(new Vector2(0, hight));
        }
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {

    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">損傷值</param>
    public void Hurt(float damage)
    {

    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Death()
    {

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

using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("血量"), Range(0, 500)]
    public float HP = 200;
    

    //私人欄位不顯示
    //開啟屬性面板除錯模式 Debug 可以看到私人欄位
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;

    [Header("攻擊區域的位移與大小")]
    public Vector2 checkattackoffset;
    public Vector3 checkattacksize;

    private float hValue;

    #endregion


    private void Start()
    {
        //GetComponent <類型> 泛行方法.可以指定任何類型

        //作用:取得此物件的2D鋼體元件
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        
    }

    private void Update()
    {
        GetPlayerInputHorizontal();
    }

    private void FixedUpdate()
    {
        Move(hValue);
    }


    private void GetPlayerInputHorizontal()
    {
        hValue = Input.GetAxis("Horizontal");

    }
    private void Move(float horizontal)
    {
        
        //區域變數:在方法內的欄位,有區域性,僅限此方法內存取
        //transform: 此物件的 transform 變形元件
        //posMove: 角色當前座標 + 玩家輸入的水平值
        //鋼體.移動座標(要前往的座標)
        //Time.fixedDeltaTime 指 1/50 秒
        Vector2 posMove = transform.position + new Vector3(horizontal,  0, 0) * speed * Time.fixedDeltaTime;

       
        rig.MovePosition(posMove);
        

       

        ani.SetBool("走路開關", horizontal != 0);
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
}

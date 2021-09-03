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

    
    public Vector2 movement;
    

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + movement * speed * Time.fixedDeltaTime);
   
    }
}

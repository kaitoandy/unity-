using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    #region 欄位:公開
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("血量"), Range(0, 500)]
    public float HP = 200;

    [Header("位移")]
    public Vector2 movement;

    [Header("發射火球")]
    public GameObject fire;
    public float fireSpeed = 8f;

    #endregion

    #region 欄位:私人
    //私人欄位不顯示
    //開啟屬性面板除錯模式 Debug 可以看到私人欄位

    /// <summary>
    /// 文字血量
    /// </summary>
    private Text textHp;
    /// <summary>
    /// 血條
    /// </summary>
    private Image imgHp;
    /// <summary>
    /// 血量最大值
    /// </summary>
    private float hpMax;

    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;

    
    

    private float hValue;

    #endregion


    private void Start()
    {
        //GetComponent <類型> 泛行方法.可以指定任何類型

        //作用:取得此物件的2D鋼體元件
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        textHp = GameObject.Find("血量文字").GetComponent<Text>();
        imgHp = GameObject.Find("血條").GetComponent<Image>();

        
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Move();
        FireAttack();

    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + movement * speed * Time.fixedDeltaTime);
        


    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ani.SetBool("往右走", true);
        }
        else ani.SetBool("往右走", false);

        if (Input.GetKeyDown(KeyCode.A))
        {
            ani.SetBool("往左走", true);
        }
        else ani.SetBool("往左走", false);
        if (Input.GetKeyDown(KeyCode.W))
        {
            ani.SetBool("往上走", true);
        }
        else ani.SetBool("往上走", false);

        if (Input.GetKeyDown(KeyCode.S))
        {
            ani.SetBool("往下走", true);
        }
        else ani.SetBool("往下走", false);
    }

    private void FireAttack()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(fire, transform.position + Vector3.right * 1.5f , Quaternion.identity);
            fire.transform.Translate(1, 0, 0 * fireSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(fire, transform.position + Vector3.up * 1.5f , Quaternion.identity);
            fire.transform.Translate(0, 1, 0 * fireSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.K))
            {
                Instantiate(fire, transform.position + Vector3.down * 1.5f , Quaternion.identity);
                fire.transform.Translate(-1, 0, 0 * fireSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.J))
            {
                Instantiate(fire, transform.position + Vector3.left * 1.5f  , Quaternion.identity);
                 
            }
        }

    
    public void Hurt(float damage)
    {
        HP -= damage;

        if (HP <= 0) Dead();

        textHp.text = "HP" + HP;
        imgHp.fillAmount = HP / hpMax;

    }


    private void Dead()
    {
        HP = 0;
        ani.SetBool("死亡開關", true);

        enabled = false;

    }
}

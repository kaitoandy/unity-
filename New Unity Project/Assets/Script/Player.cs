using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region ���:���}
    [Header("���ʳt��"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("��q"), Range(0, 500)]
    public float HP = 200;

    #endregion

    #region ���:�p�H
    //�p�H��줣���
    //�}���ݩʭ��O�����Ҧ� Debug �i�H�ݨ�p�H���

    /// <summary>
    /// ��r��q
    /// </summary>
    private Text textHp;
    /// <summary>
    /// ���
    /// </summary>
    private Image imgHp;
    /// <summary>
    /// ��q�̤j��
    /// </summary>
    private float hpMax;

    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;

    
    public Vector2 movement;
    

    private float hValue;

    #endregion


    private void Start()
    {
        //GetComponent <����> �x���k.�i�H���w��������

        //�@��:���o������2D���餸��
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        textHp = GameObject.Find("��q��r").GetComponent<Text>();
        imgHp = GameObject.Find("���").GetComponent<Image>();

        
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Move();
    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + movement * speed * Time.fixedDeltaTime);
        


    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ani.SetBool("���k��", true);
        }
        else ani.SetBool("���k��", false);

        if (Input.GetKeyDown(KeyCode.A))
        {
            ani.SetBool("������", true);
        }
        else ani.SetBool("������", false);
        if (Input.GetKeyDown(KeyCode.W))
        {
            ani.SetBool("���W��", true);
        }
        else ani.SetBool("���W��", false);

        if (Input.GetKeyDown(KeyCode.S))
        {
            ani.SetBool("���U��", true);
        }
        else ani.SetBool("���U��", false);
    }

    private void Hurt(float damage)
    {
        HP -= damage;

        if (HP <= 0) Dead();

        textHp.text = "HP" + HP;
        imgHp.fillAmount = HP / hpMax;

    }

    private void Dead()
    {
        HP = 0;
        ani.SetBool("���`�}��", true);

    }
}

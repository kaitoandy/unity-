using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    #region ���:���}
    [Header("���ʳt��"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("��q"), Range(0, 500)]
    public float HP = 200;

    [Header("�첾")]
    public Vector2 movement;

    [Header("�o�g���y")]
    public GameObject fire;
    public float fireSpeed = 8f;

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
        ani.SetBool("���`�}��", true);

        enabled = false;

    }
}

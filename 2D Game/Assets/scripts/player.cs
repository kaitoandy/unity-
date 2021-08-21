using UnityEngine;
using UnityEngine.UI;                          //�ޥ� ���� API
public class Player : MonoBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("���D����"), Range(0, 3000)]
    public int hight = 100;
    [Header("��q"), Range(0, 100)]
    public float HP = 100;
    [Header("�O�_�b�a���W"), Tooltip("�Ψ��x�s�}��O�_�b�a�O�W����T �b�a�O�W true ���b�a�O�W false")]
    public bool isGround;

    //�p�H��줣���
    //�}���ݩʭ��O�����Ҧ� Debug �i�H�ݨ�p�H���
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;

    #endregion


    #region �ƥ�
    /// <summary>
    /// ��r��q
    /// </summary>
    private Text textHP;
    /// <summary>
    /// ���
    /// </summary>
    private Image imgHP;
    /// <summary>
    /// ��q�̤j��:�O�s�̤j��q
    /// </summary>
    private float Hpmax;

    private void Start()
    {
        //GetComponent <����> �x���k.�i�H���w��������

        //�@��:���o������2D���餸��
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        textHP = GameObject.Find("��r��q").GetComponent<Text>();
        imgHP = GameObject.Find("���").GetComponent<Image>();
    }

    #endregion

    #region ��k
    //�@�������60��
    private void Update()
    {
        GetplayerInputHorizontal();
        TurnDirection();
        Jump();
        Attack();
    }


    //�T�w��s�ƥ�
    //�@�����T�w����50��    *�x���ĳ���ϥΨ쪫�z API �n�b���ƥ󤺰���
    private void FixedUpdate()
    {
        Move(hValue);
    }

    [Header("�ˬd�a�O�b�|: �첾�P�b�|")]
    public Vector3 groundoffest;
    [Range(0, 2)]
    public float groundRadius = 0.5f;

    //ø�s�ϥܨƥ� : ���U�}�o�̥�,�ȷ|��ܦb�s�边 Unity ��
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);     //�b�z������
        Gizmos.DrawSphere(transform.position + groundoffest,groundRadius); //ø�s�y��(�����I.�b�|)
    }
    /// <summary>
    /// ���a������J��
    /// </summary>
    private float hValue;

    /// <summary>
    /// ���o���a��J�����b��:A,D,��,�k
    /// </summary>
    private void GetplayerInputHorizontal()
    {

        //������ = ��J.���o�b�V(�b�V�W��)
        //�@��:���o���a���U�������䪺�� , ���k��1 , ������-1 , ������0
        hValue = Input.GetAxis("Horizontal");
        //print("���a������" + hValue);  ����:�Ω�F�ѬO�_���o��T,�i�H�R��

    }

    [Header("���O"), Range(0.01f, 1)]
    public float gravity = 1;

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="horizontal">���k������</param>
    private void Move(float horizontal)
    {
        /** �ۭq���O�����ʤ覡
        //�ϰ��ܼ�:�b��k�������,���ϰ��,�ȭ�����k���s��
        //transform: ������ transform �ܧΤ���
        //posMove: �����e�y�� + ���a��J��������
        //����.���ʮy��(�n�e�����y��)
        //Time.fixedDeltaTime �� 1/50 ��
        Vector2 posMove = transform.position + new Vector3(horizontal, -gravity, 0) * speed * Time.fixedDeltaTime;

       
        rig.MovePosition(posMove);
        */

        rig.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rig.velocity.y);

        ani.SetBool("�����}��", horizontal !=0);
    }
    /// <summary>
    /// �����V :�B�z���⭱�V���D , ���k���� 0 �������� 180
    /// </summary>
    private void TurnDirection()
    {
        print("���a���U�k" + Input.GetKeyDown(KeyCode.D));

        //�p�G�� D �N�N���׳]��(0,0,0)
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = Vector3.zero;
        }

        //�p�G�� A �N�N���׳]��(0,180,0)
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    /// <summary>
    /// ���D
    /// </summary>
    private void Jump()
    {
        //Vector2 �Ѽƥi�H�ϥ� Vector3 �N�J,�{���|�۰ʧ�Z�b����
        // << �첾�B��l
        //���w�ϼh�y�k : 1 << �ϼh�s��
        Collider2D hit =  Physics2D.OverlapCircle(transform.position + groundoffest, groundRadius, 1 << 6);

        //�p�G�I�쪫��s�b�N�N��b�a���W , �_�h �N���b�a���W
        //�P�_���p�G�u���@�ӵ����Ÿ� ; �i�H�ٲ��j�A��
        //²�g if(hit) isGround = true;
        //     else isGround = false;
        if (hit)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        //�]�w�ʵe�Ѽ�,�P�O�_�b�a�O�W�ۤ�
        ani.SetBool("���D�}��", !isGround);

        //�p�G���a���U �ť��� ����N���W���D
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(new Vector2(0, hight));
        }
    }

    [Header("�����N�o"), Range(0, 2)]
    public float cd = 0.8f;

    /// <summary>
    /// �����p�ɾ�
    /// </summary>
    private float timer;

    /// <summary>
    /// �O�_����
    /// </summary>
    private bool isAttack;

    


    /// <summary>
    /// ����
    /// </summary>
    private void Attack()
    {
        //�p�G���O������ �åB ���U���� �~�i�H���� �Ұ�Ĳ�o�Ѽ�
        if (!isAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttack = true;
            ani.SetTrigger("����Ĳ�o");

            
        }

        //�p�G���U�������� �h�}�l�֥[�ɶ�
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
    /// ����
    /// </summary>
    /// <param name="damage">�l�˭�</param>
    public void Hurt(float damage)
    {
        HP -= damage;          //��q�����ˮ`��

        if (HP <= 0) Death();  //�p�G��q<= 0 �N�|��

        textHP.text = "HP " + HP;        //��r��q.��r���e = "HP" + ��q
        imgHP.fillAmount = HP / Hpmax;   //���.�񺡼ƭ� = HP / hpmax
    }
    /// <summary>
    /// ���`
    /// </summary>
    private void Death()
    {
        HP = 0;                        //��q�k�s
        ani.SetBool("���`", true);     //���`�ʵe
        enabled = false;               //�������}��
    }
    /// <summary>
    /// �Y�D��
    /// </summary>
    /// <param name="prop">�D��W��</param>
    private void EatProp(string prop)
    {

    }
    #endregion


}

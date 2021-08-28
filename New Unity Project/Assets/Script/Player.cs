using UnityEngine;

public class Player : MonoBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("��q"), Range(0, 500)]
    public float HP = 200;
    

    //�p�H��줣���
    //�}���ݩʭ��O�����Ҧ� Debug �i�H�ݨ�p�H���
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;

    [Header("�����ϰ쪺�첾�P�j�p")]
    public Vector2 checkattackoffset;
    public Vector3 checkattacksize;

    private float hValue;

    #endregion


    private void Start()
    {
        //GetComponent <����> �x���k.�i�H���w��������

        //�@��:���o������2D���餸��
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
        
        //�ϰ��ܼ�:�b��k�������,���ϰ��,�ȭ�����k���s��
        //transform: ������ transform �ܧΤ���
        //posMove: �����e�y�� + ���a��J��������
        //����.���ʮy��(�n�e�����y��)
        //Time.fixedDeltaTime �� 1/50 ��
        Vector2 posMove = transform.position + new Vector3(horizontal,  0, 0) * speed * Time.fixedDeltaTime;

       
        rig.MovePosition(posMove);
        

       

        ani.SetBool("�����}��", horizontal != 0);
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
}

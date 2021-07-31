using UnityEngine;

public class player : MonoBehaviour
{
#region ���
[Header("���ʳt��"),Range(0,100)]
public float speed = 10.5f;
[Header("���D����"),Range(0,3000)]
public int hight = 100;
[Header("��q"), Range(0,100)]
public float HP = 100;
[Header("�O�_�b�a���W"), Tooltip("�Ψ��x�s�}��O�_�b�a�O�W����T �b�a�O�W true ���b�a�O�W false")]
public bool isGround;

private AudioSource aud;
private Rigidbody2D rig;
private Animator ani;




    #endregion


    #region �ƥ�

    private void Start()
    {
        //GetComponent <����> �x���k.�i�H���w��������
        //�@��:���o������2D���餸��
        rig = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region ��k
    //�@�������60��
    private void Update()
    {
        GetplayerInputHorizontal();
        TurnDirection();
        Jump();
    }

    //�T�w��s�ƥ�
    //�@�����T�w����50��    *�x���ĳ���ϥΨ쪫�z API �n�b���ƥ󤺰���
    private void FixedUpdate()
    {
        Move(hValue);
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
        //�ϰ��ܼ�:�b��k�������,���ϰ��,�ȭ�����k���s��
        //transform: ������ transform �ܧΤ���
        //posMove: �����e�y�� + ���a��J��������
        //����.���ʮy��(�n�e�����y��)
        //Time.fixedDeltaTime �� 1/50 ��
        Vector2 posMove = transform.position + new Vector3(horizontal, -gravity, 0) * speed * Time.fixedDeltaTime;
        
        rig.MovePosition(posMove);

    }

    private void TurnDirection()
    {
        print("���a���U�k" + Input.GetKeyDown(KeyCode.D));

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
    /// ���D
    /// </summary>
    private void Jump()
    {
        //�p�G���a���U �ť��� ����N���W���D
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(new Vector2(0, hight));
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Attack()
    {

    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage">�l�˭�</param>
    public void Hurt(float damage)
    {

    }

    /// <summary>
    /// ���`
    /// </summary>
    private void Death()
    {

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

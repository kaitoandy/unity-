using UnityEngine;
/// <summary>
/// �ĤH�����O
/// �\��: �H������ ���� �l�ܪ��a ���� ���` - ���A�ˬd
/// ���A��: �C�|Enum, �P�_��, switch (��¦�y�k)
/// </summary>
public class BaseEnemy : MonoBehaviour
{
    #region ���
    [Header("�򥻯�O")]
    [Range(50, 5000)]
    public float hp = 100;
    [Range(5, 1000)]
    public float attack = 20;
    [Range(1, 1000)]
    public float speed = 1.5f;

    //�N�p�H�����ܦb�ݩʭ��O�W
    [SerializeField]
    private StateEnemy state;

    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;

    /// <summary>
    /// �H�����ݽd��P�H�������d��
    /// </summary>
    public Vector2 v2RandomIdle = new Vector2(1, 5);
    public Vector2 v2RandomWalk = new Vector2(3, 6);

    /// <summary>
    /// ���ݮɶ��H��
    /// </summary>
    private float timeIdle;

    /// <summary>
    /// ���ݥέp�ɾ�
    /// </summary>
    private float timeridle;

    /// <summary>
    /// �����ɶ��H��
    /// </summary>
    private float timeWalk;
    /// <summary>
    /// �����p�ɾ�
    /// </summary>
    private float timerWalk;

   






    #endregion

    #region �ƥ�
    private void Start()
    {
        #region ���o����
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        #endregion

        #region ��l�ȳ]�w
        timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
        #endregion
    }

    private void Update()
    {
        ChackForward();
        CheckState();
    }

    private void FixedUpdate()
    {
        WalkInFixedUpdate();
    }

    #endregion

    [Header("�ˬd�e�o�O�_����ê�άO�a�O�y��")]
    public Vector3 checkForwardOffest;
    [Range(0, 1)]
    public float checkForwardRadius = 0.3f;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.3f, 0.3f, 0.3f);
        //transform.right ��e���󪺥k�� (2D�Ҧ����e�� , ����b�Y)
        //transform.up ��e���󪺤W�� (���b�Y)
        Gizmos.DrawSphere(transform.position +  transform.right * checkForwardOffest.x + transform.up * checkForwardOffest.y, checkForwardRadius); 
    }

    public int[] scores;
    public Collider2D[] hits;

    #region ��k
    /// <summary>
    /// �ˬd�e��: �O�_���a�O�άO��ê��
    /// </summary>
    private void ChackForward()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position + transform.right * checkForwardOffest.x + transform.up * checkForwardOffest.y, checkForwardRadius);
        print("�e��I�쪺����" + hit.name);
    }
    /// <summary>
    /// �ˬd���A
    /// </summary>
    private void CheckState()
    {
        switch (state)
        {
            case StateEnemy.idle:
                Idle();
                break;
            case StateEnemy.walk:
                Walk();
                break;
            case StateEnemy.track:
                break;
            case StateEnemy.attack:
                break;
            case StateEnemy.dead:
                break;
            
        }
    }

    /// <summary>
    /// ���� : �H���Ƭ��i�J�������A
    /// �P�_����ܨ������A
    /// </summary>
    private void Idle()
    {
        if (timeridle < timeIdle)                                        //�p�G�p�ɾ� < ���ݮɶ�
        {
            timeridle += Time.deltaTime;                                 //�֥[�ɶ�
            ani.SetBool("�����}��", false);                              //���������}��:���ݰʵe
           
        }

        else                                                             //�_�h  
        {
            RandomDirection();                                           //�H����V
            state = StateEnemy.walk;                                     //�������A
            timeWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);     // ���o�����ɶ�
            timeridle = 0;                                               //�p�ɾ��k0
        }
    }

    /// <summary>
    /// �H������
    /// </summary>
    private void Walk()
    {
        
        if (timerWalk < timeWalk)
        {
           
            timeridle += Time.deltaTime;
            ani.SetBool("�����}��", true);                             //���������}��: �����ʵe
        }
        else
        {
            
            state = StateEnemy.idle;
            rig.velocity = Vector2.zero;
            timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
            timerWalk = 0;
        }
    }
    /// <summary>
    /// �N���z�欰��W�B�z�æbFixedUpdate�I�s
    /// </summary>
    private void WalkInFixedUpdate()
    {
        //�p�G�ثe���A�O����,�N����.�[�t�� = �k�� * �t�� * 1/50 + �W�� * �a�ߤޤO
        if (state == StateEnemy.walk) rig.velocity = transform.right * speed  * Time.deltaTime  + Vector3.up * rig.velocity.y; 
    }
    /// <summary>
    /// �H����V : �H�����V����Υk��
    /// �k��: 0 , 0, 0
    /// ����: 0, 180, 0
    /// </summary>
    private void RandomDirection()
    {
        int random = Random.Range(0, 2);                  //�H��.�d��(�̤p , �̤j) - ��Ʈɤ��]�t�̤j��(0 , 2) - �H�����o(0 , 1)
        if (random == 0) transform.eulerAngles = Vector2.up * 180;
        else transform.eulerAngles = Vector2.zero;
    }

    #endregion
}

//�w�q�C�|
//1. �ϥ�����r enum �w�q�C�|�H�Υ]�t���ﶵ,�i�H�b�B�~�w�q
//2. �ݭn���@�����w�q�����C�|����
//�y�k:�׹��� enum �C�|�W��(�ﶵ1 �ﶵ2......�ﶵN))

public enum StateEnemy
{
    idle,walk,track,attack,dead
}

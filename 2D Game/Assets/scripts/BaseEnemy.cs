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
        
    }

    #endregion

    #region ��k
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
           
        }
        else                                                             //�_�h  
        {
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
            
        }
        else
        {
            state = StateEnemy.idle;
            timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
            timerWalk = 0;
        }
    }

    private void WalkInFixedUpdate()
    {
        if(state == StateEnemy.walk) rig.velocity = transform.right * speed * Time.deltaTime; 
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

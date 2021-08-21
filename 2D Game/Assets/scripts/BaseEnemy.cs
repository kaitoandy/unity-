using UnityEngine;
using System.Linq;

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
    protected StateEnemy state;

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
    /// <summary>
    /// ���a���O
    /// </summary>
    protected Player player;

    /// <summary>
    /// �����ϰ쪺�I��:�O�s���a�O�_�i�J�H�Ϊ��a�I����T
    /// </summary>
    protected Collider2D hit;

    [Header("�����D����:�D�� ���v")]
    public GameObject goProp;
    [Range(0, 1)]
    public float propProbability = 0.3f;

    #region �ƥ�
    private void Start()
    {
        #region ���o����
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        player = GameObject.Find("���a").GetComponent<Player>();
        #endregion

        #region ��l�ȳ]�w
        timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
        #endregion
    }

    protected virtual void Update()
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

    //�}�C:�O�s�ۦP������ƪ��,�֦��s���P��2�����
    //�}�C�y�k:����[]
    [Header("�������� �i�ۦ�]�w�ƶq"), Range(0, 5)]
    public float[] attacksDelay;
    [Header("�����h�[�^�_�쥻���A"), Range(0, 5)]
    public float afterAttackRestoreOriginal = 1;

    //�����O�������p�G�Ʊ�l���O�Ƽg������u:
    //1.�׹��������O public �� protected - �O�@ ���\�l���O�s��
    //2.�K�[����r virtual ���� - ���\�l���O�Ƽg
    //3.�l���O�ϥ� override �Ƽg

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.3f, 0.3f, 0.3f);
        //transform.right ��e���󪺥k�� (2D�Ҧ����e�� , ����b�Y)
        //transform.up ��e���󪺤W�� (���b�Y)
        Gizmos.DrawSphere(transform.position +  transform.right * checkForwardOffest.x + transform.up * checkForwardOffest.y, checkForwardRadius); 
    }

    //�{�Ѱ}�C
    //�y�k: �������[�W���ظ�,�Ҧp int[] float[] string[] Vector2[]
    public int[] scores;
    
    public Collider2D[] hits;
    /// <summary>
    /// �s��e��O�_�����]�t�a�O�θ��x������
    /// </summary>
    public Collider2D[] hitsResult;

    #region ��k
    /// <summary>
    /// �ˬd�e��: �O�_���a�O�άO��ê��
    /// </summary>
    private void ChackForward()
    {
        hits = Physics2D.OverlapCircleAll(transform.position + transform.right * checkForwardOffest.x + transform.up * checkForwardOffest.y, checkForwardRadius);


        //��ر��p���n��V,�קK�����ê���H�α���
        //1.�C�}���� ���O�a�O �åB ���O���x������ = ����ê��
        //�}�C�O�Ū� = �S���a�诸,�|����
        //�d�߻y�� LinQ : �i�d�ߦC�}���,�Ҧp:�O�_�]�t�a�O,�O�_�����.......
        hitsResult = hits.Where(x => x.name !="�a�O" && x.name !="���x" && x.name !="���a" && x.name !="�i��V���x����").ToArray();

        //�C�}���ŭ�:�C�}�ƶq��0
        //�p�G �I���ƶq��0 (�e��S���a�诸��) �ε� �I�����G�j��0 (�e�観��ê��) ���n��V
        if (hits.Length ==0 || hitsResult.Length > 0)
        {
            TurnDirection();
        }
    }


    /// <summary>
    /// ��V
    /// </summary>
    private void TurnDirection()
    {
        float y = transform.eulerAngles.y;
        if (y == 0) transform.eulerAngles = Vector3.up * 180;
        else transform.eulerAngles = Vector3.zero;
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
                Attack();
                break;
            case StateEnemy.dead:
                break;
            
        }
    }

    [Range(0.5f , 5)]
    /// <summary>
    /// �����N�o�ɶ�
    /// </summary>
    public float cdAttack = 3;
    private float timerAttack;
    /// <summary>
    /// �������A:��������òK�[�N�o
    /// </summary>
    private void Attack()
    {
        if (timerAttack < cdAttack)
        {
            timerAttack += Time.deltaTime;

        }
        else
        {
            AttackMethod();
        }
    }

    /// <summary>
    /// �l���O�i�H�M�w�Ӧp���������k
    /// </summary>
    protected virtual void AttackMethod()
    {
        timerAttack = 0;
        ani.SetTrigger("����Ĳ�o");
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
           
            timerWalk += Time.deltaTime;
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

    #region ��k:���}
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage"></param>
    public void Hurt(float damage)
    {
        hp -= damage;
        ani.SetTrigger("����Ĳ�o");
        if (hp <= 0) Dead();
    }
    /// <summary>
    /// ���`
    /// </summary>
    public void Dead()
    {
        hp = 0;
        ani.SetBool("���`", true);
        state = StateEnemy.dead;
        GetComponent<CapsuleCollider2D>().enabled = false;   //�����I����
        rig.velocity = Vector3.zero;                         //�[�t���k�s
        rig.constraints = RigidbodyConstraints2D.FreezeAll;  //����ᵲ����
        Dropprop();

        enabled = false;

    }

    private void Dropprop()
    {
        if(Random.value <= propProbability)
        {
            Instantiate(goProp, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        }
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

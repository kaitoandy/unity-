using UnityEngine;
using System.Linq;
/// <summary>
/// �ĤH��
/// </summary>
public class BaseEnemy : MonoBehaviour
{
    #region ���:���}
    [Header("��q"), Range(0, 1000)]
    public float HP = 100;
    [Header("�����O"), Range(0, 500)]
    public float attack = 20;
    [Header("�t��"), Range(0, 100)]
    public float speed = 1.5f;

    public Vector2 v2RandomIdle = new Vector2(1, 5);
    public Vector2 v2RandomWalk = new Vector2(3, 6);

    public Vector2 movement;

    [Header("�ˬd�e�誫��")]
    public Vector3 checkForwardOffest;
    [Range(0, 1)]
    public float checkForwardRadius;

    public Collider2D[] hits;

    public Collider2D[] hitsReault;

    [Header("�����N�o")]
    public float cdAttack = 1;

    [Header("�Ĥ@���������"),Range(0.5f,5)]
    public float attackDelayFirs = 0.5f;





    #endregion

    #region ���:�p�H
    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;

    private float timeIdle;       //���ݮɶ�
    private float timerIdle;      //���ݭp�ɾ�

    private float timeWalk;       //�����ɶ�
    private float timerWalk;      //�����p�ɾ�

    //���O�i�H��ܨp�H���
    [SerializeField]
    protected StateEnemy state;

    private float timerAttack;

    protected Player player;

    #endregion

    #region �ƥ�

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        player = GameObject.Find("���a").GetComponent<Player>();

        timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
    }


    protected virtual void Update()
    {
        CheckForward();
        CheckState();

    }

    private void FixedUpdate()
    {
        WalkInFixedUpdate();
        
        rig.MovePosition(rig.position + movement * speed * Time.fixedDeltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.3f, 0.3f, 0.3f);
        Gizmos.DrawSphere(transform.position  + 
            transform.right * checkForwardOffest.x +
            transform.up * checkForwardOffest.y, checkForwardRadius);
    }

    private void CheckForward()
    {
         hits = Physics2D.OverlapCircleAll(transform.position +
            transform.right * checkForwardOffest.x +
            transform.up * checkForwardOffest.y, checkForwardRadius);

        
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
                idle();
                break;
            case StateEnemy.walkUp:
                walkUp();
                break;
            case StateEnemy.walkRight:
                walkRight();
                break;
            case StateEnemy.walkLift:
                walkLift();
                break;
            case StateEnemy.walkDown:
                walkDown();
                break;
            case StateEnemy.track:
                break;
            case StateEnemy.attack:
                Attack();
                break;
            case StateEnemy.dead:
                Dead();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ���ݨöi�J�H���|�訫�����A
    /// </summary>
    private void idle()
    {
        if (timerIdle < timeIdle )
        {
            timerIdle += Time.deltaTime;
            
            
        }
        else
        {
            state = StateEnemy.walkRight;
            timeWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            timerIdle = 0; 
        }

        if (timerIdle < timeIdle)
        {
            timerIdle += Time.deltaTime;


        }
        else
        {
            state = StateEnemy.walkUp;
            timeWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            timerIdle = 0;
        }

        if (timerIdle < timeIdle)
        {
            timerIdle += Time.deltaTime;


        }
        else
        {
            state = StateEnemy.walkLift;
            timeWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            timerIdle = 0;
        }

        if (timerIdle < timeIdle)
        {
            timerIdle += Time.deltaTime;


        }
        else
        {
            state = StateEnemy.walkDown;
            timeWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            timerIdle = 0;
        }
    }

    #region �H���|��V����
    private void walkRight()
    {
        if (timerWalk < timeWalk)
        {
            timerWalk += Time.deltaTime;
            rig.velocity = new Vector2 (movement.x = 1 * speed * Time.deltaTime, movement.y = 0 * speed * Time.deltaTime);
            
        }
        else
        {
            state = StateEnemy.idle;
            timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
            timerWalk = 0;
        }
    }

    private void walkUp()
    {
        if (timerWalk < timeWalk)
        {
            timerWalk += Time.deltaTime;
            rig.velocity = new Vector2(movement.x = 0 * speed * Time.deltaTime, movement.y = 1 * speed * Time.deltaTime);

        }
        else
        {
            state = StateEnemy.idle;
            timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
            timerWalk = 0;
        }
    }

    private void walkLift()
    {
        if (timerWalk < timeWalk)
        {
            timerWalk += Time.deltaTime;
            rig.velocity = new Vector2(movement.x = -1 * speed * Time.deltaTime, movement.y = 0 * speed * Time.deltaTime);


        }
        else
        {
            state = StateEnemy.idle;
            timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
            timerWalk = 0;
        }
    }

    private void walkDown()
    {
        if (timerWalk < timeWalk)
        {
            timerWalk += Time.deltaTime;
            rig.velocity = new Vector2(movement.x = 0 * speed * Time.deltaTime, movement.y = -1 * speed * Time.deltaTime);

        }
        else
        {
            state = StateEnemy.idle;
            timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
            timerWalk = 0;
        }
    }
    #endregion

    /// <summary>
    /// �]�w�����ʵe
    /// </summary>
    private void WalkInFixedUpdate()
    {
        if (state == StateEnemy.walkRight)
        {      
           ani.SetBool("���k��", true);
        }
        else ani.SetBool("���k��", false);

        if (state == StateEnemy.walkUp)
        {
            ani.SetBool("���W��", true);
        }
        else ani.SetBool("���W��", false);

        if (state == StateEnemy.walkLift)
        {
            ani.SetBool("������", true);
        }
        else ani.SetBool("������", false);

        if (state == StateEnemy.walkDown)
        {
            ani.SetBool("���U��", true);
        }
        else ani.SetBool("���U��", false);
    }

    
   

    private void Attack()
    {
        if (timerAttack < cdAttack)
        {
            timerAttack += Time.deltaTime;
        }
        else AttackMethod();
    }

    protected virtual void AttackMethod()
    {
        timerAttack = 0;

    }

    private void Dead()
    {
        HP = 0;
        ani.SetBool("���`�}��", true);
    }

    #endregion
}

public enum StateEnemy
{
    idle, walkUp, walkRight, walkLift, walkDown, track,attack,dead
}
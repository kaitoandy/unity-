using UnityEngine;
using System.Linq;
/// <summary>
/// 敵人基底
/// </summary>
public class BaseEnemy : MonoBehaviour
{
    #region 欄位:公開
    [Header("血量"), Range(0, 1000)]
    public float HP = 100;
    [Header("攻擊力"), Range(0, 500)]
    public float attack = 20;
    [Header("速度"), Range(0, 100)]
    public float speed = 1.5f;

    public Vector2 v2RandomIdle = new Vector2(1, 5);
    public Vector2 v2RandomWalk = new Vector2(3, 6);

    public Vector2 movement;

    [Header("檢查前方物體")]
    public Vector3 checkForwardOffest;
    [Range(0, 1)]
    public float checkForwardRadius;

    public Collider2D[] hits;

    public Collider2D[] hitsReault;

    [Header("攻擊冷卻")]
    public float cdAttack = 1;

    [Header("第一次延遲攻擊"),Range(0.5f,5)]
    public float attackDelayFirs = 0.5f;





    #endregion

    #region 欄位:私人
    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;

    private float timeIdle;       //等待時間
    private float timerIdle;      //等待計時器

    private float timeWalk;       //走路時間
    private float timerWalk;      //走路計時器

    //面板可以顯示私人欄位
    [SerializeField]
    protected StateEnemy state;

    private float timerAttack;

    protected Player player;

    #endregion

    #region 事件

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        player = GameObject.Find("玩家").GetComponent<Player>();

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

    #region 方法
    /// <summary>
    /// 檢查狀態
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
    /// 等待並進入隨機四方走路狀態
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

    #region 隨機四方向走路
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
    /// 設定走路動畫
    /// </summary>
    private void WalkInFixedUpdate()
    {
        if (state == StateEnemy.walkRight)
        {      
           ani.SetBool("往右走", true);
        }
        else ani.SetBool("往右走", false);

        if (state == StateEnemy.walkUp)
        {
            ani.SetBool("往上走", true);
        }
        else ani.SetBool("往上走", false);

        if (state == StateEnemy.walkLift)
        {
            ani.SetBool("往左走", true);
        }
        else ani.SetBool("往左走", false);

        if (state == StateEnemy.walkDown)
        {
            ani.SetBool("往下走", true);
        }
        else ani.SetBool("往下走", false);
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
        ani.SetBool("死亡開關", true);
    }

    #endregion
}

public enum StateEnemy
{
    idle, walkUp, walkRight, walkLift, walkDown, track,attack,dead
}
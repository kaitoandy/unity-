using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/// <summary>
/// AI基底 3D模式
/// 等待 行走 追蹤玩家 攻擊 死亡
/// </summary>
public class AiBase3D : MonoBehaviour
{
    #region 欄位:公開
    [Header("移動速度"), Range(0, 500)]
    public float speed = 3;
    [Header("攻擊力"), Range(0, 500)]
    public float attack = 50;
    [Header("血量"), Range(0, 500)]
    public float hp = 300;
    [Header("追蹤範圍"), Range(0, 100)]
    public float rangTrack = 10;
    [Header("攻擊範圍"), Range(0, 100)]
    public float rangAttack = 10;
    [Header("攻擊冷卻"), Range(0, 100)]
    public float cdAttack = 2;
    [Header("面向速度"), Range(0, 100)]
    public float turn = 2.5f;
    [Header("攻擊尺寸與位移")]
    public Vector3 areaAttackSize = Vector3.one;
    public Vector3 areaAttackOffst;
    [Header("傳送傷害延遲時間"),Range(0,10)]
    public float delaySendAttackToTarget = 0.3f;

    #endregion

    #region 欄位:私人
    private Animator ani;
    private NavMeshAgent nav;
    private Transform target;
    private float timerAttack;
    


    #endregion

    #region 事件
    private void Awake()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
        nav.stoppingDistance = rangAttack;
        timerAttack = cdAttack;

    }

    private void OnDrawGizmos()
    {
        #region 追蹤與攻擊範圍
        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, rangTrack);

        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, rangAttack);
        #endregion

        #region 攻擊範圍
        Gizmos.color = new Color(0.2f, 0,1, 0.3f);
        Gizmos.matrix = Matrix4x4.TRS(transform.position +
            transform.right * areaAttackOffst.x +
            transform.up * areaAttackOffst.y +
            transform.forward * areaAttackOffst.z,
            transform.rotation, transform.localScale);
        Gizmos.DrawCube(Vector3.zero,areaAttackSize);

        #endregion
    }

    private void Update()
    {
        CheckTargetIsInTarkRang();
        Track();
        Attack();
    }
    #endregion

    #region 方法:公開
    #endregion

    #region 方法:私人
    /// <summary>
    /// 檢查目標是否進入範圍並保存資訊
    /// </summary>
    private void CheckTargetIsInTarkRang()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, rangTrack ,1 << 3);

        if (hits.Length > 0) target = hits[0].transform;                //玩家進入後儲存目標資訊
        else target = null;                                             //離開後目標設為空檔
    }

    private void Track()
    {
        if (target)
        {
            nav.isStopped = false;
            nav.SetDestination(target.position);
        }
        else
        {
            nav.isStopped = true;
        }
        ani.SetBool("走路開關", !nav.isStopped);
    }

    /// <summary>
    /// 攻擊行為
    /// 檢查目標是否在攻擊範圍內
    /// 進行攻擊與冷卻處理並且面向玩家
    /// </summary>
    private void Attack()
    {
        if (!target) return;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= rangAttack)
        {
            if (timerAttack >= cdAttack)
            {
                ani.SetTrigger("攻擊觸發");
                timerAttack = 0;
                AttackAreaCheck();
            }
            else
            {
                timerAttack += Time.deltaTime;
                ani.SetBool("走路開關", false);
            }
            LookAtTarget();
        }
        
    }

    private IEnumerable AttackAreaCheck()
    {
        yield return new WaitForSeconds(delaySendAttackToTarget);
        Collider[] hits = Physics.OverlapBox(transform.position +
            transform.right * areaAttackOffst.x +
            transform.up * areaAttackOffst.y +
            transform.forward * areaAttackOffst.z,
            areaAttackSize / 2, Quaternion.identity, 1 << 3);
    }
    private void LookAtTarget()
    {
        Vector3 posTarget = target.position;
        posTarget.y = transform.position.y;
        Quaternion lookRotation = Quaternion.LookRotation(posTarget, transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turn * Time.deltaTime);
    }
    #endregion
}

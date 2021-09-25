using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/// <summary>
/// AI�� 3D�Ҧ�
/// ���� �樫 �l�ܪ��a ���� ���`
/// </summary>
public class AiBase3D : MonoBehaviour
{
    #region ���:���}
    [Header("���ʳt��"), Range(0, 500)]
    public float speed = 3;
    [Header("�����O"), Range(0, 500)]
    public float attack = 50;
    [Header("��q"), Range(0, 500)]
    public float hp = 300;
    [Header("�l�ܽd��"), Range(0, 100)]
    public float rangTrack = 10;
    [Header("�����d��"), Range(0, 100)]
    public float rangAttack = 10;
    [Header("�����N�o"), Range(0, 100)]
    public float cdAttack = 2;
    [Header("���V�t��"), Range(0, 100)]
    public float turn = 2.5f;
    [Header("�����ؤo�P�첾")]
    public Vector3 areaAttackSize = Vector3.one;
    public Vector3 areaAttackOffst;
    [Header("�ǰe�ˮ`����ɶ�"),Range(0,10)]
    public float delaySendAttackToTarget = 0.3f;

    #endregion

    #region ���:�p�H
    private Animator ani;
    private NavMeshAgent nav;
    private Transform target;
    private float timerAttack;
    


    #endregion

    #region �ƥ�
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
        #region �l�ܻP�����d��
        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, rangTrack);

        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, rangAttack);
        #endregion

        #region �����d��
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

    #region ��k:���}
    #endregion

    #region ��k:�p�H
    /// <summary>
    /// �ˬd�ؼЬO�_�i�J�d��ëO�s��T
    /// </summary>
    private void CheckTargetIsInTarkRang()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, rangTrack ,1 << 3);

        if (hits.Length > 0) target = hits[0].transform;                //���a�i�J���x�s�ؼи�T
        else target = null;                                             //���}��ؼг]������
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
        ani.SetBool("�����}��", !nav.isStopped);
    }

    /// <summary>
    /// �����欰
    /// �ˬd�ؼЬO�_�b�����d��
    /// �i������P�N�o�B�z�åB���V���a
    /// </summary>
    private void Attack()
    {
        if (!target) return;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= rangAttack)
        {
            if (timerAttack >= cdAttack)
            {
                ani.SetTrigger("����Ĳ�o");
                timerAttack = 0;
                AttackAreaCheck();
            }
            else
            {
                timerAttack += Time.deltaTime;
                ani.SetBool("�����}��", false);
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

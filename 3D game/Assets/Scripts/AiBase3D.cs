using UnityEngine;
using UnityEngine.AI;

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

    #endregion

    #region ���:�p�H
    private Animator ani;
    private NavMeshAgent nav;


    #endregion

    #region �ƥ�
    private void Awake()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, rangTrack);
    }
    #endregion

    #region ��k:���}
    #endregion

    #region ��k:�p�H
    #endregion
}

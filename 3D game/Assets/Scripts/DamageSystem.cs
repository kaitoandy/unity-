using UnityEngine;

/// <summary>
/// ���˨t��
/// ��q �ʵe ���`
/// </summary>
public class DamageSystem : MonoBehaviour
{
    #region ���:���}
    [Header("��q"), Range(0, 1000)]
    public float hp = 200;
    [Header("���˰ʵe�ѼƦW��")]
    public string parameterDamage = "����Ĳ�o";
    public string parameterDead = "���`�}��";

    #endregion

    #region ���:�p�H

    private Animator ani;

    #endregion

    #region �ƥ�
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    #endregion

    /// <summary>
    /// ����
    /// ��������B�z���˦欰
    /// </summary>
    /// <param name="getAttack">���쪺�����O</param>
    #region ��k:���}
    public void Damage(float getAttack)
    {
        hp -= getAttack;
        ani.SetTrigger(parameterDamage);
        if (hp <= 0) Dead();

    }
    #endregion

    /// <summary>
    /// ���`��k
    /// </summary>
    #region ��k:�p�H
    private void Dead()
    {
        hp = 0;
        ani.SetBool(parameterDead, true);
            
    }
    #endregion
}

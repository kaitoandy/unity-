using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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
    [Header("����:��� �ݭn�ɱN�����J")]
    public Image imgHp;
    [Header("���˨ƥ�:���ˤ���n�B�z���欰")]
    public UnityEvent onDamage;
    [Header("���`�ƥ�:���`����n�B�z����")]
    public UnityEvent onDead;

    #endregion

    #region ���:�p�H

    private Animator ani;
    private float hpMax;

    #endregion

    #region �ƥ�
    private void Awake()
    {
        ani = GetComponent<Animator>();
        hpMax = hp;
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
        if (ani.GetBool(parameterDead)) return;        //�p�G���`���X���B�z

        hp -= getAttack;
        ani.SetTrigger(parameterDamage);
        onDamage.Invoke();
        if (hp <= 0) Dead();

    }
    /// <summary>
    /// ��s�������
    /// </summary>
    public void UpdateHpUI()
    {
        imgHp.fillAmount = hp / hpMax;
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
        onDead.Invoke();
            
    }
    #endregion
}

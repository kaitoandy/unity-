using UnityEngine;

public class player : MonoBehaviour
{
#region ���
[Header("���ʳt��"),Range(0,1000)]
public float speed = 10.5f;
[Header("���D����"),Range(0,3000)]
public int hight = 100;
[Header("��q"), Range(0,100)]
public float HP = 100;
[Header("��q"), Tooltip("�Ψ��x�s�}��O�_�b�a�O�W����T �b�a�O�W true ���b�a�O�W false")]
public bool isGround;

private AudioSource aud;
private Rigidbody2D rig;
private Animator ani;




    #endregion


    #region �ƥ�

    #endregion

    #region ��k

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="horizontal">���k������</param>
    private void Move(float horizontal)
    {

    }

    /// <summary>
    /// ���D
    /// </summary>
    private void Jump()
    {

    }

    /// <summary>
    /// ����
    /// </summary>
    private void Attack()
    {

    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage">�l�˭�</param>
    public void Hurt(float damage)
    {

    }

    /// <summary>
    /// ���`
    /// </summary>
    private void Death()
    {

    }

    /// <summary>
    /// �Y�D��
    /// </summary>
    /// <param name="prop">�D��W��</param>
    private void EatProp(string prop)
    {

    }
    #endregion

}

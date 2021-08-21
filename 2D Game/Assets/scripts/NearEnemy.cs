using UnityEngine;
using System.Collections;  //�ޥ�.�t��.���X  ��P�{��
/// <summary>
/// ��Z���ĤH��������:��Z������
/// </summary>
/// 

//���O:�����O
// : �᭱�N���O�n�~�Ӫ����O
public class NearEnemy : BaseEnemy
{
    #region ���
    [Header("�����ϰ쪺�첾�P�j�p")]
    public Vector2 checkattackoffset;
    public Vector3 checkattacksize;

    #endregion

    #region �ƥ�
    protected override void OnDrawGizmos()
    {
        //�����O�쥻���{��
        base.OnDrawGizmos();
        Gizmos.color = new Color(1, 0.3f, 0.1f, 0.3f);
        Gizmos.DrawCube(transform.position + 
            transform.right * checkattackoffset.x + 
            transform.up * checkattackoffset.y, 
            checkattacksize);
    }

    protected override void Update()
    {
        base.Update();

        CheckPlayerInAttackArea();
    }
    #endregion

    #region ��k
    /// <summary>
    /// �ˬd���a�O�_�i�J�����ϰ�
    /// </summary>
    private void CheckPlayerInAttackArea()
    {
         hit = Physics2D.OverlapBox(transform.position + 
            transform.right * checkattackoffset.x + 
            transform.up * checkattackoffset.y, 
            checkattacksize,0, 1 << 7);

        if (hit) state = StateEnemy.attack;
        

    }

    protected override void AttackMethod()
    {
        base.AttackMethod();

        StartCoroutine(DelaySendDamageToPlayer());    //�Ұʨ�P�{��

    }

    //��P�{�ǥΪk
    //1. �ޥ�System.Collections API
    //2. �Ǧ^��k:�Ǧ^����IEnumerator
    //3. �ϥ�StartCoroutine() �Ұʨ�P�{��

    /// <summary>
    /// ����N�ˮ`�ǵ����a
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelaySendDamageToPlayer()
    {
        //�h���{���ֱ���
        //Alt + �W�ΤU

        //�榡�Ʊƪ�:Ctrl + K D

        //���o�}�C�y�k:�}�C.Length
        for (int i = 0; i < attacksDelay.Length; i++)
        {
            //���o�}�C��ƻy�k:�}�C���W��[�s��]
            yield return new WaitForSeconds(attacksDelay[i]);

            if (hit) player.Hurt(attack);           //�p�G�I����T�s�b,�N�缾�a�y���ˮ`
        }

        //���ݧ�����^�_�쥻���A�ɶ� - �����̫᪺�ɶ�
        yield return new WaitForSeconds(afterAttackRestoreOriginal);
        //�p�G���a�٦b�����ϰ줺 �N���� �_ �h����
        if (hit) state = StateEnemy.attack;
        else state = StateEnemy.walk;

    }

    
    #endregion
}

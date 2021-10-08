using UnityEngine;
using System.Collections;

/// <summary>
/// ��Z���ĤH�l����
/// </summary>
public class NearEnemy : BaseEnemy
{

    #region ���:���}
    [Header("�����ϰ�첾�j�p")]
    public Vector2 checkAttackOffset;
    public Vector2 checkAttackSize;

    #endregion

    #region ���:�p�H
    #endregion

    #region �ƥ�
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.3f, 0.1f, 0.3f);
        Gizmos.DrawCube(transform.position +
            transform.right * checkAttackOffset.x + 
            transform.up * checkAttackOffset.y, checkAttackSize);
    }

    protected override void Update()
    {
        base.Update();
        CheckPlayerInAttackArea();
    }
    #endregion

    #region ��k
    private void CheckPlayerInAttackArea()
    {
      Collider2D hit =  Physics2D.OverlapBox(transform.position +
            transform.right * checkForwardOffest.x +
            transform.up * checkForwardOffest.y, checkAttackSize, 0,  1 << 0);

        if (hit) state = StateEnemy.attack;
    }

    protected override void AttackMethod()
    {
        base.AttackMethod();

        StartCoroutine(DelaySendDamageToPlayer());
    }

    private IEnumerator DelaySendDamageToPlayer()
    {
        yield return new WaitForSeconds(attackDelayFirs);
        player.Hurt(attack);
    }
    #endregion
}

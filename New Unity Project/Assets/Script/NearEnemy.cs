using UnityEngine;
using System.Collections;

/// <summary>
/// 近距離敵人子類型
/// </summary>
public class NearEnemy : BaseEnemy
{

    #region 欄位:公開
    [Header("攻擊區域位移大小")]
    public Vector2 checkAttackOffset;
    public Vector2 checkAttackSize;

    #endregion

    #region 欄位:私人
    #endregion

    #region 事件
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

    #region 方法
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

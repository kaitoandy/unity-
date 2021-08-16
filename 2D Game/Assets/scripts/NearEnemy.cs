using UnityEngine;
/// <summary>
/// 近距離敵人攻擊類型:近距離攻擊
/// </summary>
/// 

//類別:父類別
// : 後面代表的是要繼承的類別
public class NearEnemy : BaseEnemy
{
    #region 欄位
    [Header("攻擊區域的位移與大小")]
    public Vector2 checkattackoffset;
    public Vector3 checkattacksize;

    #endregion

    #region 事件
    protected override void OnDrawGizmos()
    {
        //父類別原本的程式
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

    #region 方法
    private void CheckPlayerInAttackArea()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position + 
            transform.right * checkattackoffset.x + 
            transform.up * checkattackoffset.y, 
            checkattacksize,0, 1 << 7);

        if (hit) state = StateEnemy.attack;
        

    }
    #endregion
}

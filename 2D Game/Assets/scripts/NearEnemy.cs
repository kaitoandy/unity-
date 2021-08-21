using UnityEngine;
using System.Collections;  //引用.系統.集合  協同程式
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
    /// <summary>
    /// 檢查玩家是否進入攻擊區域
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

        StartCoroutine(DelaySendDamageToPlayer());    //啟動協同程序

    }

    //協同程序用法
    //1. 引用System.Collections API
    //2. 傳回方法:傳回類型IEnumerator
    //3. 使用StartCoroutine() 啟動協同程序

    /// <summary>
    /// 延遲將傷害傳給玩家
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelaySendDamageToPlayer()
    {
        //搬移程式快捷鍵
        //Alt + 上或下

        //格式化排版:Ctrl + K D

        //取得陣列語法:陣列.Length
        for (int i = 0; i < attacksDelay.Length; i++)
        {
            //取得陣列資料語法:陣列欄位名稱[編號]
            yield return new WaitForSeconds(attacksDelay[i]);

            if (hit) player.Hurt(attack);           //如果碰撞資訊存在,就對玩家造成傷害
        }

        //等待攻擊後回復原本狀態時間 - 攻擊最後的時間
        yield return new WaitForSeconds(afterAttackRestoreOriginal);
        //如果玩家還在攻擊區域內 就攻擊 否 則走路
        if (hit) state = StateEnemy.attack;
        else state = StateEnemy.walk;

    }

    
    #endregion
}

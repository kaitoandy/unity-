using UnityEngine;

/// <summary>
/// 受傷系統
/// 血量 動畫 死亡
/// </summary>
public class DamageSystem : MonoBehaviour
{
    #region 欄位:公開
    [Header("血量"), Range(0, 1000)]
    public float hp = 200;
    [Header("受傷動畫參數名稱")]
    public string parameterDamage = "受傷觸發";
    public string parameterDead = "死亡開關";

    #endregion

    #region 欄位:私人

    private Animator ani;

    #endregion

    #region 事件
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    #endregion

    /// <summary>
    /// 受傷
    /// 受到攻擊處理受傷行為
    /// </summary>
    /// <param name="getAttack">受到的攻擊力</param>
    #region 方法:公開
    public void Damage(float getAttack)
    {
        hp -= getAttack;
        ani.SetTrigger(parameterDamage);
        if (hp <= 0) Dead();

    }
    #endregion

    /// <summary>
    /// 死亡方法
    /// </summary>
    #region 方法:私人
    private void Dead()
    {
        hp = 0;
        ani.SetBool(parameterDead, true);
            
    }
    #endregion
}

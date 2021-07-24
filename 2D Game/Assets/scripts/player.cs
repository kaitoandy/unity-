using UnityEngine;

public class player : MonoBehaviour
{
#region 欄位
[Header("移動速度"),Range(0,1000)]
public float speed = 10.5f;
[Header("跳躍高度"),Range(0,3000)]
public int hight = 100;
[Header("血量"), Range(0,100)]
public float HP = 100;
[Header("血量"), Tooltip("用來儲存腳色是否在地板上的資訊 在地板上 true 不在地板上 false")]
public bool isGround;

private AudioSource aud;
private Rigidbody2D rig;
private Animator ani;




    #endregion


    #region 事件

    #endregion

    #region 方法

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="horizontal">左右水平值</param>
    private void Move(float horizontal)
    {

    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {

    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {

    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">損傷值</param>
    public void Hurt(float damage)
    {

    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Death()
    {

    }

    /// <summary>
    /// 吃道具
    /// </summary>
    /// <param name="prop">道具名稱</param>
    private void EatProp(string prop)
    {

    }
    #endregion

}

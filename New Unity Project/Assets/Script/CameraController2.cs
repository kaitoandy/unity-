using UnityEngine;

/// <summary>
/// 主場景中攝影機的追蹤
/// </summary>
public class CameraController2 : MonoBehaviour
{
    #region 欄位:公開
    [Header("追蹤速度"),Range(0,100)]
    public float speed = 10;
    [Header("追蹤目標")]
    public string nameTraget;
    #endregion
    [Header("左右限制")]
    public Vector2 limitHorizontol;
    [Header("上下限制")]
    public Vector2 limitVertical;

    #region 欄位:私人
    /// <summary>
    /// 追蹤目標
    /// </summary>
    private Transform traget;

    #endregion

    #region 事件
    /// <summary>
    /// 目標變形元件
    /// </summary>
    private void Start()
    {
        traget = GameObject.Find(nameTraget).transform;
    }

    /// <summary>
    /// 用比 Update 更新慢的 LateUpdate 執行攝影機
    /// </summary>
    private void LateUpdate()
    {
        Tarck();
    }

    #endregion

    #region 方法
    private void Tarck()
    {
        Vector3 posCamear = transform.position;            //攝影機座標
        Vector3 posTraget = traget.position;               //玩家座標

        //運算後座標
        Vector3 posResult = Vector3.Lerp(transform.position, traget.position, speed * Time.deltaTime);

        //攝影機回預設
        posResult.z = -10;

        posResult.x = Mathf.Clamp(posResult.x, limitHorizontol.x, limitHorizontol.y);
        posResult.y = Mathf.Clamp(posResult.y, limitHorizontol.x, limitHorizontol.y);

        //此物件座標為運算後座標
        transform.position = posResult;

    }

    #endregion
}

using UnityEngine;
using System.Collections;

/// <summary>
/// 攝影機追蹤目標
/// </summary>
public class CameraComtrol : MonoBehaviour
{
    #region 欄位
    [Header("追蹤速度"), Range(0, 100)]
    public float speed = 10;
    [Header("要追蹤物件名稱")]
    public string nameTreget;
    [Header("左右限制")]
    public Vector2 limitHorizontal;

    /// <summary>
    /// 追蹤目標
    /// </summary>
    private Transform target;


    #endregion

    #region 事件
    private void Start()
    {
        //很吃效能,所以在start執行
        //目標變形元件 = 遊戲物件.尋找(物件名稱).變形元件
        target = GameObject.Find(nameTreget).transform;

    }
    #endregion

    //較慢更新:在 Update 執行 , 建議用來處理攝影機
    private void LateUpdate()
    {
        Track();
    }

    #region 方法
    /// <summary>
    /// 追蹤目標
    /// </summary>
    private void Track()
    {
        Vector3 posCamera = transform.position;     //A點:攝影機座標
        Vector3 posTarget = target.position;        //B點:目標物座標


        //運算後的結果座標 = 取得A點攝影機 與 B點目標物 之間的座標
        Vector3 posReault = Vector3.Lerp(posCamera, posTarget, speed * Time.deltaTime);

        //攝影機Z軸放回預設 -10 避免看不到2D物件
        posReault.z = -10;

        //使用夾住 API 限制攝影機的'左右範圍
        posReault.x = Mathf.Clamp(posReault.x, limitHorizontal.x, limitHorizontal.y);

        //此物件座標 指定為 運算後的結果座標
        transform.position = posReault;
    }
    #endregion

    [Header("晃動的值"), Range(0, 5)]
    public float shakeValue = 0.2f;
    [Header("晃動的次數"), Range(0, 20)]
    public float shakeCount = 10;
    [Header("晃動的間隔"), Range(0, 5)]
    public float shajeInterval = 0.3f;


    public IEnumerator ShakeEffect()
    {
        Vector3 posOriginal = transform.position;              //取得晃動的座標

        for(int i = 0; i < shakeCount; i++)                    //迴圈執行座標改動
        {
            Vector3 posShake = posOriginal;

            if (i % 2 == 0) posShake.y -= shakeValue;          // i 為偶數就往左
            else posShake.y += shakeValue;                     // 奇數就往右

            transform.position = posShake;

            yield return new WaitForSeconds(shajeInterval);
        }

        transform.position = posOriginal;                      //攝影機回到原始座標
    }



}

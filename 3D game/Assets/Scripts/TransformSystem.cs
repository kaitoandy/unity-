using UnityEngine;

/// <summary>
/// 變身系統
/// </summary>
public class TransformSystem : MonoBehaviour
{
    #region 欄位:公開
    [Header("玩家變身前後模型物件")]
    public GameObject goTransformBefore;
    public GameObject goTransformAfter;

    public vThirdPersonCamera cam;

    // 靜態資料特性
    // 1.不會顯示在資料面板
    // 2.切換場景不會還原
    // 3.存取方式 類別.靜態資料名稱
    /// <summary>
    /// 是否變身
    /// </summary>
    public static bool isTransform;
    #endregion




    #region 欄位:私人
    #endregion

    #region 事件
    private void Update()
    {
        TransformSwitch();
    }

    #endregion

    /// <summary>
    /// 變身切換
    /// 按下R鈕對調模型
    /// </summary>
    #region 方法:私人
    private void TransformSwitch()
    {
        //按下R鍵 變身前後模型顯示狀態與原本顛倒
        if (Input.GetKeyDown(KeyCode.R))
        {

            isTransform = !isTransform;

            if (!goTransformBefore.activeInHierarchy)
            {
                goTransformBefore.transform.position = goTransformAfter.transform.position;
                goTransformBefore.transform.eulerAngles = goTransformAfter.transform.eulerAngles;
            }
            else if (!goTransformAfter.activeInHierarchy)
            {
                goTransformAfter.transform.position = goTransformBefore.transform.position;
                goTransformAfter.transform.eulerAngles = goTransformBefore.transform.eulerAngles;
            }


            goTransformBefore.SetActive(!goTransformBefore.activeInHierarchy);
            goTransformAfter.SetActive(!goTransformAfter.activeInHierarchy);


            //攝影機目標設定為目前顯示的模型
            if (goTransformBefore.activeInHierarchy) cam.SetTarget(goTransformBefore.transform);
            else if (goTransformAfter.activeInHierarchy) cam.SetTarget(goTransformAfter.transform);

            

        }

    }

        

}

#endregion


using UnityEngine;

/// <summary>
/// 儲存對話資料
/// 1. 對話者名稱
/// 2. 對話內容 - 多段
/// </summary>
/// 

//繼承類別改為 ScriptableObject 腳本化物件
//代表此類別的內容可以建立物件並儲存於專案內
//需搭配 CreateAssetMenu 建立素材選項來建立此物件
[CreateAssetMenu(menuName ="kaitoandy/對話資料" , fileName = "對話資料")]
public class DialogueData : ScriptableObject
{
    [Header("對話者名稱")]
    public string dialogueTalkerName;
    [Header("對話內容") ,TextArea(2,5)]
    public string[] dialogueContents;
    [Header("此對話任務的需求量"), Range(0, 100)]
    public float coundNeed = 2;
    [Header("結束內容"), TextArea(2, 5)]
    public string[] dialogueContentsFinish;


}

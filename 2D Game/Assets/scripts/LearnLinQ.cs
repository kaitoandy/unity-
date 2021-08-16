using UnityEngine;
using System.Linq;      //引用LinQ 查詢語言API - 查找陣列資料

public class LearnLinQ : MonoBehaviour
{
    public int[] scores = { 10, 80, 60, 30, 70, 90, 77, 1, 0 };
    public int[] result;
    public int[] resultEqualThan60;

    private void Start()
    {
        //檢查有沒有0分
        //黏巴達 Lambda 簡寫 C# 7.0 後的簡寫方式

        //檢查scores 內有沒有分數為 0的值
        // x 代名詞
        // => 設定條件
        result = scores.Where(x => x == 0).ToArray();

        //檢查有沒有大於60分

        resultEqualThan60 = scores.Where(x => x >= 60).ToArray();
    }
}

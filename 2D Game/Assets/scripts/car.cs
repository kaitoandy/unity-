using UnityEngine; //引用Unity引擎提供的API (Unity Engine 命名空間)

//類別
//語法 類別關鍵字 腳本名稱
public class Car : MonoBehaviour
{
   #region 註解
    //單行註解:添加說明 翻譯 紀錄等等...會被程式忽略
    //kaito 2021.07.17 (六)開發汽車腳本
    /*多行註解
     *多行註解
     *多行註解
     *多行註解
     */
     #endregion

   #region 認識欄位與四大常用類型
     //欄位屬性:輔助欄位添加功能
     //語法:[屬性名稱(屬性值)]
     //欄位:儲存簡單的資料
     //語法:
     //修飾詞 資料類型 欄位名稱 指定符號 預設值 結尾
     //指定符號=
     //修飾詞
     //1.私人 private 預設-不顯示
     //2.公開 public 預設-顯示

     //Unity 常用四大類型
     //整數 int 例:1,99,0,-123
     //浮點數: float 例:2.3,3.1415,-1.123
     //字串: string 例:BMW,賓士,對話內容
     //布林值 bool 例:true,false

     //定義欄位
     //Unity以屬性 Inspector 面板上的值為主
     public float weight = 3.5f;
     public int cc = 2000;
     public string brand = "賓士";
     public bool windowSky = true;
     //標題:[Header(字串)]

     [Header("輪胎數量")]
     public int wheelCount = 4;
     //提示:[Tooltip(字串)]

     [Tooltip("這個欄位作用是設定汽車的高度")]
     public float height = 1.5f;

     //範圍 :[Range(最大值 最小值)]-僅限數值類型 float或 int
     [Range(2,10)]
     public int doorCount;
     #endregion 
     
  #region 其他類型
     //顏色 Color
     public Color Color1;             // 不指定為黑
     public Color red = Color.red;    //使用預設值
     public Color yello = Color.yellow;
     public Color ColorCustom1 = new Color(0.5f,0.5f,0); //自訂顏色(RGB)
     public Color ColorCustom2 = new Color(0.5f,0,0.5f,0); //自訂顏色(RGB)

     //座標 2-4 維 Vector2-4
     //保持數值資訊 浮點數
     public Vector2 v2;
     public Vector2 v2Zero = Vector2.zero;
     public Vector2 v2one = Vector2.one;
     public Vector2 v2Up = Vector2.up;
     public Vector2 v2Right = Vector2.right;
     public Vector2 v2Costum = new Vector2(-99.5f, 100.5f);

     public Vector3 v3;
     public Vector4 v4;

     //按鍵類型
     public KeyCode kc;
     public KeyCode forwar = KeyCode.D;
     public KeyCode attack = KeyCode.Mouse0; //左鍵0 右鍵1 滾輪2

     //遊戲物件與元件
     public GameObject goCamera; //遊戲物件包含場景上的以及專案內的預製物
     //元件僅限於存放屬性面板有此元件的物件

     public Transform tracCar;
     public SpriteRenderer sprPicture;


     #endregion 
     
    #region 事件
     //開始事件:撥放遊戲時執行一次,處理初始化
     private void Start ()
     {
        //輸出(任何類型資料);
        print("你好");


        //練習取得欄位 Get

        print(brand);

        //練習取得欄位 Set
        windowSky = true;
        cc = 5000;
        weight = 9.9f;


    }


    //更新事件:大概一秒60次.60fps.處理物件移動或是監聽玩家輸入
    private void Update()
    {
        print("我要黃金");
    }

    #endregion
}

using UnityEngine; //�ޥ�Unity�������Ѫ�API (Unity Engine �R�W�Ŷ�)

//���O
//�y�k ���O����r �}���W��
public class Car : MonoBehaviour
{
   #region ����
    //������:�K�[���� ½Ķ ��������...�|�Q�{������
    //kaito 2021.07.17 (��)�}�o�T���}��
    /*�h�����
     *�h�����
     *�h�����
     *�h�����
     */
     #endregion

   #region �{�����P�|�j�`������
     //����ݩ�:���U���K�[�\��
     //�y�k:[�ݩʦW��(�ݩʭ�)]
     //���:�x�s²�檺���
     //�y�k:
     //�׹��� ������� ���W�� ���w�Ÿ� �w�]�� ����
     //���w�Ÿ�=
     //�׹���
     //1.�p�H private �w�]-�����
     //2.���} public �w�]-���

     //Unity �`�Υ|�j����
     //��� int ��:1,99,0,-123
     //�B�I��: float ��:2.3,3.1415,-1.123
     //�r��: string ��:BMW,���h,��ܤ��e
     //���L�� bool ��:true,false

     //�w�q���
     //Unity�H�ݩ� Inspector ���O�W���Ȭ��D
     public float weight = 3.5f;
     public int cc = 2000;
     public string brand = "���h";
     public bool windowSky = true;
     //���D:[Header(�r��)]

     [Header("���L�ƶq")]
     public int wheelCount = 4;
     //����:[Tooltip(�r��)]

     [Tooltip("�o�����@�άO�]�w�T��������")]
     public float height = 1.5f;

     //�d�� :[Range(�̤j�� �̤p��)]-�ȭ��ƭ����� float�� int
     [Range(2,10)]
     public int doorCount;
     #endregion 
     
  #region ��L����
     //�C�� Color
     public Color Color1;             // �����w����
     public Color red = Color.red;    //�ϥιw�]��
     public Color yello = Color.yellow;
     public Color ColorCustom1 = new Color(0.5f,0.5f,0); //�ۭq�C��(RGB)
     public Color ColorCustom2 = new Color(0.5f,0,0.5f,0); //�ۭq�C��(RGB)

     //�y�� 2-4 �� Vector2-4
     //�O���ƭȸ�T �B�I��
     public Vector2 v2;
     public Vector2 v2Zero = Vector2.zero;
     public Vector2 v2one = Vector2.one;
     public Vector2 v2Up = Vector2.up;
     public Vector2 v2Right = Vector2.right;
     public Vector2 v2Costum = new Vector2(-99.5f, 100.5f);

     public Vector3 v3;
     public Vector4 v4;

     //��������
     public KeyCode kc;
     public KeyCode forwar = KeyCode.D;
     public KeyCode attack = KeyCode.Mouse0; //����0 �k��1 �u��2

     //�C������P����
     public GameObject goCamera; //�C������]�t�����W���H�αM�פ����w�s��
     //����ȭ���s���ݩʭ��O�������󪺪���

     public Transform tracCar;
     public SpriteRenderer sprPicture;


     #endregion 
     
    #region �ƥ�
     //�}�l�ƥ�:����C���ɰ���@��,�B�z��l��
     private void Start ()
     {
        //��X(�����������);
        print("�A�n");


        //�m�ߨ��o��� Get

        print(brand);

        //�m�ߨ��o��� Set
        windowSky = true;
        cc = 5000;
        weight = 9.9f;


    }


    //��s�ƥ�:�j���@��60��.60fps.�B�z���󲾰ʩάO��ť���a��J
    private void Update()
    {
        print("�ڭn����");
    }

    #endregion
}

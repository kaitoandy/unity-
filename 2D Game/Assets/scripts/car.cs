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
        #region �m�����
        //��X(�����������);
        print("�A�n");


        //�m�ߨ��o��� Get

        print(brand);

        //�m�ߨ��o��� Set
        windowSky = true;
        cc = 5000;
        weight = 9.9f;

        #endregion

        //�I�s��k�y�k: ��k�W��()
        Drive50();
        Drive100();
        Drive(150 , "������");         //�I�s�p�A�������٬��޼�,�ݿ�J�����޼�
        Drive(200 , "�F�F�F");
        Drive(300);                   //���w�]�Ȫ��ѼƤ��Τ޼�

        //Drive(80 , "�H��");  �ɳt80 , ����:������ , �S��:�H�� �o�O���~�ܽd

        Drive(80, effact: "�H��");    //�ϥΦh�ӹw�]�ȰѼƮɥi�ϥ� �ѼƦW��:"��"

        Drive(999, "�N�N�N", "�z��");

        float kg = KG();              //�ϰ��ܼ�,�Ȧb���A�����A��
        print("�ର�����T" + kg);

        print("BMI��" + BMI(90, 1.64f));

    }


    //��s�ƥ�:�j���@��60��.60fps.�B�z���󲾰ʩάO��ť���a��J
    private void Update()
    {
        print("�ڭn����");
    }

    #endregion

    #region ��k (�\��,�禡) Method
     //��@����������欰 , �Ҧp: �T�����e�} , �}�ҨT�����T�ü��񭵼�
     //���y�k: �׹��� ��    �� �W�� ���w �w�]��;
     //��k�y�k: �׹��� �Ǧ^���� �W�� (�Ѽ�) { �{���϶� }
     //���� : void - �L�Ǧ^
     //�w�q��k.���|���楲���I�s,�I�s���覡:�b�ƥ󤺩I�s����k
     //���@,�X�R��

    private void Drive50()
    {
        print("�}���� ~  �ɳt:50");
    }

    private void Drive100()
    {
        print("�}���� ~  �ɳt:100");
    }

    //�Ѽƻy�k: ���� �ѼƦW�� - �g�b�p�A����.�Ȧb����k���i�ϥ�
    //�Ѽ�1.�Ѽ�2.�Ѽ�3.............�Ѽ�N
    //�Ѽƹw�]��:���� �Ѽ� �W�� ���w �� (��񦡰Ѽ�)
    //*�w�]�ȥu���b�̥k��

    /// <summary>
    /// �o�O�}������k,�Ψӱ�����t��.����.�S��
    /// </summary>
    /// <param name="speed">���l�����ʳt��</param>
    /// <param name="sound">�}���ɪ�����</param>
    /// <param name="effact">�}���ɪ��S��</param>


    private void Drive( int speed , string sound = "������" , string effact = "�ǹ�")
    {
        print("�}���� ~ �ɳt:" + speed);
        print("�}������" + sound);
        print("�}���S��" + effact);
    }

    /// <summary>
    /// �����ഫ������
    /// </summary>
    /// <returns>�ର���窺���q��T</returns>

    private float KG()
    {
        return weight * 1000;
    }

    /// <summary>
    /// �p��BMI��
    /// </summary>
    /// <param name="weight">�魫��T(����)</param>
    /// <param name="height">������T(����)</param>
    /// <returns></returns>

    private float BMI(float weight ,float height)
    {
        return weight / (height * height);
    }
    #endregion
}

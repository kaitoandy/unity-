using UnityEngine;

/// <summary>
/// �{��API,�H�βĤ@�إΪk: �R�A static
/// </summary>
public class APIstatic : MonoBehaviour
{
    //API ��� ������j��
    //1. �R  �A   ������r static
    //2. �D�R�A   �L����r static


    //�ݩ� Properties �i�z�Ѭ��P�������
    //��k Methiods

    private void Start()
    {
        //�R�A�ݩ�
        //1.���o
        //�y�k: ���O.�R�A�ݩ�

        print("�H���� : " + Random.value);         //0 - 1
        print("�L���j : " + Mathf.Infinity);

        #region  �m���R�A�ݩʻP��k
        //1.���o�R�A�ݩ�
        print("�Ҧ���v���ƶq:" + Camera.allCamerasCount);
        print("2D�����O�j�p:" + Physics2D.gravity);
        print("��P�v:" + Mathf.PI);

        //2.�]�w�R�A�ݩ�
        Physics2D.gravity = new Vector2(0, -20);
        Time.timeScale = 0.5f;                     //�C�ʧ@ ,�ְʧ@ = 2 ,�Ȱ� = 0
        print("�ɶ��j�p:" + Time.timeScale);

        //�I�s�R�A�ݩ�
        number = Mathf.Round(number);
        print("9.999�h�p���I" + number);

        float d = Vector3.Distance(a, b);
        print("a�Pb���Z��" + d);

        //Application.OpenURL(" https://unity.com/");

        #endregion


        //2.�]�w
        //�y�k: ���O.�R�A�ݩ� ���w �� ;

        Cursor.visible = false ;
        //Random.value = 7.7f; -���~�ܽd(��Ū�ݩʤ���]�w (Read Only))

        Screen.fullScreen = true;

       

        //�R�A��k
        //3.�I�s�R�A��k
        //�y�k: ���O.�R�A��k(�����޼�)

        float r = Random.Range(7.3f, 9.4f);

        print("�H���d�� 7.3 - 9.4 : " + r);

        


    }

    public float number = 9.999f;
    public Vector3 a = new Vector3(1, 1, 1);
    public Vector3 b = new Vector3(22, 22, 22);
    public float hp = 70;
    


    private void Update()
    {
        hp = Mathf.Clamp(hp, 0, 100);      //�ƾ�.����(��,�̤p��,�̤j��) - �N��J���ȧ��b�̤j�̤p�Ƚd��
        print("��q" + hp);

        #region  �m���R�A�ݩʻP��k
        //���o�R�A�ݩ�
        print("�O�_���o���N��:" + Input.anyKey);
       // print("�C���g�L�ɶ�:" + Time.time);

        //�I�s�R�A��k
        bool space = Input.GetKeyDown("space");
        print("�O�_���U�ť���:" + space);

        
        #endregion
    }
}

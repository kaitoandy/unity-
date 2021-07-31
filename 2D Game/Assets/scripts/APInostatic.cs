
using UnityEngine;

public class APInostatic : MonoBehaviour
{
    
    //API ��� ������j��
    //1. �R  �A   ������r static
    //2. �D�R�A   �L����r static


    //�ϥΫD�R�A�ݩ�:1.���w�q�D�R�A������O
    //�ϥΫD�R�A�ݩ�:3.��쥲����J�n���o��T������  *���ର�ŭ�
    public Transform traA;
    public Camera cam;
    public Transform traB;
    public Light lightA;

    public Camera camA;
    public SpriteRenderer srA;
    public Transform traC;
    public Rigidbody2D rig;

    private void Start()
    {
        #region �{�ѫD�R�A�ݩ�
        //1.���o�D�R�A�ݩ�

        //print("���o�y��" + Transform.position);  // ���~:�ݭn������Ѧ�

        //�ϥΫD�R�A�ݩ�:2.
        //*�y�k: ���.�D�R�A�ݩ�
        print("���o�ߤ���y��:" + traA.position);
        print("���o��v���I�����C��:" + cam.backgroundColor);

        //2.�]�w�D�R�A�ݩ�
        cam.backgroundColor = new Color(0.8f, 0.5f, 0.6f);

        //3.�I�s�D�R�A�ݩ�
        traB.Translate(1, 0, 0);
        lightA.Reset();



        #endregion

        #region �m�߫D�R�A�ݩ�
        //1.���o�D�R�A�ݩ�
        print("��v���`��" + camA.depth);
        print("�Ϥ�1���C��" + srA.color);

        //2.�]�w�D�R�A�ݩ�

        camA.backgroundColor = Random.ColorHSV();
        srA.flipY = true;

        #endregion

    }

    private void Update()
    {
        traC.Rotate(0, 0, 1);
        rig.AddForce(new Vector2(0, 10));
    }



}

using UnityEngine;

/// <summary>
/// ��v���l�ܥؼ�
/// </summary>
public class CameraComtrol : MonoBehaviour
{
    #region ���
    [Header("�l�ܳt��"), Range(0, 100)]
    public float speed = 10;
    [Header("�n�l�ܪ���W��")]
    public string nameTreget;
    [Header("���k����")]
    public Vector2 limitHorizontal;

    /// <summary>
    /// �l�ܥؼ�
    /// </summary>
    private Transform target;


    #endregion

    #region �ƥ�
    private void Start()
    {
        //�ܦY�į�,�ҥH�bstart����
        //�ؼ��ܧΤ��� = �C������.�M��(����W��).�ܧΤ���
        target = GameObject.Find(nameTreget).transform;

    }
    #endregion

    //���C��s:�b Update ���� , ��ĳ�ΨӳB�z��v��
    private void LateUpdate()
    {
        Track();
    }

    #region ��k
    /// <summary>
    /// �l�ܥؼ�
    /// </summary>
    private void Track()
    {
        Vector3 posCamera = transform.position;     //A�I:��v���y��
        Vector3 posTarget = target.position;        //B�I:�ؼЪ��y��


        //�B��᪺���G�y�� = ���oA�I��v�� �P B�I�ؼЪ� �������y��
        Vector3 posReault = Vector3.Lerp(posCamera, posTarget, speed * Time.deltaTime);

        //��v��Z�b��^�w�] -10 �קK�ݤ���2D����
        posReault.z = -10;

        //�ϥΧ��� API ������v����'���k�d��
        posReault.x = Mathf.Clamp(posReault.x, limitHorizontal.x, limitHorizontal.y);

        //������y�� ���w�� �B��᪺���G�y��
        transform.position = posReault;
    }
    #endregion




}

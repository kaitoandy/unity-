using UnityEngine;

/// <summary>
/// �D��������v�����l��
/// </summary>
public class CameraController2 : MonoBehaviour
{
    #region ���:���}
    [Header("�l�ܳt��"),Range(0,100)]
    public float speed = 10;
    [Header("�l�ܥؼ�")]
    public string nameTraget;
    #endregion
    [Header("���k����")]
    public Vector2 limitHorizontol;
    [Header("�W�U����")]
    public Vector2 limitVertical;

    #region ���:�p�H
    /// <summary>
    /// �l�ܥؼ�
    /// </summary>
    private Transform traget;

    #endregion

    #region �ƥ�
    /// <summary>
    /// �ؼ��ܧΤ���
    /// </summary>
    private void Start()
    {
        traget = GameObject.Find(nameTraget).transform;
    }

    /// <summary>
    /// �Τ� Update ��s�C�� LateUpdate ������v��
    /// </summary>
    private void LateUpdate()
    {
        Tarck();
    }

    #endregion

    #region ��k
    private void Tarck()
    {
        Vector3 posCamear = transform.position;            //��v���y��
        Vector3 posTraget = traget.position;               //���a�y��

        //�B���y��
        Vector3 posResult = Vector3.Lerp(transform.position, traget.position, speed * Time.deltaTime);

        //��v���^�w�]
        posResult.z = -10;

        posResult.x = Mathf.Clamp(posResult.x, limitHorizontol.x, limitHorizontol.y);
        posResult.y = Mathf.Clamp(posResult.y, limitHorizontol.x, limitHorizontol.y);

        //������y�Ь��B���y��
        transform.position = posResult;

    }

    #endregion
}

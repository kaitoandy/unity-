using UnityEngine;

/// <summary>
/// �ܨ��t��
/// </summary>
public class TransformSystem : MonoBehaviour
{
    #region ���:���}
    [Header("���a�ܨ��e��ҫ�����")]
    public GameObject goTransformBefore;
    public GameObject goTransformAfter;

    public vThirdPersonCamera cam;

    // �R�A��ƯS��
    // 1.���|��ܦb��ƭ��O
    // 2.�����������|�٭�
    // 3.�s���覡 ���O.�R�A��ƦW��
    /// <summary>
    /// �O�_�ܨ�
    /// </summary>
    public static bool isTransform;
    #endregion




    #region ���:�p�H
    #endregion

    #region �ƥ�
    private void Update()
    {
        TransformSwitch();
    }

    #endregion

    /// <summary>
    /// �ܨ�����
    /// ���UR�s��ռҫ�
    /// </summary>
    #region ��k:�p�H
    private void TransformSwitch()
    {
        //���UR�� �ܨ��e��ҫ���ܪ��A�P�쥻�A��
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


            //��v���ؼг]�w���ثe��ܪ��ҫ�
            if (goTransformBefore.activeInHierarchy) cam.SetTarget(goTransformBefore.transform);
            else if (goTransformAfter.activeInHierarchy) cam.SetTarget(goTransformAfter.transform);

            

        }

    }

        

}

#endregion


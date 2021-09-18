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
            goTransformBefore.SetActive(!goTransformBefore.activeInHierarchy);
            goTransformAfter.SetActive(!goTransformAfter.activeInHierarchy);


            //��v���ؼг]�w���ثe��ܪ��ҫ�
            if (goTransformBefore.activeInHierarchy) cam.SetTarget(goTransformBefore.transform);
            else if (goTransformAfter.activeInHierarchy) cam.SetTarget(goTransformAfter.transform);

            //�P�B�y��
            //goTransformBefore.transform.position = goTransformAfter.transform.position;
            // goTransformAfter.transform.position = goTransformBefore.transform.position;

        }

    }

        

}

#endregion


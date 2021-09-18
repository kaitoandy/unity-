using UnityEngine;

/// <summary>
/// �s�q�����P����
/// </summary>
public class AttackSystem : MonoBehaviour
{
    #region ���:���}
    [Header("�ѼƦW��")]
    public string parAttackPart = "����q��";
    public string parAttackGather = "�������";
    [Header("�s�����j�ɶ�"), Range(0, 2)]
    public float intervalBetweenAttackPart = 0.2f;
    [Header("����ɶ�"), Range(0, 2)]
    public float timeToAttackGather = 1;

    #endregion

    #region ���:�p�H
    private Animator ani;
    /// <summary>
    /// ���a���U���䪺�ɶ�
    /// </summary>
    private float timer;

    #endregion

    #region �ƥ�
    //����ƥ�:�C�������H��Start���椧�e����@��
    private void Awake()
    {
        ani.GetComponent<Animator>();
    }

    //�}�l�ƥ�:�C�����񤧫�H��Awake���椧�����@��
    private void Start()
    {

    }

    private void Update()
    {
        ClickTime();
    }

    #endregion

    #region ��k:�p�H
    /// <summary>
    /// �I���᪺�ɶ��֥[
    /// </summary>
    private void ClickTime()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))                    //������
        {
            timer += Time.deltaTime;                             //�֥[ �p�ɾ�

        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))                 //��}����
        {
            if (timer >= timeToAttackGather)                     //�p�G �p�ɾ� >= ����ɶ�
            {
                AttackGather();
            }
            else
            {
                print("����ɶ�����");
            }
            timer = 0;                                           //�p�ɾ� �k�s
        }

        print("���U���䪺�ɶ�:" + timer);

    }

    /// <summary>
    /// �������
    /// </summary>
    private void AttackGather()
    {
        ani.SetTrigger(parAttackGather);
    }
    #endregion
}

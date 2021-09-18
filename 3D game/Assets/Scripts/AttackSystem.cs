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
    public float[] intervalBetweenAttackPart = { 0.4f, 0.5f, 0.9f };
    [Header("����ɶ�"), Range(0, 2)]
    public float timeToAttackGather = 1;
    [Header("�����q��"), Range(0, 10)]
    public int countAttackParMax = 3;

    #endregion

    #region ���:�p�H
    private Animator ani;
    /// <summary>
    /// ���a���U���䪺�ɶ�
    /// </summary>
    private float timerAttackGather;

    /// <summary>
    /// �s�q�p�ɾ�
    /// </summary>
    private float timerAttackPart;
    /// <summary>
    /// �����q��
    /// </summary>
    private int countAttackPart;

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
        if (Input.GetKeyDown(KeyCode.Mouse0))                                 //������
        {
            timerAttackGather += Time.deltaTime;                              //�֥[ �p�ɾ�����
            timerAttackPart += Time.deltaTime;                                //�֥[ �p�ɾ��q��
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))                              //��}����
        {
            if (timerAttackGather >= timeToAttackGather)                      //�p�G �p�ɾ� >= ����ɶ�
            {
                AttackGather();
            }
            else
            {
                AttackPart();
            }
            timerAttackGather = 0;                                             //�p�ɾ� �k�s
        }

        

    }

    /// <summary>
    /// �������
    /// </summary>
    private void AttackGather()
    {
        
        ani.SetTrigger(parAttackGather);
    }

    /// <summary>
    /// �����q��
    /// </summary>
    private void AttackPart()
    {
        if (timerAttackPart <= intervalBetweenAttackPart[countAttackPart])                       //�p�G�p�ɾ��q�� <= �q�ƶ��j
        {
            
            CancelInvoke();
            Invoke("RestorAttackParCountToZero", intervalBetweenAttackPart[countAttackPart]);
            countAttackPart++;                                                                   //�W�[�q��
        }
        else
        {
            countAttackPart = 0;                                                                 //�q���k�s
        }
        timerAttackPart = 0;                                                                      //�p�ɾ��k�s
        ani.SetInteger(parAttackPart,countAttackPart);
        if (countAttackPart == countAttackParMax) countAttackPart = 0;
    }
    
    private void RestorAttackParCountToZero()
    {
        countAttackPart = 0;
        ani.SetInteger(parAttackPart, countAttackPart);
    }

    #endregion
}

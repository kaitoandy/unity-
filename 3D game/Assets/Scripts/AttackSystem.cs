using UnityEngine;
using System.Collections;
using Invector.vCharacterController;
/// <summary>
/// �s�q�����P����
/// </summary>
public class AttackSystem : MonoBehaviour
{
    #region ���:���}
    [Header("�ѼƦW��")]
    public string parAttackPart = "����q��";
    public string parAttackGather = "���𶰮�";
    [Header("�s�����j�ɶ�"), Range(0, 2)]
    public float[] intervalBetweenAttackPart = { 0.4f, 0.5f, 0.9f };
    [Header("����ɶ�"), Range(0, 2)]
    public float timeToAttackGather = 1;
    [Header("�����q��"), Range(0, 10)]
    public int countAttackParMax = 3;
    [Header("�����������:�����O �ؤo �첾"),Range(0,500)]
    public float[] attack= {10,20,30,40};
    public Vector3[] areaAttackSize; 
    public Vector3[] areaAttackOffst;
    public Color[] areaAttackColor;
    public float[] delaySendAttackToTarget;

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
        ani = GetComponent<Animator>();
    }

    //�}�l�ƥ�:�C�����񤧫�H��Awake���椧�����@��
    private void Start()
    {

    }

    public vThirdPersonCamera v;
    public AvatarMask am;


    private void Update()
    {
        ClickTime();
    }

    private void OnDrawGizmos()
    {
        #region �����d��
        for (int i = 0; i < attack.Length; i++)
        {
            Gizmos.color = areaAttackColor[i];
            Gizmos.matrix = Matrix4x4.TRS(transform.position +
                transform.right * areaAttackOffst[i].x +
                transform.up * areaAttackOffst[i].y +
                transform.forward * areaAttackOffst[i].z,
                transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, areaAttackSize[i]);
        }
        

        #endregion
    }

    #endregion

    #region ��k:�p�H
    /// <summary>
    /// �I���᪺�ɶ��֥[
    /// </summary>
    private void ClickTime()
    {
        //�ܨ���.�����Ҧ����ܨ�����
        //���o��L�}����T�����
        //1. bool isTransform = GameObject.Find("�ܨ��t��").GetComponent<TransformSystem>().isTransform;
        //2. �N�n���o��Ƨאּ�R�A
         bool isTransform = TransformSystem.isTransform;

        if (isTransform && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("�ܨ������");
            return;
        }



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
        StartCoroutine(AttackAreaCheck(3));
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
            StartCoroutine(AttackAreaCheck(countAttackPart));
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
    /// <summary>
    /// �����ϰ��ˬd:�ˬd�O�_�������ؼ�
    /// </summary>
    /// <param name="indexAttack"></param>
    /// <returns></returns>
    private IEnumerator AttackAreaCheck(int indexAttack)
    {
        yield return new WaitForSeconds(delaySendAttackToTarget[indexAttack]);

        Collider[] hits = Physics.OverlapBox(transform.position +
            transform.right * areaAttackOffst[indexAttack].x +
            transform.up * areaAttackOffst[indexAttack].y +
            transform.forward * areaAttackOffst[indexAttack].z,
            areaAttackSize[indexAttack] / 2, Quaternion.identity, 1 << 6);         //�󴫭n�������ϼh

        hits[0].GetComponent<DamageSystem>().Damage(attack[indexAttack]);
    }

    private void RestorAttackParCountToZero()
    {
        countAttackPart = 0;
        ani.SetInteger(parAttackPart, countAttackPart);
    }

    #endregion
}

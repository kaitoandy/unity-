using UnityEngine;

/// <summary>
/// ���Ⱥ޲z��
/// </summary>
public class MissionManager : MonoBehaviour
{
    #region ���:���}

    /// <summary>
    /// �w�q�C�|:��t�λ����C�|�W�٤Υ]�t���ǿﶵ
    /// ���Ȫ��A:�����ȫe, ���ȶi�椤,���ȧ���
    /// </summary>
    public enum StateMission
    {
        MissionBefore,Missionning ,MissionFinish
    }

    [Header("���Ȫ��A")]
    public StateMission state;
    [Header("��e���d��ܸ��")]
    public DialogueData date;
    /// <summary>
    /// ���Ⱥ޲z�����骫��
    /// </summary>
    public static MissionManager instance;
    #endregion

    #region ���:�p�H
    #endregion

    #region �ƥ�
    private void Awake()
    {
        instance = this;     //���骫�� = ������
    }
    #endregion

    #region ��k:���}
    /// <summary>
    /// �N���Ȫ��A�אּ�i�椤
    /// </summary>
    public void ChangeStateToMissionning()
    {
        state = StateMission.Missionning;
    }
    /// <summary>
    /// ��s���ȻݨD�ƶq
    /// </summary>
    /// <param name="count">�n��s���ƶq</param>
    public void UpdateMissionCount(int count)
    {
        date.coundNeed -= count;

        if (date.coundNeed == 0) MissionFinish();
    }

    private void MissionFinish()
    {
        state = StateMission.MissionFinish;
       // StartCoroutine(DialogueSystem.instance.ShowEveryDialogue(date.dialogueContentsFinish,false,true));
    }
    #endregion
}

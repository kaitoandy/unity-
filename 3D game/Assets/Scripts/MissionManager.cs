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
    #endregion

    #region ���:�p�H
    #endregion

    #region �ƥ�
    #endregion

    #region ��k:���}
    /// <summary>
    /// �N���Ȫ��A�אּ�i�椤
    /// </summary>
    public void ChangeStateToMissionning()
    {
        state = StateMission.Missionning;
    }

    #endregion
}

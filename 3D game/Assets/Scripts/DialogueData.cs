using UnityEngine;

/// <summary>
/// �x�s��ܸ��
/// 1. ��ܪ̦W��
/// 2. ��ܤ��e - �h�q
/// </summary>
/// 

//�~�����O�אּ ScriptableObject �}���ƪ���
//�N�����O�����e�i�H�إߪ�����x�s��M�פ�
//�ݷf�t CreateAssetMenu �إ߯����ﶵ�ӫإߦ�����
[CreateAssetMenu(menuName ="kaitoandy/��ܸ��" , fileName = "��ܸ��")]
public class DialogueData : ScriptableObject
{
    [Header("��ܪ̦W��")]
    public string dialogueTalkerName;
    [Header("��ܤ��e") ,TextArea(2,5)]
    public string[] dialogueContents;
    [Header("����ܥ��Ȫ��ݨD�q"), Range(0, 100)]
    public float coundNeed = 2;
    [Header("�������e"), TextArea(2, 5)]
    public string[] dialogueContentsFinish;


}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// ��ܨt��
/// 1. �M�w���ܪ̦W��
/// 2. �M�w��ܤ��e - �i�H���h�q
/// 3. ��ܨC�Ӭq����ܧ������ʺA�ĪG
/// </summary>
public class DialogueSystem : MonoBehaviour
{
    [Header("��ܸ��")]
    public DialogueData date;
    [Header("��ܶ��j") , Range(0, 3)]
    public float invertal = 0.2f;
    [Header("��ܧ����ϥ�")]
    public GameObject goFinishIcon;
    [Header("��r����: ���ܪ̦W��,��ܤ�r���e")]
    public Text textTalker;
    public Text textContent;
    /// <summary>
    /// ��ܨt�εe��: �s�դ���
    /// </summary>
    private CanvasGroup groupDialogue;

    [Header("�~���ܪ�����")]
    public KeyCode kcContinute = KeyCode.Mouse0;

    [Header("���r����")]
    public AudioClip soundType;

    private AudioSource aud;

    [Header("���r���q"), Range(0, 2)]
    public float volume = 1;

    [Header("���Ⱥ޲z��")]
    public MissionManager missionManager;

    public static DialogueSystem instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        aud = GetComponent<AudioSource>();
        groupDialogue = transform.GetChild(0).GetComponent<CanvasGroup>();
        StartDialogue();
    }

    /// <summary>
    /// �}�l���
    /// </summary>
    private void StartDialogue()
    {
        StartCoroutine(ShowEveryDialogue(date.dialogueContents,true,false));
    }

    /// <summary>
    /// ��ܨC�q���:�æb�q���������ݪ��a���~�����
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowEveryDialogue(string[] contents, bool changToMissionning,bool loadNextScene)
    {
        groupDialogue.alpha = 1;                                      //��ܹ�ܵe�� - �z���׬�1
        textTalker.text = date.dialogueTalkerName;                    //��ܹ�ܪ̦W��
        textContent.text = "";                                        //��ܤ��e�M��

        
        for (int i = 0; i < contents.Length; i++)         //�j��,����C�Ӭq��
        {
           
            for (int j = 0; j < contents[i].Length; j++)  //�j��,����C�Ӭq�������C�@�Ӧr
            {
                
                textContent.text += contents[i][j];       //��s��ܤ��e
                aud.PlayOneShot(soundType, volume);                   //���񭵮�
                yield return new WaitForSeconds(invertal);            //���r���j
            } 
             
            goFinishIcon.SetActive(true);                             //�C�q�ܧ�������ܧ����ϥ�

            
            while (!Input.GetKeyDown(kcContinute))                    //���ݪ��a���~����s - �ϥ�null ���C�V���ɶ�
            {
                yield return null;
            }
            textContent.text = "";                                    //���a���U�ť����M�Ť��e
            goFinishIcon.SetActive(false);                            //���������ϥ�

            if (i == contents.Length - 1)
            {
                groupDialogue.alpha = 0;
                if(changToMissionning)missionManager.ChangeStateToMissionning();
                //if (loadNextScene) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
    }
}

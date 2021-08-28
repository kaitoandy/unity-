using UnityEngine;
using UnityEngine.Audio;     //�ޥ� ���� API

public class AudioControl : MonoBehaviour
{
    [Header("�V����")]
    public AudioMixer mixer;

    //�Ʊ� Slider �� On Value Changed �ƥ�:�ư��ܧ�ƭȮɰ���@��
    //(singel) ���� float
    
    /// <summary>
    /// �]�wBGM���q
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolumeBGM(float volume)
    {
        mixer.SetFloat("VolumeBGM", volume);
    }
    /// <summary>
    /// �]�wSFX���q
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolumSFX(float volume)
    {
        mixer.SetFloat("VolumeSFX", volume);
    }

}

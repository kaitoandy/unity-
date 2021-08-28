using UnityEngine;
using UnityEngine.Audio;     //引用 音源 API

public class AudioControl : MonoBehaviour
{
    [Header("混音器")]
    public AudioMixer mixer;

    //滑桿 Slider 的 On Value Changed 事件:滑動變更數值時執行一次
    //(singel) 等於 float
    
    /// <summary>
    /// 設定BGM音量
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolumeBGM(float volume)
    {
        mixer.SetFloat("VolumeBGM", volume);
    }
    /// <summary>
    /// 設定SFX音量
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolumSFX(float volume)
    {
        mixer.SetFloat("VolumeSFX", volume);
    }

}

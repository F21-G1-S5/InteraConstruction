using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer audioMixerBG;
    public AudioMixer audioMixerFX;

    public void SetVolumebackground(float volume)
    {
        audioMixerBG.SetFloat("BackgroundMusic", volume);

    }
    public void SetVolumeFX(float volume)
    {
        audioMixerFX.SetFloat("FXSound", volume);
    }

}

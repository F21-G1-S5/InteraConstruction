using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Provides methods for adjusting BG and FX channels of the given audio mixer.
/// </summary>
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

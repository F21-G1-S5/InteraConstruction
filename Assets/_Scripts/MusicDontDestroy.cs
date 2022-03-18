using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Preserves an audio source between scenes by designating its parent not be destroyed on unload.
/// </summary>
public class MusicDontDestroy : MonoBehaviour
{
    public AudioSource ad;
  
    void Awake()
    {
        DontDestroyOnLoad(ad);
        //This is played every time the script is called.
    }
}

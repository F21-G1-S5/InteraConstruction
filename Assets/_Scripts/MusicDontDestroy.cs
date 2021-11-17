using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDontDestroy : MonoBehaviour
{
    public AudioSource ad;
  
    void Awake()
    {
        DontDestroyOnLoad(ad);
        //This is played every time the script is called.
    }
}

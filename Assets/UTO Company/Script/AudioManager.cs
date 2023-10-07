using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Audio is Null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public AudioClip audioClip;
    public AudioSource audioSourceSFX;
    public AudioSource audioSourceBGM;
    private bool isPlayBGM;

    private void Start()
    {
        
        
    }

    public void OnPlayOneShot(AudioClip clip, float volume = 1)
    {
        //audioSourceSFX.PlayOneShot(clip,volume);
    }
    
    public void OnPlayOneShotOnTarget(AudioSource source,AudioClip clip,float volume=1)
    {
        source.PlayOneShot(clip,volume);
    }

    public void OnPlayBGM(bool isPlay)
    {
        if (isPlay)
        {
            audioSourceBGM.Play();
        }
        else
        {
            audioSourceBGM.Stop();
        }
        
    }

    public void OnChangeBGM(AudioClip clip)
    {
        audioSourceBGM.clip = clip;
    }
}

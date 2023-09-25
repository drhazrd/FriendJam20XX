using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource sfxSource;
    public AudioSource bgmSource;

    void Awake(){
         if(AudioManager.instance == null){
            instance = this;
        } else if (AudioManager.instance != this && AudioManager.instance != null){
            Destroy(this);
        }
    }
    public void PlayClip(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip){
        bgmSource.clip = clip;
    }
}

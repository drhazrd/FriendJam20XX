using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake(){
         if(AudioManager.instance == null){
            instance = this;
        } else if (AudioManager.instance != this && AudioManager.instance != null){
            Destroy(gameObject);
        }
        //playerControls = new PlayerControls();

    }
    public void PlayClip(AudioClip clip){
        //play clip oneshot
    }
}

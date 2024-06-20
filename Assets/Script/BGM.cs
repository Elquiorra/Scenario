using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource audioSource;

    void Update()
    {
        if(PauseMenu.GameIsPaused){
            audioSource.volume = 0.05f;
        }
        else{
            audioSource.volume = 0.15f;
        }
    }
}

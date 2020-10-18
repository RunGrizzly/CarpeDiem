using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip pauseMusic;
    public AudioClip sessionMusic;

    public AudioSource sessionSource;
    public AudioSource menuSource;

    //Method for changing music if its different.
    public void SetPlayClip(AudioSource source, AudioClip clip)
    {

        if (clip != source.clip)
        {

            source.clip = clip;
            source.Play();

        }

    }

}
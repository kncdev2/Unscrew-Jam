using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HG;
public static class SoundEx 
{
    public static void Play(this AudioClip audioClip,float volume = 1)
    {
        SoundManager.I.SetVolume(volume);
        SoundManager.I.PlayOneShot(audioClip);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;

    public AudioSource audioSrc => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 1f, float p1 = 0.85f, float p2 = 1.2f)
    {
        audioSrc.PlayOneShot(clip, volume);
    }
}
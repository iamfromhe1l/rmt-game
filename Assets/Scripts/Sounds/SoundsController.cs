using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    [System.Serializable]
    public class SoundsController : MonoBehaviour
    {
        public AudioClip[] sounds;
        public AudioSource AudioSource => GetComponent<AudioSource>();
        public void PlaySound(AudioClip clip, float volume = 1f, float p1 = 0.85f, float p2 = 1.2f, bool destroyed = false)
        {
            AudioSource.pitch = Random.Range(p1, p2);
            if (destroyed)
                AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            else
                AudioSource.PlayOneShot(clip, volume);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : MonoBehaviour
{
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }
    public void PlaySound()
    {
        if (!sound.isPlaying)
        {
            sound.PlayOneShot(sound.clip);
        }
        else
        {
            sound.Stop();
            sound.PlayOneShot(sound.clip);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip jumpInEnemySFX;
    public AudioClip Coin;
    public AudioClip ExtraPoints;
    public AudioClip FireTraps;
    public AudioClip SawTraps;
    public AudioClip deathSFX;
    public AudioClip bgm;
    public static AudioController current;

    private AudioSource audioSource;


    void Start()
    {
        current = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}

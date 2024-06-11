using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptAudio : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip background;
    public AudioClip shot;
    public AudioClip reload;
    public AudioClip empty;
    public AudioClip item;
    void Start()
    {
        musicSource.clip = background;
        musicSource.volume = 0.2f;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.volume = 0.1f;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayLoopSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.loop = true;
        sfxSource.volume = 0.5f;
        sfxSource.Play();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
        sfxSource.loop = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource music;
    public AudioSource sfx;
    public AudioSource pausableSfx;

    public AudioClip[] Music;
    public AudioClip[] SFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(string name)
    {
        foreach (AudioClip sound in SFX)
        {
            if (sound.name == name)
            {
                sfx.PlayOneShot(sound);
                return;
            }
        }
    }

    public void PlayPausableSFX(string name)
    {
        foreach (AudioClip sound in SFX)
        {
            if (sound.name == name)
            {
                pausableSfx.clip = sound;
                pausableSfx.Play();
                return;
            }
        }
    }

    public void PauseSFX()
    {
        pausableSfx.Stop();
    }

    public void PlayMusic(string name)
    {
        foreach (AudioClip song in Music)
        {
            if (song.name == name)
            {
                music.clip = song;
                music.Play();
                return;
            }
        }
    }

    public IEnumerator FadeOut()
    {
        while (music.volume > 0)
        {
            music.volume -= 0.1f;
            yield return null;
        }

        music.volume = 0;
        music.Stop();
    }
}

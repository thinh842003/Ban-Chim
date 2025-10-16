using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [Header("Settings:")]
    [Range(0, 1)]
    public float MusicVolume;
    [Range(0, 1)]
    public float soundVolume;

    public AudioSource MusicAus;
    public AudioSource SoundAus;

    [Header("Game Sounds and Musics:")]
    public AudioClip shooting;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip[] BackgroundMusic;

    public override void Start()
    {
        PlayMusic(BackgroundMusic);
    }

    public void PlaySound(AudioClip sound, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = SoundAus;
        }

        if (aus)
        {
            aus.PlayOneShot(sound, soundVolume);
        }
    }

    public void PlaySound(AudioClip[] sounds, AudioSource aus = null)
    {
        if(!aus)
        {
            aus = SoundAus;
        }

        if (aus)
        {
            int randomIndex = Random.Range(0, sounds.Length);

            if (sounds[randomIndex] != null)
            {
                aus.PlayOneShot(sounds[randomIndex], soundVolume);
            }
        }
    }

    public void PlayMusic(AudioClip music, bool loop = true)
    {
        if (MusicAus)
        {
            MusicAus.clip = music;
            MusicAus.loop = loop;
            MusicAus.volume = MusicVolume;
            MusicAus.Play();
        }
    }

    public void PlayMusic(AudioClip[] music, bool loop = true)
    {
        if (MusicAus)
        {
            int randomIndex = Random.Range(0, music.Length);

            if (music[randomIndex] != null)
            {
                MusicAus.clip = music[randomIndex];
                MusicAus.loop = loop;
                MusicAus.volume = MusicVolume;
                MusicAus.Play();
            }
            
        }
    }
}

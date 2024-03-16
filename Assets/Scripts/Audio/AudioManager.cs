using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSound, sFXSounds;
    public AudioSource musicSource, sFXSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Soundtrack");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySFX("Click");
        }
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sFXSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("SFX not found");
        }
        else
        {
            sFXSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sFXSource.mute = !sFXSource.mute;
    }

    public void MusicVolume(float volume) { musicSource.volume = volume; }
    public void SFXVolume(float volume) { sFXSource.volume = volume; }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;// Static variable to hold an instance of the AudioManager

    public Sound[] musicSounds, sfxSounds;// Arrays to store music and sound effects
    public AudioSource musicSource, sfxSource, footStepsWalk, footStepsRun;// Audio sources for music and sound effects

    private void Awake()
    {
        instance= this;// Set the instance variable to this AudioManager instance
    }

    private void Start()
    {
        PlayMusic("Ambient");// Play the music with the name "NameHere" when the game starts
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);// Find the Sound object with the specified name in the musicSounds array

        if (s == null) 
        {
            Debug.Log("Sound Not Found!");// If the sound is not found, log an error message
        }
        else
        {
            musicSource.clip = s.clip;// Set the musicSource's clip to the found sound clip
            musicSource.Play();// Play the music
        }
    }
    public void PlaySFX(string name) 
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);// Find the Sound object with the specified name in the sfxSounds array

        if (s == null)
        {
            Debug.Log("Sound Not Found!");// If the sound is not found, log an error message
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);// Play the sound effect once
        }
    }

    //Audio toggle and addjustment
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;// Toggle the mute state of the musicSource
    }
    public void ToggleSFX()
    {
        sfxSource.mute= !sfxSource.mute; // Toggle the mute state of the sfxSource
        footStepsWalk.mute= !footStepsWalk.mute;
        footStepsRun.mute= !footStepsRun.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume; // Set the volume of the musicSource
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;// Set the volume of the sfxSource
        footStepsWalk.volume = volume;
        footStepsRun.volume = volume;
    }



}

﻿using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {
    /*
    public AudioSource audioP;

    public static float musicVolume = 1f;
    public static float SFXVolume = 1f;
    private static bool isPaused;

    	// Use this for initialization
	void Start () {
        //The music should be playing between scenes
        DontDestroyOnLoad(gameObject);
        audioP = GetComponent<AudioSource>();
        audioP.loop = true;
        
	}
    //Plays the music
    public static void PlayMusic()
    {
        audioP.Play();
    }
    //Plays the music from a clip
    public static void PlayMusic(AudioClip music)
    {
        audioP.clip = music;
        audioP.Play();
    }
    //Plays the music from a file
    public static void PlayMusic(string name)
    {
        AudioClip music = Resources.Load("09_Audio/" + name) as AudioClip;
        PlayMusic(music);
    }
    //Pauses or unpauses the music
    public static void PauseMusic()
    {
        if (isPaused)
        {
            audioP.UnPause();
        }else
        {
            audioP.Pause();
        }
        
    }
	
    //Mute or unmute the audio player
	public static void Mute()
    {
        audioP.mute = !audioP.mute;
    }
    //Change the volume of the audioplayer between 0 and 1
    public static void ChangeVolume(float vol)
    {
        audioP.volume = Mathf.Clamp01(vol);
    }
    //Play sound effects
    public static void PlaySound(AudioClip clip)
    {
        audioP.PlayOneShot(clip, SFXVolume);
    }
    //Play sound effects from path
    public static void PlaySound(string name)
    {
        AudioClip sound = Resources.Load("09_Audio/" + name) as AudioClip;
        PlaySound(sound);
    }*/

    //Only instance of one audioplayer
    public static AudioPlayer instance;

    //Get the audio player
    AudioSource audioP;

    public float musicVolume = 1f;
    public float SFXVolume = 1f;
    private bool isPaused;

    void Start()
    {
        //The music should be playing between scenes
        DontDestroyOnLoad(gameObject);
        audioP = GetComponent<AudioSource>();
        audioP.loop = true;
    }

    //When the game starts
    void Awake()
    {
        if (instance == null) {
            //Make this audio player the only audio player
            instance = this;
        }
    }

    //Plays the music
    public void PlayMusic()
    {
        audioP.Play();
    }
    //Plays the music from a clip
    public void PlayMusic(AudioClip music)
    {
        audioP.clip = music;
        audioP.Play();
    }
    //Plays the music from a file
    public void PlayMusic(string name)
    {
        AudioClip music = Resources.Load("09_Audio/" + name) as AudioClip;
        PlayMusic(music);
    }
    //Pauses or unpauses the music
    public void PauseMusic()
    {
        if (isPaused)
        {
            audioP.UnPause();
        }
        else
        {
            audioP.Pause();
        }

    }
    //Mute or unmute the audio player
    public void Mute()
    {
        audioP.mute = !audioP.mute;
    }
    //Change the volume of the audioplayer between 0 and 1
    public void ChangeVolume(float vol)
    {
        audioP.volume = Mathf.Clamp01(vol);
    }
    //Play sound effects
    public void PlaySound(AudioClip clip)
    {
        audioP.PlayOneShot(clip, SFXVolume);
    }
    //Play sound effects from path
    public void PlaySound(string name)
    {
        AudioClip sound = Resources.Load("09_Audio/" + name) as AudioClip;
        PlaySound(sound);
    }



}

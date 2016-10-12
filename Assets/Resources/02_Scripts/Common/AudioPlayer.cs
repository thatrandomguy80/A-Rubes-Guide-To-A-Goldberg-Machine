using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioPlayer : GameState {

    #region Members
    //Only instance of one audioplayer
    public static AudioPlayer instance;
    //Get the audio player
    AudioSource audioP;
    public float musicVolume = 1f;
    public float SFXVolume = 1f;
    private bool isPaused;

    private int currentWorldSelected;

    #endregion
    #region AudioTracks
    //Gets all the tracks that can be loaded in
    public AudioClip menu, world1,world2,world3,world4;
    private AudioClip[] tracks;
    #endregion
    void Start()
    {
        //Fix this later cause this is a horrible way to implement it
        tracks = new AudioClip[5];
        tracks[0] = menu;
        tracks[1] = world1;
        tracks[2] = world2;
        tracks[3] = world3;
        tracks[4] = world4;

		//If there is already an audio player destroy this audio player
		if (instance.audioP != null) {
			Destroy (gameObject);
		}
        currentWorldSelected = 0;
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
    
    void Update()
    {
        ChangeTracksBetweenSections();
    }
    //Handles music changes between worlds
    private void ChangeTracksBetweenSections()
    {
        int fadeSpeed = 1;
        int currentScene = SceneManager.GetActiveScene().buildIndex -1;
        int worldNum = PreGame.getCurrentWorldAndLevel(currentScene)[0];

        
        
        //If its in the menu or level select
        if (currentScene < PreGame.nonGameLevels)
        {
            if (currentWorldSelected != 0)
            {
                FadeInNewTrack(tracks[0], fadeSpeed);
                currentWorldSelected = 0;
                return;
            }
        //else if its one of the worlds
        }else
        {
            //Get the current world
            int currentLevelWorld = PreGame.getCurrentWorldAndLevel(currentScene)[0];

            //if the levels current world equals the world track and it not already playing
            for(int i = 1; i < tracks.Length; i++)
            {
                if(currentLevelWorld == i && currentWorldSelected != i)
                {
                    //Fade in the new track
                    FadeInNewTrack(tracks[i], fadeSpeed);
                    currentWorldSelected = i;
                    return;
                }
            }
        }
        
    }

    public void FadeInNewTrack(AudioClip newClip,float fadeSpeed)
    {
        StartCoroutine(crossFadeToNewTrack(newClip, fadeSpeed));
    }
    

    //fades out the old track for the newtrack
    IEnumerator crossFadeToNewTrack(AudioClip newClip,float transSpeed)
    {
        //Gets a new track ready and turns the volume to zero
        AudioSource newAudioP = gameObject.AddComponent<AudioSource>();
        newAudioP.clip = newClip;
        newAudioP.loop = audioP.loop;
        newAudioP.mute = audioP.mute;
        newAudioP.Play();

        float time = 0;
        //Fades in the new track
        //Fades out the old track
        while(time < 1)
        {
            time += Time.deltaTime / transSpeed;
            audioP.volume = Mathf.Lerp(musicVolume, 0, time);
            newAudioP.volume = Mathf.Lerp(0, musicVolume, time);

            yield return new WaitForEndOfFrame();

        }
        //Destorys the old audio player and replaces it with the new one
        Destroy(audioP);
        audioP = newAudioP;
    }


}

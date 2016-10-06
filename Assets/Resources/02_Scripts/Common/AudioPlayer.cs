using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {
 
    //Only instance of one audioplayer
    public static AudioPlayer instance;

    //Get the audio player
    AudioSource audioP;

    public float musicVolume = 1f;
    public float SFXVolume = 1f;
    private bool isPaused;

    void Start()
    {
		//If there is already an audio player destroy this audio player
		if (AudioPlayer.instance.audioP != null) {
			Destroy (gameObject);
		}

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioClip newMusic = Resources.Load("09_Audio/" + "Juhani Junkala [Retro Game Music Pack] Level 3") as AudioClip;
            FadeInNewTrack(newMusic, 2f);
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
        newAudioP.loop = true;
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

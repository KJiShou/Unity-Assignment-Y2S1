using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("---------- Audio Source ----------")]
    public AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip menuBgm;
    public AudioClip menuHover;
    public AudioClip menuClickIn;
    public AudioClip menuClickOut;


    public AudioClip stageBgm;
    public AudioClip speech;
    public AudioClip playMenuOpen;
    public AudioClip playMenuClose;
    public AudioClip playMenuExpand;
    public AudioClip playMenuShrink;
    public AudioClip playMenuHover;
    public AudioClip playMenuSlider;
    public AudioClip pauseMenuResume;
    public AudioClip pauseMenuStop;



    public AudioClip changeSkinSwipe;
    public AudioClip menuSlider;
    public AudioClip stageSelectConfirm;
    public AudioClip stageClear;

    // If you want to use another sound that's not listed here, simply declare a new variable and put it here.
    // Remember to put the audio clip into the Audio Manager game object afterwards!



    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        PlayMainTheme();
    }

    public void PlayMainTheme()
    {
        musicSource.clip = menuBgm;
        musicSource.Play();
    }

    public void PlayStageTheme()
    {
        musicSource.clip = stageBgm;
        musicSource.Play();
    }



    public void PlaySFX(AudioClip clip)
    {
        /* Declared as public so other scripts can access this method
            To call this, declare this in your script:
        
            AudioManager audioManager;

            AudioManager has a tag named "Audio", so we access it by using something like:
            
            private void Awake() {
                audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            }

            Now you can simply call a specific sound effect by using:
              
            audioManager.PlaySFX(audioManager.whateverSoundYouWant);   

            Note: .whateverSoundYouWant refers to line 11 clips. You just pick whichever you want to use.
         */

        SFXSource.PlayOneShot(clip);
    }


}

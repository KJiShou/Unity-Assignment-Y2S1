using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // public AudioClip buttonHitSounds;
    // static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        // audioSrc = GetComponent<AudioSource>();
    }

    public void startFunc()
    {
        // audioSrc.PlayOneShot(buttonHitSounds);
        SceneManager.LoadScene("StageSelect");
    }

    public void customizeFunc()
    {
        SceneManager.LoadScene("ChangeSkinUI");
    }

    public void settingsFunc()
    {
        SceneManager.LoadScene("MainMenuSettings");
    }

    public void quitFunc()
    {
        //audioSrc.PlayOneShot(buttonHItSounds);
        Application.Quit();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

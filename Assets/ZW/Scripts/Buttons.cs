using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public void returnHome()
    {
        audioManager.musicSource.Stop();
        audioManager.PlaySFX(audioManager.pauseMenuStop);
        AudioManager.Instance.PlayMainTheme();


        SceneManager.LoadScene("StageSelect");
    }
    
    public void toMainScene() {
        audioManager.musicSource.Stop();
        audioManager.PlaySFX(audioManager.pauseMenuResume);
        AudioManager.Instance.PlayStageTheme();


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextStage()
    {
        audioManager.musicSource.Stop();
        audioManager.PlaySFX(audioManager.pauseMenuResume);
        AudioManager.Instance.PlayStageTheme();

        SceneManager.LoadScene("1-3");
    }
}

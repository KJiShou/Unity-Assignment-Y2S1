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
        audioManager.PlaySFX(audioManager.pauseMenuStop);
        SceneManager.LoadScene("StageSelect");
    }
    
    public void toMainScene() {
        audioManager.PlaySFX(audioManager.pauseMenuResume);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextStage()
    {
        SceneManager.LoadScene("1-3");
    }
}

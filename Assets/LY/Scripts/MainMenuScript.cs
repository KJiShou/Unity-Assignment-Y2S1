using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void startFunc()
    {
        audioManager.PlaySFX(audioManager.menuClickIn);
        SceneManager.LoadScene("StageSelect");
    }

    public void customizeFunc()
    {
        audioManager.PlaySFX(audioManager.menuClickIn);
        SceneManager.LoadScene("ChangeSkinUI");
    }

    public void settingsFunc()
    {
        audioManager.PlaySFX(audioManager.menuClickIn);
        SceneManager.LoadScene("MainMenuSettings");
    }

    public void quitFunc()
    {
        audioManager.PlaySFX(audioManager.menuClickIn);
        Application.Quit();
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}

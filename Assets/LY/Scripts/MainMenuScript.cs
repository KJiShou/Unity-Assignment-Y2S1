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


    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlaySFX(audioManager.menuBgm);
    }




    public void startFunc()
    {
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
        Application.Quit();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

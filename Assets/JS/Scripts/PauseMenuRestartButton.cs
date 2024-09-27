using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuRestartButton : MonoBehaviour
{
    AudioManager audioManager;
    public Button generateButton;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (generateButton != null)
        {
            // Add listener to call GenerateCanvas() when the button is clicked
            generateButton.onClick.AddListener(toMainScene);
        }
        else
        {
            Debug.LogError("Button not assigned in the Inspector!");
        }
    }

    private void toMainScene() {

        audioManager.PlaySFX(audioManager.pauseMenuStop);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

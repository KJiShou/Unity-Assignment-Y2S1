using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMainMenuButton : MonoBehaviour
{
    public Button generateButton;
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
        SceneManager.LoadScene("StageSelect");
    }
}

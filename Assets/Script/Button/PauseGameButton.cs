using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameButton : MonoBehaviour
{
    AudioManager audioManager;
    public Button generateButton;  // Assign your button in the Inspector
    public GameObject canvasPrefab;  // Assign your Canvas prefab in the Inspector
    private GameObject instantiatedCanvas;  // To track the instantiated Canvas

    private GameObject[] playerUIs;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        
        
        if (generateButton != null)
        {
            // Add listener to call GenerateCanvas() when the button is clicked
            generateButton.onClick.AddListener(GenerateCanvas);
        }
        else
        {
            Debug.LogError("Button not assigned in the Inspector!");
        }
    }

    // Method to generate the Canvas from a prefab
    void GenerateCanvas()
    {
        // Find all game objects with the tag "PlayerUI"
        playerUIs = GameObject.FindGameObjectsWithTag("PlayerUIs");
        audioManager.musicSource.Stop();
        audioManager.PlaySFX(audioManager.pauseMenuStop);

        AudioManager.Instance.PlayPauseTheme();

        if (canvasPrefab != null)
        {
            // Check if a canvas is already instantiated
            if (instantiatedCanvas == null)
            {
                // Instantiate the canvas prefab
                instantiatedCanvas = Instantiate(canvasPrefab);

                Debug.Log("Canvas generated from prefab.");
            }
            else
            {
                Debug.Log("Canvas already exists.");
            }
        }
        else
        {
            Debug.LogError("Canvas prefab is not assigned in the Inspector!");
        }
        foreach (GameObject playerUI in playerUIs)
        {
            // Check if the GameObject has a Canvas component
            Canvas canvasComponent = playerUI.GetComponent<Canvas>();
            if (canvasComponent != null)
            {
                // Set the Canvas component to disabled
                canvasComponent.enabled = false;
            }
        }
    }

    // Optional method to destroy the instantiated Canvas
    public void DestroyCanvas()
    {
        if (instantiatedCanvas != null)
        {
            Destroy(instantiatedCanvas);
            instantiatedCanvas = null;
            Debug.Log("Canvas destroyed.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    AudioManager audioManager;
    public Button destroyButton; // Assign your destroy button in the Inspector
    private GameObject[] playerUIs;
    private Timer timer;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        // Find all game objects with the tag "PlayerUI"
        playerUIs = GameObject.FindGameObjectsWithTag("PlayerUIs");
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        // Add listener to the button's click event
        if (destroyButton != null)
        {
            destroyButton.onClick.AddListener(DestroyParent);
        }
        else
        {
            Debug.LogError("Destroy button not assigned.");
        }
    }

    // Method to destroy the button's parent GameObject
    void DestroyParent()
    {
        audioManager.musicSource.Stop();
        audioManager.PlaySFX(audioManager.pauseMenuResume);

        AudioManager.Instance.PlayStageTheme();

        timer.ContinueCountdown();
        foreach (GameObject playerUI in playerUIs)
    {
        // Check if the GameObject has a Canvas component
        Canvas canvasComponent = playerUI.GetComponent<Canvas>();
        if (canvasComponent != null)
        {
            // Set the Canvas component to disabled
            canvasComponent.enabled = true;
        }
    }
        // Check if the button has a parent GameObject
        if (transform.parent != null)
        {
            // Destroy the parent of the button
            Destroy(transform.parent.gameObject);
            Debug.Log("Parent GameObject destroyed.");
        }
        else
        {
            Debug.LogWarning("Button has no parent to destroy.");
        }
        
    }
}

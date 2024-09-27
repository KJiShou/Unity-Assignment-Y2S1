using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    AudioManager audioManager;
    public Button destroyButton; // Assign your destroy button in the Inspector
    private Canvas AddEquation;
    private Canvas EquationUI;
    private Canvas StartingButton;
    private Timer timer;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        AddEquation = GameObject.Find("Add Equation Set").GetComponent<Canvas>();
        EquationUI = GameObject.Find("Equation UI Set").GetComponent<Canvas>();
        StartingButton = GameObject.Find("Starting button").GetComponent<Canvas>();
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
        audioManager.PlaySFX(audioManager.pauseMenuResume);


        timer.ContinueCountdown();
        AddEquation.enabled = true;
        EquationUI.enabled = true;
        StartingButton.enabled = true;
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

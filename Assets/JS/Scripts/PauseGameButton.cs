using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameButton : MonoBehaviour
{
    public Button generateButton;  // Assign your button in the Inspector
    public GameObject canvasPrefab;  // Assign your Canvas prefab in the Inspector
    private GameObject instantiatedCanvas;  // To track the instantiated Canvas

    private Canvas AddEquation;
    private Canvas EquationUI;
    private Canvas StartingButton;
    void Start()
    {
        AddEquation = GameObject.Find("Add Equation").GetComponent<Canvas>();
        EquationUI = GameObject.Find("Equation UI").GetComponent<Canvas>();
        StartingButton = GameObject.Find("Starting button").GetComponent<Canvas>();
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
        AddEquation.enabled = false;
        EquationUI.enabled = false;
        StartingButton.enabled = false;
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

using UnityEngine;

public class VictoryManager : MonoBehaviour 
{
    AudioManager audioManager;

    public GameObject victoryPrefab;  // The victory menu prefab to generate
    private GameObject instantiatedVictoryMenu;  // To track the instantiated Victory Menu
    private Canvas AddEquation;
    private Canvas EquationUI;
    private Canvas StartingButton;
    void Start()
    {
        AddEquation = GameObject.Find("Add Equation Set").GetComponent<Canvas>();
        EquationUI = GameObject.Find("Equation UI Set").GetComponent<Canvas>();
        StartingButton = GameObject.Find("Starting button").GetComponent<Canvas>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Call this method when the spaceship is fully shrunk
    public void GenerateVictoryMenu()
    {
        audioManager.PlaySFX(audioManager.stageClear);

        if (victoryPrefab != null)
        {
            // Instantiate the victory prefab if it hasn't been instantiated yet
            if (instantiatedVictoryMenu == null)
            {
                instantiatedVictoryMenu = Instantiate(victoryPrefab);
                Debug.Log("Victory menu generated.");
            }
            else
            {
                Debug.Log("Victory menu already exists.");
            }

            Time.timeScale = 0;
        }
        else
        {
            Debug.LogError("Victory menu prefab is not assigned in the Inspector!");
        }
        AddEquation.enabled = false;
        EquationUI.enabled = false;
        StartingButton.enabled = false;
    }
}

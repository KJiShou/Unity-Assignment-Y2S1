using UnityEngine;

public class LoseManager : MonoBehaviour
{
    public GameObject lostPrefab;  // The victory menu prefab to generate
    private GameObject instantiatedLostMenu;  // To track the instantiated Victory Menu
    private Canvas AddEquation;
    private Canvas EquationUI;
    private Canvas StartingButton;
    void Start()
    {
        AddEquation = GameObject.Find("Add Equation Set").GetComponent<Canvas>();
        EquationUI = GameObject.Find("Equation UI Set").GetComponent<Canvas>();
        StartingButton = GameObject.Find("Starting button").GetComponent<Canvas>();
    }

    // Call this method when the spaceship is fully shrunk
    public void GenerateLoseMenu()
    {
        if (lostPrefab != null)
        {
            if (instantiatedLostMenu == null)
            {
                instantiatedLostMenu = Instantiate(lostPrefab);
                Debug.Log("Lost menu generated.");    
            }
            else
            {
                Debug.Log("Lost menu already exists.");
            }

            Time.timeScale = 0;  // Pause the game after instantiating the menu
        }
        else
        {
            Debug.LogError("Lost menu prefab is not assigned in the Inspector!");
        }
        AddEquation.enabled = false;
        EquationUI.enabled = false;
        StartingButton.enabled = false;
    }
}

using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject victoryPrefab;  // The victory menu prefab to generate
    private GameObject instantiatedVictoryMenu;  // To track the instantiated Victory Menu

    // Call this method when the spaceship is fully shrunk
    public void GenerateVictoryMenu()
    {
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
    }
}

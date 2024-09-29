using UnityEngine;
using System.Collections;

public class LoseManager : MonoBehaviour
{
    public GameObject lostPrefab;  // The victory menu prefab to generate
    private GameObject instantiatedLostMenu;  // To track the instantiated Victory Menu
    private GameObject playerUI;
    void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
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
        }
        else
        {
            Debug.LogError("Lost menu prefab is not assigned in the Inspector!");
        }
        playerUI.SetActive(false);
    }   
}

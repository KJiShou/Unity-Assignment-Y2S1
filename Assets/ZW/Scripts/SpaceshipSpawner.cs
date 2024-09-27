using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipSpawner : MonoBehaviour
{
    public GameObject[] spaceshipPrefabs;  // Array of spaceship prefabs (different models)
    public Transform spawnLocation;  // The location where the spaceship will be spawned
    public Button startButton;  // Button to trigger spaceship movement

    private GameObject spawnedSpaceship;  // Keep track of the spawned spaceship

    void Start()
    {
        SpawnPlayerSpaceship();

        // Assign the button's onClick listener to call the ActivateAndStart method when clicked
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartSpaceshipMovement);
        }
    }

    void SpawnPlayerSpaceship()
    {
        // Get the selected spaceship index and color from GameManager
        int selectedIndex = GameManager.Instance.spaceshipIndex;
        Color selectedColor = GameManager.Instance.spaceshipColor;

        // Ensure the index is within the bounds of the spaceshipPrefabs array
        if (selectedIndex >= 0 && selectedIndex < spaceshipPrefabs.Length)
        {
            // Spawn the selected spaceship prefab at the spawn location, with rotation to face right
            spawnedSpaceship = Instantiate(spaceshipPrefabs[selectedIndex], spawnLocation.position, Quaternion.Euler(0, 0, 270f));  // Rotation to face right

            // Apply the selected color to the spaceship
            SpriteRenderer spaceshipRenderer = spawnedSpaceship.GetComponent<SpriteRenderer>();
            if (spaceshipRenderer != null)
            {
                spaceshipRenderer.color = selectedColor;
            }
        }
        else
        {
            Debug.LogError("Invalid spaceship index selected.");
        }
    }

    // This method will be called when the start button is clicked
    void StartSpaceshipMovement()
    {
        if (spawnedSpaceship != null)
        {
            // Get the SpaceshipFollowLine component from the spawned spaceship and call ActivateAndStart
            SpaceshipFollowLine spaceshipFollowLine = spawnedSpaceship.GetComponent<SpaceshipFollowLine>();
            if (spaceshipFollowLine != null)
            {
                spaceshipFollowLine.ActivateAndStart();  // Start the spaceship movement
            }
            else
            {
                Debug.LogError("SpaceshipFollowLine component not found on the spawned spaceship.");
            }
        }
        else
        {
            Debug.LogError("No spaceship has been spawned.");
        }
    }
}

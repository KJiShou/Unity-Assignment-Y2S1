using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipSpawner : MonoBehaviour
{
    public GameObject[] spaceshipPrefabs;  // Array of spaceship prefabs (different models)
    public Transform spawnLocation;  // The location where the spaceship will be spawned

    private GameObject spawnedSpaceship;  // Keep track of the spawned spaceship
    private Canvas AddEquation;
    private Canvas EquationUI;
    private Canvas StartingButton;

    void Start()
    {
        GameManager.Instance.SetCurrentStageFromSceneName();
        AddEquation = GameObject.Find("Add Equation Set").GetComponent<Canvas>();
        EquationUI = GameObject.Find("Equation UI Set").GetComponent<Canvas>();
        StartingButton = GameObject.Find("Starting button").GetComponent<Canvas>();
        // Ensure spaceship spawns when the game starts
        SpawnPlayerSpaceship();

        // Automatically find the start button in the scene by its tag
        Button startButton = GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>();

        // Assign the button's onClick listener to call the OnStartButtonClick method when clicked
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }
        else
        {
            Debug.LogError("Start button not found. Make sure the button is tagged 'StartButton'.");
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
    void OnStartButtonClick()
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

            // Disable the Add Equation Set and Equation UI Canvases
            AddEquation.enabled = false;
            EquationUI.enabled = false;

            // Disable specific elements (left arrow and timer) inside the Starting Button
            Transform leftArrow = StartingButton.transform.Find("Left Arrow");  // Assuming "LeftArrow" is the name of the object

            if (leftArrow != null)
            {
                leftArrow.GetComponent<MainEquationButton>().isMenuOpen = false;
                leftArrow.gameObject.SetActive(false);  // Disable the left arrow
            }
            else
            {
                Debug.LogError("Left Arrow not found in Starting Button.");
            }
        }
        else
        {
            Debug.LogError("No spaceship has been spawned.");
        }
    }
}

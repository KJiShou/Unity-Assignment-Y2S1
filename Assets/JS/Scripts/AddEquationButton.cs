using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AddEquationButton : MonoBehaviour
{
    AudioManager audioManager;

    public Button button; // Assign your button here
    public GameObject prefabToInstantiate; // The prefab to generate on button click

    private GameObject instantiatedPrefab; // To keep track of the instantiated prefab
    private Animator animator; // Animator to handle animations

    public bool isMenuOpen = false; // A flag to track if the menu is open or closed

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (prefabToInstantiate == null)
        {
            Debug.LogError("Prefab to instantiate is not assigned.");
        }
    }

    // This method is called when the button is clicked
    void OnButtonClick()
    {
        if (isMenuOpen)
        {
            audioManager.PlaySFX(audioManager.playMenuShrink);

            // If the menu is currently open, destroy the instantiated prefab and play the 'Close' animation
            if (instantiatedPrefab != null)
            {
                animator = instantiatedPrefab.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("Close"); // Trigger the close animation
                }

                // Destroy the prefab after the animation finishes
                StartCoroutine(DestroyAfterAnimation(animator));
            }
        }
        else
        {
            audioManager.PlaySFX(audioManager.playMenuExpand);

            // If the menu is currently closed, generate the prefab and play the 'Open' animation
            if (prefabToInstantiate != null)
            {
                // Calculate a position in world space or screen space for instantiation
                Vector3 spawnPosition = CalculateSpawnPosition();

                // Instantiate the prefab at the calculated position
                instantiatedPrefab = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);

                // Play the 'Open' animation if an animator exists on the prefab
                animator = instantiatedPrefab.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("Open");
                }

                Debug.Log("Prefab instantiated at position: " + spawnPosition);
            }
        }

        // Toggle the isMenuOpen flag
        isMenuOpen = !isMenuOpen;
    }

    // Coroutine to wait for the animation to finish before destroying the prefab
    private IEnumerator DestroyAfterAnimation(Animator animator)
    {
        // Wait for the length of the 'Close' animation before destroying the prefab
        if (animator != null)
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        // Destroy the instantiated prefab after the close animation finishes
        Destroy(instantiatedPrefab);
        Debug.Log("Prefab destroyed.");
    }

    // Method to calculate the spawn position based on screen or world space
    private Vector3 CalculateSpawnPosition()
    {
        // You can use different techniques to calculate the position, such as using screen space or world space

        // Example: Instantiate based on screen position
        Vector3 screenPosition = new Vector3(Screen.width / 2, Screen.height / 2, 10); // Spawn in the middle of the screen
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition); // Convert screen position to world position

        // Adjust the Z position if necessary, as ScreenToWorldPoint may give an incorrect Z value in 3D space
        worldPosition.z = 0; // For 2D or UI purposes, keep Z at 0
        return worldPosition;
    }
}

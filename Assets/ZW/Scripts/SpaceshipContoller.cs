using System.Collections;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 5f;  // Speed of the spaceship
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component
    private Animator animator;
    public GameObject engineFire;  // The engine fire GameObject
    public GameObject headlights;
    public GameObject arealights;
    public GameObject shield;  // The shield GameObject
    public GameObject explosionPrefab;  // Explosion prefab for visual effect (optional)
    public float destructionDelay = 5f;  // Delay before destruction (in seconds)
    public float blinkInterval = 0.2f;
    public float shrinkSpeed = 2f;  // Speed at which the spaceship shrinks
    private bool isShrinking = false;  // Track if the spaceship is shrinking

    private VictoryManager victoryManager;  // Reference to VictoryManager script
    private int collisionCount = 0;  // Counter to track collisions
    private bool isExploding = false;  // To prevent multiple explosions
    private bool isBlinking = false;

    void Start()
    {
        // Get the Rigidbody2D component attached to the spaceship
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        // Get the reference to the VictoryManager in the scene
        victoryManager = FindObjectOfType<VictoryManager>();
    }

    void Update()
    {
        // Handle the shrinking effect
        if (isShrinking)
        {
            ShrinkSpaceship();
        }

        // Check if the "collide" parameter is true in the Animator
        bool isCollide = animator.GetBool("collide");

        // If the parameter is true, deactivate the engine fire
        if (isCollide)
        {
            engineFire.SetActive(false);
            
        }
        else
        {
            engineFire.SetActive(true);
        }
    }

    

    // Detect collision with Black Hole using a Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlackHole"))
        {
            // Start the shrinking process when spaceship enters the black hole
            Debug.Log("Spaceship entering the black hole!");
            StartShrinking();
        }
    }

    void StartShrinking()
    {
        // Stop the spaceship's movement
        rb.velocity = Vector2.zero;

        // Start shrinking
        isShrinking = true;

        // Disable the shield and engine fire
        engineFire.SetActive(false);
        shield.SetActive(false);
    }

    void ShrinkSpaceship()
    {
        // Gradually shrink the spaceship by reducing its scale
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);

        // Once the spaceship's scale is close to zero, call VictoryManager to generate the Victory menu
        if (transform.localScale.x <= 0.01f)
        {
            isShrinking = false;

            // Call the VictoryManager to generate the victory menu
            if (victoryManager != null)
            {
                victoryManager.GenerateVictoryMenu();
            }
            else
            {
                Debug.LogError("VictoryManager not found in the scene!");
            }
        }
    }

    // Handle collisions with asteroids (kept as-is)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isExploding) return;  // Prevent multiple explosions

        collisionCount++;  // Increment the collision counter

        if (collisionCount == 1)
        {
            // First contact: disable the shield and trigger the first collision animation
            Debug.Log("First collision! Disabling shield and setting 'collide' animation.");
            shield.SetActive(false);

            // Set the "collide" parameter in the Animator to play the first collision animation
            animator.SetBool("collide", true);

            if (headlights != null && !isBlinking)
            {
                StartCoroutine(BlinkLight());
            }
        }
        else if (collisionCount == 2)
        {
            // Second contact: trigger explosion and destroy the spaceship
            Debug.Log("Second collision! Spaceship exploding.");

            // Set the "collide2" parameter in the Animator to trigger the explosion animation
            animator.SetBool("collide2", true);

            StopBlinking();

            DisableRigidbody();

            // Proceed with explosion and delayed destruction
            TriggerExplosion();
        }
    }

    IEnumerator BlinkLight()
    {
        isBlinking = true;

        while (isBlinking)
        {
            if (headlights != null)
            {
                headlights.SetActive(!headlights.activeSelf);  // Toggle light on and off
            }
            yield return new WaitForSeconds(blinkInterval);  // Wait for blink interval
        }

        // Ensure the light is off once blinking stops
        if (headlights != null)
        {
            arealights.SetActive(false);
            headlights.SetActive(false);
        }
    }

    void StopBlinking()
    {
        isBlinking = false;
        if (headlights != null)
        {
            headlights.SetActive(false);  // Turn off the light
        }
    }

    void DisableRigidbody()
    {
        // Set the Rigidbody2D to kinematic (which stops all physics interactions)
        rb.isKinematic = true;
    }

    void TriggerExplosion()
    {
        isExploding = true;  // Set flag to prevent multiple explosions

        // Optionally, instantiate an explosion effect if you have a prefab for it
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Disable the collider to prevent further collisions after explosion
        GetComponent<Collider2D>().enabled = false;

        // Delay destruction of the spaceship to allow explosion animation to finish
        Invoke("DestroySpaceship", destructionDelay);
    }

    void DestroySpaceship()
    {
        Destroy(gameObject);  // Destroy the spaceship after the delay
    }
}

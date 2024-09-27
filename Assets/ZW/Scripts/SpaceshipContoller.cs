using System.Collections;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 5f;  // Speed of the spaceship
    public float slowSpeed = 0.5f;
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component
    private Collider2D collider; 
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
    private bool inPortal = false;  // Track if the spaceship is in a portal animation

    private VictoryManager victoryManager;  // Reference to VictoryManager script
    
    private GameObject player;
    private Animation anim;
    private Rigidbody2D playerRb;

    public int collisionCount = 0;  // Counter to track collisions
    private bool isExploding = false;  // To prevent multiple explosions
    private bool isBlinking = false;

    void Awake()
    {
        // Get the Rigidbody2D component attached to the spaceship
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRb = player.GetComponent<Rigidbody2D>();
        
        GetComponent<Collider2D>().enabled = false;

        this.enabled = false;  // Deactivate the script at the start
    }
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
            return;
        }

        // Handle the spaceship movement after the portal animations
        if (!inPortal && !isExploding)
        {
            //MoveSpaceship();
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

    // Handle spaceship movement (this will be resumed after portal animations)
    void MoveSpaceship()
    {
        rb.velocity = transform.up * speed;
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
        SlowDownSpaceship();
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

            engineFire.SetActive(false);

            StopBlinking();

            DisableRigidbody();

            // Proceed with explosion and delayed destruction
            TriggerExplosion();
        }
    }

    void SlowDownSpaceship()
    {
        Debug.Log("Spaceship hit an asteroid! Slowing down...");
        speed = slowSpeed;  // Reduce the speed when hitting an asteroid
        rb.velocity = transform.up * slowSpeed;  // Set the slower velocity
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

    // This method will be called by the PortalController after the portal animations
    public void ResumeMovement()
    {
        inPortal = false;  // Exit portal mode
        rb.simulated = true;  // Enable Rigidbody2D simulation
        // Reset velocity to move in the direction the spaceship is facing
        rb.velocity = Vector2.zero;

        // Move in the direction the spaceship is facing (in 2D, 'up' is forward)
        rb.velocity = transform.up * speed;
    }

    // Call this method when entering the portal to stop movement and animation
    public void EnterPortal()
    {
        inPortal = true;
        rb.velocity = Vector2.zero;  // Stop the spaceship during the portal animation
        rb.simulated = false;  // Disable Rigidbody2D simulation during the portal animation
    }
}
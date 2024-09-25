using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 5f;  // Speed of the spaceship
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component
    private Animator animator;
    public GameObject engineFire;  // The engine fire GameObject
    public GameObject shield;  // The shield GameObject
    public GameObject explosionPrefab;  // Explosion prefab for visual effect (optional)
    public Animator spaceshipAnimator;  // Reference to the Animator controlling the spaceship
    public float destructionDelay = 5f;  // Delay before destruction (in seconds)

    private int collisionCount = 0;  // Counter to track collisions
    private bool isExploding = false;  // To prevent multiple explosions

    void Start()
    {
        // Get the Rigidbody2D component attached to the spaceship
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Set the spaceship to move in the right direction at the start
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        // Check if the "collide" parameter is true in the Animator
        bool isCollide = spaceshipAnimator.GetBool("collide");

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
            spaceshipAnimator.SetBool("collide", true);
        }
        else if (collisionCount == 2)
        {
            // Second contact: trigger explosion and destroy the spaceship
            Debug.Log("Second collision! Spaceship exploding.");

            // Set the "collide2" parameter in the Animator to trigger the explosion animation
            spaceshipAnimator.SetBool("collide2", true);

            DisableRigidbody();

            // Proceed with explosion and delayed destruction
            TriggerExplosion();
        }
    }

    void DisableRigidbody()
    {
        // Set the Rigidbody2D to kinematic (which stops all physics interactions)
        rb.isKinematic = true;

        // You can also disable Rigidbody2D if needed:
        // rb.simulated = false;
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

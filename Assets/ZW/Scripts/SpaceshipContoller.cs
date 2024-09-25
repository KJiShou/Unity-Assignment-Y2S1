using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 5f;  // Speed of the spaceship
    public float shrinkSpeed = 2f;  // Speed at which the spaceship shrinks
    public float moveSpeed = 2f;  // Speed at which the spaceship moves toward the portal
    public GameObject engineFire;  // The engine fire GameObject
    public GameObject shield;  // The shield GameObject
    public Animator spaceshipAnimator;  // Reference to the Animator controlling the spaceship

    private Rigidbody2D rb;
    private bool isShrinking = false;  // Track if the spaceship is shrinking
    private Vector3 portalPosition;  // Position of the portal

    void Start()
    {
        // Get the Rigidbody2D component attached to the spaceship
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;  // Set the spaceship to move in the upward direction
    }

    void Update()
    {
        // Handle the shrinking effect
        if (isShrinking)
        {
            ShrinkAndMoveTowardsPortal();
        }
    }

    // Trigger when the spaceship enters the portal's collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object the spaceship collided with is tagged as "Portal"
        if (other.CompareTag("Portal"))
        {
            Debug.Log("Spaceship entering the portal!");

            // Start shrinking the spaceship and get the portal's position
            StartShrinking(other.transform.position);
        }
    }

    void StartShrinking(Vector3 targetPortalPosition)
    {
        // Set isShrinking to true to start the shrinking effect
        isShrinking = true;

        // Store the portal's position to move towards it
        portalPosition = targetPortalPosition;

        // Optionally stop the spaceship from moving (you can keep this or remove it)
        rb.velocity = Vector2.zero;
    }

    void ShrinkAndMoveTowardsPortal()
    {
        // Gradually shrink the spaceship by reducing its scale
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);

        // Move the spaceship towards the portal
        transform.position = Vector3.MoveTowards(transform.position, portalPosition, moveSpeed * Time.deltaTime);

        // Once the spaceship's scale is close to zero, destroy the object
        if (transform.localScale.x <= 0.01f)
        {
            Destroy(gameObject);  // Delete the spaceship when it has shrunk to near zero size
        }
    }
}
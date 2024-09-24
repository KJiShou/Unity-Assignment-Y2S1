using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 5f;  // Speed of the spaceship
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component
    private Animator animator;
    public GameObject engineFire;  // The engine fire GameObject
    public GameObject shield;
    public Animator spaceshipAnimator;  // Reference to the Animator controlling the spaceship


    void Update()
    {
        // Check if the "collide" parameter is true in the Animator
        bool isCollide = spaceshipAnimator.GetBool("collide");

        // If the parameter is true, deactivate the engine fire
        if (isCollide)
        {
            engineFire.SetActive(false);
            shield.SetActive(false);

        }
        else
        {
            shield.SetActive(true);
            engineFire.SetActive(true);
        }
    }
    void Start()
    {
        // Get the Rigidbody2D component attached to the spaceship
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Set the spaceship to move in the right direction at the start
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Detect collision with other objects
        Debug.Log("Spaceship collided with: " + collision.gameObject.name);

        animator.SetBool("collide",true);
    
        // Handle collision logic here, e.g., destroy the spaceship
        // Destroy(gameObject);  // Uncomment this line if you want to destroy the spaceship on collision
    }
}
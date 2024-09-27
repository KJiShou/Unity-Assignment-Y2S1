using System.Collections;
using UnityEngine;

public class SpaceshipFollowLine : MonoBehaviour
{
    public float speed = 5f;  // Speed of the spaceship
    private Vector3[] linePoints;  // Stores the points of the currently selected line
    private int currentPointIndex = 0;  // Track which point the spaceship is moving towards
    public bool isMoving = false;  // Check if the spaceship is currently moving
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component
    private bool hasLine = false;  // Check if a line has been set

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // Initialize Rigidbody2D
        this.enabled = false;  // Disable the script at the start until it's triggered
    }

    void Update()
    {
        if (isMoving)
        {
            if (hasLine && linePoints != null && currentPointIndex < linePoints.Length)
            {
                
            }
            else
            {
                if (GetComponent<SpaceshipController>().collisionCount == 0)
                    MoveForward();  // Move forward if no line is provided
            }
        }
    }

    // Start movement when the button is pressed or the portal animation finishes
    public void StartMovement()
    {
        GetComponent<Collider2D>().enabled = true;
        isMoving = true;  // Start the movement
        Debug.Log("Movement started.");
    }

    // Move forward continuously if no line is set
    void MoveForward()
    {
        // Continue moving in the current "up" direction
        transform.position += transform.up * speed * Time.deltaTime;
    }

    // Called when the portal animation finishes
    public void OnPortalAnimationFinished()
    {
        StartMovement();  // Start moving the spaceship after the portal animation
    }

    // Method to activate the script and start movement via a button click
    public void ActivateAndStart()
    {
        Debug.Log("ActivateAndStart called.");
        
        this.enabled = true;  // Enable the script
        StartMovement();
    }
}

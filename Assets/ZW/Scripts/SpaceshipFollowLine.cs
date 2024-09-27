using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipFollowLine : MonoBehaviour
{
    public float speed = 5f;  // Speed of the spaceship
    private Vector3[] linePoints;  // Stores the points of the currently selected line
    private int currentPointIndex = 0;  // Track which point the spaceship is moving towards
    private bool isMoving = false;  // Check if the spaceship is currently moving
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
                MoveAlongLine();  // Follow the line points if available
            }
            else
            {
                MoveForward();  // Move forward if no line is provided
            }
        }
    }

    // Assign a LineRendererLinear and get its line points
    public void SetLineRenderer(LineRendererLinear lineRendererLinear)
    {
        if (lineRendererLinear != null)
        {
            linePoints = lineRendererLinear.linePoints;  // Get the line points from LineRendererLinear
            currentPointIndex = 0;  // Start from the first point
            hasLine = true;  // Mark that the spaceship has a line to follow
            Debug.Log("Line set with " + linePoints.Length + " points.");
        }
    }

    // Start movement when the button is pressed or the portal animation finishes
    public void StartMovement()
    {
        GetComponent<Collider2D>().enabled = true;
        isMoving = true;  // Start the movement
        Debug.Log("Movement started.");

        if (!hasLine)
        {
            // If no line is set, just move forward in the current direction
            rb.velocity = transform.up * speed;
        }
    }

    // Move forward continuously if no line is set
    void MoveForward()
    {
        // Continue moving in the current "up" direction
        transform.position += transform.up * speed * Time.deltaTime;
    }

    // Move the spaceship towards the next point in the line
    void MoveAlongLine()
    {
        Vector3 targetPosition = linePoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= linePoints.Length)
            {
                isMoving = false;  // Stop moving when the spaceship reaches the end of the line
                Debug.Log("Spaceship has reached the end of the line.");
            }
        }
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

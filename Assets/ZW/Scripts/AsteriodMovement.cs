using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public float initialRotationSpeed = 10f;  // Initial rotation speed (slow)
    public float hitRotationSpeedMultiplier = 3f;  // Multiplier for rotation speed when hit
    public float hitForce = 10f;  // Force applied to the asteroid when hit

    private Rigidbody2D rb;
    private float currentRotationSpeed;

    void Start()
    {
        // Get the Rigidbody2D component attached to the asteroid
        rb = GetComponent<Rigidbody2D>();

        // Set the initial rotation speed
        currentRotationSpeed = initialRotationSpeed;
    }

    void Update()
    {
        // Apply continuous slow rotation
        transform.Rotate(0f, 0f, currentRotationSpeed * Time.deltaTime);
    }

    // This method will be called when the asteroid is hit
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Increase the rotation speed when hit
        currentRotationSpeed *= hitRotationSpeedMultiplier;

        // Apply a force to the asteroid in the direction of the collision
        Vector2 hitDirection = collision.contacts[0].normal * 1;  // The direction the asteroid should be pushed
        rb.AddForce(hitDirection * hitForce, ForceMode2D.Impulse);

        // Optionally, add a little random torque for a tumbling effect
        float randomTorque = Random.Range(-hitForce, hitForce);
        rb.AddTorque(randomTorque);
    }
}
using UnityEngine;

public class StarCollectibles : MonoBehaviour
{
    AudioManager audioManager;
    public int scoreValue = 1;  // The value of the collectible (default is 1 point)

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        audioManager.PlaySFX(audioManager.starCollect);

        // Assuming the player has a "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with collectible.");

            // Add score through GameManager
            GameManager.Instance.AddScore(scoreValue);
            Debug.Log("Score added: " + scoreValue);

            // After collecting, you might want to remove the star from the game
            Destroy(gameObject);
            Debug.Log("Collectible destroyed.");
        }
    }
}
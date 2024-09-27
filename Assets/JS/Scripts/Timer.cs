using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float seconds = 10f;  // Time in seconds
    public float minutes = 1f;   // Time in minutes
    public TMP_Text countdownText;  // UI Text element to display the countdown
    public bool isCountingDown = true;
    private LoseManager loseManager;  // Reference to LoseManager script

    void Start()
    {
        // Initialize the timer based on difficulty settings
        if (GameManager.Instance.difficulty == 1)
        {
            minutes = 10f;
            seconds = 0f;
        }
        else if (GameManager.Instance.difficulty == 2)
        {
            minutes = 5f;
            seconds = 0f;
        }
        else if (GameManager.Instance.difficulty == 3)
        {
            minutes = 1f;
            seconds = 0f;
        }

        // Find LoseManager in the scene if not assigned manually
        if (loseManager == null)
        {
            loseManager = FindObjectOfType<LoseManager>();
        }
    }

    void Update()
    {
        if (isCountingDown && (seconds > 0 || minutes > 0))
        {
            // Reduce seconds by the time elapsed since the last frame
            seconds -= Time.deltaTime;

            // When seconds go below 0, subtract 1 from minutes and reset seconds to 59
            if (seconds < 0 && minutes > 0)  // Corrected from seconds < -1 to seconds < 0
            {
                seconds = 59;
                minutes -= 1;
            }
            if (seconds == 30 && minutes ==0)
                countdownText.color = Color.red;
            // Update the UI text with the remaining time in MM:SS format
            countdownText.text = Mathf.Floor(minutes).ToString("00") + " : " + Mathf.Ceil(seconds).ToString("00");

            // Check if the timer has reached zero
            if (seconds <= 0 && minutes <= 0)
            {
                seconds = 0;
                minutes = 0;
                isCountingDown = false;
                OnCountdownEnd();  // Trigger an event when the countdown ends
            }
        }
    }

    // Event when the countdown finishes
    void OnCountdownEnd()
    {
        Debug.Log("Countdown finished!");
        if (loseManager != null)
        {
            loseManager.GenerateLoseMenu();  // Call LoseManager to show the lose menu
        }
        else
        {
            Debug.LogError("LoseManager not found in the scene!");  // Log error if LoseManager is not found
        }
    }

    // Method to stop the countdown
    public void StopCountdown()
    {
        isCountingDown = false;
        Debug.Log("Countdown stopped!");
    }

    // Method to continue the countdown
    public void ContinueCountdown()
    {
        isCountingDown = true;
        Debug.Log("Countdown continued!");
    }
}

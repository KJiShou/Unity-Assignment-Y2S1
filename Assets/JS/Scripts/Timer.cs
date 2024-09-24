using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float seconds = 10f;  // Time in seconds
    public float minutes = 1f;   // Time in minutes
    public TMP_Text countdownText; // UI Text element to display the countdown
    public bool isCountingDown = true;

    void Start() {
        if (GameManager.Instance.difficulty == 1) {
            minutes = 10f;
            seconds = 0f;
        }else if (GameManager.Instance.difficulty == 2) {
            minutes = 5f;
            seconds = 0f;
        }else if (GameManager.Instance.difficulty == 3) {
            minutes = 1;
            seconds = 0f;
        }
    }

    void Update()
    {
        if (isCountingDown && (seconds > 0 || minutes > 0))
        {
            // Reduce the countdownTime by the time elapsed since the last frame
            seconds -= Time.deltaTime;

            // When seconds go below 0, subtract 1 from minutes and reset seconds to 59
            if (seconds < -1 && minutes > 0)
            {
                seconds = 59;
                minutes -= 1;
            }

            // Update the UI text with the remaining time in MM:SS format
            countdownText.text = Mathf.Floor(minutes).ToString("00") + " : " + Mathf.Ceil(seconds).ToString("00"); // Display as two-digit integer
            
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

    // Event when countdown finishes
    private void OnCountdownEnd()
    {
        Debug.Log("Countdown finished!");
        // Add any logic for what should happen when the timer reaches 0
    }

    // Method to stop the countdown
    public void StopCountdown()
    {
        isCountingDown = false;
        Debug.Log("Countdown stopped!");
    }

    public void ContinueCountdown()
    {
        isCountingDown = true;
        Debug.Log("Countdown continue!");
    }
}

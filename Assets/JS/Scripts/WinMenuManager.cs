using System.Collections;
using UnityEngine;
using TMPro;

public class WinMenuManager : MonoBehaviour
{
    public Transform stars;
    private GameObject[] starList = new GameObject[3];
    private Timer timer;
    private TMP_Text timerText;
    private float defaultMinutes;
    private float defaultSeconds;
    private float usedMinutes;
    private float usedSeconds;
    private GameObject[] playerUIs;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.CompleteStage();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        timerText = GameObject.Find("Score").GetComponent<TMP_Text>();
        stars = GameObject.Find("GoldStar").transform;
        starList[0] = stars.Find("GoldStar1").gameObject;
        starList[1] = stars.Find("GoldStar2").gameObject;
        starList[2] = stars.Find("GoldStar3").gameObject;
        playerUIs = GameObject.FindGameObjectsWithTag("PlayerUIs");
        UpdateWinMenu(); // Call the method to update the time and stars
    }

    void UpdateWinMenu()
    {
        // Update and display the time
        changeTime();

        // Calculate and display the stars based on score
        calculateStar();

        foreach (GameObject playerUI in playerUIs)
        {
            // Check if the GameObject has a Canvas component
            Canvas canvasComponent = playerUI.GetComponent<Canvas>();
            if (canvasComponent != null)
            {
                // Set the Canvas component to disabled
                canvasComponent.enabled = false;
            }
        }
    }

    void changeTime()
    {
        // Set default times based on difficulty level
        if (GameManager.Instance.difficulty == 1)
        {
            defaultMinutes = 10;
            defaultSeconds = 0;
        }
        else if (GameManager.Instance.difficulty == 2)
        {
            defaultMinutes = 5;
            defaultSeconds = 0;
        }
        else
        {
            defaultMinutes = 3;
            defaultSeconds = 0;
        }

        // Calculate the time used based on the timer
        usedMinutes = defaultMinutes - timer.minutes - 1;
        usedSeconds = 60 - timer.seconds;

        // Ensure seconds do not go below 0 or above 60
        if (usedSeconds == 60)
        {
            usedSeconds = 0;
            usedMinutes += 1;
        }

        // Update the timer UI in the format "Time: 10min 30s"
        timerText.text = $"Time: {(int)usedMinutes}min {(int)usedSeconds}s";
    }

    void calculateStar()
    {
        // Get the current stage score from GameManager
        int stageScore = GameManager.Instance.GetLevelScore(GameManager.Instance.currentStage);

        // Show the stars one by one with an animation delay
        StartCoroutine(ShowStarsWithDelay(stageScore));
    }

    // Coroutine to show stars one by one with a delay
    IEnumerator ShowStarsWithDelay(int starCount)
    {
        // Hide all stars initially
        for (int i = 0; i < starList.Length; i++)
        {
            starList[i].SetActive(false);
        }

        // Enable the stars one by one with a 0.5s delay
        for (int i = 0; i < starCount; i++)
        {
            starList[i].SetActive(true); // Activate the star
            yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds before showing the next one
        }
    }

    public void RestartGame()
    {
        // Add your restart logic here
    }
}

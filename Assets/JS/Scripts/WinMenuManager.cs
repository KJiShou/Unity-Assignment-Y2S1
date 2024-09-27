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

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        timerText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
        stars = GameObject.Find("GoldStar").transform;
        starList[0] = stars.Find("GoldStar1").gameObject;
        starList[1] = stars.Find("GoldStar2").gameObject;
        starList[2] = stars.Find("GoldStar3").gameObject;

        UpdateWinMenu(); // Call the method to update the time and stars
    }

    void UpdateWinMenu()
    {
        // Update and display the time
        changeTime();

        // Calculate and display the stars based on score
        calculateStar();
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
        timerText.text = $"Time: {usedMinutes}min {usedSeconds}s";
    }

    void calculateStar()
    {
        // Get the current stage score from GameManager
        int stageScore = GameManager.Instance.GetLevelScore(GameManager.Instance.currentStage);

        // Determine stars based on the stage score
        ShowStars(stageScore);
    }

    void ShowStars(int starCount)
    {
        // Hide all stars initially
        for (int i = 0; i < starList.Length; i++)
        {
            starList[i].SetActive(false);
        }

        // Enable the number of stars based on the stage score
        for (int i = 0; i < starCount; i++)
        {
            starList[i].SetActive(true);
        }
    }
}

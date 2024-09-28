using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonManager : MonoBehaviour
{
    public GameObject[] levelButtons;  // Array of Level Buttons (each representing a level)

    private void Start()
    {
        SetUpLevelButtons();
    }

    private void SetUpLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Get the stars under the level button
            Transform stars = levelButtons[i].transform.Find("Stars");
            GameObject star1 = stars.Find("Star1").gameObject;
            GameObject star2 = stars.Find("Star2").gameObject;
            GameObject star3 = stars.Find("Star3").gameObject;

            // Check the player's score for the current level from GameManager.Instance
            int levelScore = GameManager.Instance.GetLevelScore(i + 1);  // Fetch score from GameManager

            // Show stars based on the score (1-3 stars)
            star1.SetActive(levelScore >= 1);  // Activate star1 if score is at least 1
            star2.SetActive(levelScore >= 2);  // Activate star2 if score is at least 2
            star3.SetActive(levelScore == 3);  // Activate star3 if score is 3

            // Disable/Enable Level Button based on the previous level's score
            if (i == 0)  // The first level should always be unlocked
            {
                EnableLevelButton(levelButtons[i]);
            }
            else
            {
                int previousLevelScore = GameManager.Instance.GetLevelScore(i);  // Get the previous level's score
                if (previousLevelScore > 0)  // Previous level completed
                {
                    if (GameManager.Instance.GetLevelScore(i+1) == 0) {
                        Transform grayStars = levelButtons[i].transform.Find("GrayStars");
                        GameObject grayStar1 = grayStars.Find("Star1").gameObject;
                        GameObject grayStar2 = grayStars.Find("Star2").gameObject;
                        GameObject grayStar3 = grayStars.Find("Star3").gameObject;
                        grayStar1.SetActive(false);
                        grayStar2.SetActive(false);
                        grayStar3.SetActive(false);
                    }
                    EnableLevelButton(levelButtons[i]);  // Unlock the current level
                }
                else
                {
                    DisableLevelButton(levelButtons[i]);  // Lock the current level
                }
            }
        }
    }

    private void EnableLevelButton(GameObject levelButton)
    {
        ButtonScaleEffect button = levelButton.GetComponent<ButtonScaleEffect>();
        button.enabled = true;
        // Reset color to normal (assumes original color is white or non-gray)
        Image buttonImage = levelButton.GetComponent<Image>();
        buttonImage.color = Color.white;
    }

    private void DisableLevelButton(GameObject levelButton)
    {
        Transform grayStars = levelButton.transform.Find("GrayStars");
        GameObject grayStar1 = grayStars.Find("Star1").gameObject;
        GameObject grayStar2 = grayStars.Find("Star2").gameObject;
        GameObject grayStar3 = grayStars.Find("Star3").gameObject;
        grayStar1.SetActive(false);
        grayStar2.SetActive(false);
        grayStar3.SetActive(false);
        ButtonScaleEffect button = levelButton.GetComponent<ButtonScaleEffect>();
        button.enabled = false;


        // Set button to gray to indicate it's locked
        Image buttonImage = levelButton.GetComponent<Image>();
        buttonImage.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        TMP_Text text = levelButton.GetComponentInChildren<TMP_Text>();
        text.color = new Color(0.2f, 0.2f, 0.2f, 1f);
    }
}

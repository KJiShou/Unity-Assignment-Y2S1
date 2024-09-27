using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSpaceShip : MonoBehaviour
{
    [SerializeField]
    Sprite[] imageList;  // List of spaceship skins
    public Image image;

    private int backupIndex;

    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
        // Backup the current spaceship index
        backupIndex = GameManager.Instance.spaceshipIndex;

        // Set the initial spaceship skin
        image.sprite = imageList[GameManager.Instance.spaceshipIndex];
        image.color = GameManager.Instance.spaceshipColor;  // Set the current color from RGBColor
    }

    public void leftImage()
    {
        audioManager.PlaySFX(audioManager.changeSkinSwipe);
        GameManager.Instance.spaceshipIndex = (GameManager.Instance.spaceshipIndex > 0)
            ? GameManager.Instance.spaceshipIndex - 1
            : imageList.Length - 1;

        image.sprite = imageList[GameManager.Instance.spaceshipIndex];
    }

    public void rightImage()
    {
        audioManager.PlaySFX(audioManager.changeSkinSwipe);
        GameManager.Instance.spaceshipIndex = (GameManager.Instance.spaceshipIndex < imageList.Length - 1)
            ? GameManager.Instance.spaceshipIndex + 1
            : 0;

        image.sprite = imageList[GameManager.Instance.spaceshipIndex];
    }

    public void cancel()
    {
        audioManager.PlaySFX(audioManager.menuClickOut);
        GameManager.Instance.spaceshipIndex = backupIndex;
        SceneManager.LoadScene("Main");
    }

    public void submit()
    {
        audioManager.PlaySFX(audioManager.menuClickIn);
        SceneManager.LoadScene("Main");
    }
}

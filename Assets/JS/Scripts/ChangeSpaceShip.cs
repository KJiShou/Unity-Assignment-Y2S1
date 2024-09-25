using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeSpaceShip : MonoBehaviour
{
    [SerializeField]
    Sprite[] imageList;
    public Image image;
    private int backup;
    private float backupR = 1;
    private float backupG = 1;
    private float backupB = 1;

    AudioManager audioManager;
    void Start() {

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    
        backup = GameManager.Instance.spaceshipIndex;
        backupR = GameManager.Instance.spaceshipColor.r;
        backupG = GameManager.Instance.spaceshipColor.g;
        backupB = GameManager.Instance.spaceshipColor.b;
        image.sprite = imageList[GameManager.Instance.spaceshipIndex];
        image.color = GameManager.Instance.spaceshipColor;
    }
    public void leftImage() {
        audioManager.PlaySFX(audioManager.changeSkinSwipe);

        if (GameManager.Instance.spaceshipIndex>0) {
            GameManager.Instance.spaceshipIndex--;
            image.sprite = imageList[GameManager.Instance.spaceshipIndex];
            image.color = GameManager.Instance.spaceshipColor;
        }else {
            GameManager.Instance.spaceshipIndex = imageList.Length-1;
            image.sprite = imageList[GameManager.Instance.spaceshipIndex];
            image.color = GameManager.Instance.spaceshipColor;
        }
    }

    public void rightImage() {
        audioManager.PlaySFX(audioManager.changeSkinSwipe);

        if (GameManager.Instance.spaceshipIndex<imageList.Length-1) {
            GameManager.Instance.spaceshipIndex++;
            image.sprite = imageList[GameManager.Instance.spaceshipIndex];
            image.color = GameManager.Instance.spaceshipColor;
        }else{
            GameManager.Instance.spaceshipIndex = 0;
            image.sprite = imageList[GameManager.Instance.spaceshipIndex];
            image.color = GameManager.Instance.spaceshipColor;
        }
    }

    public void cancel() {

        audioManager.PlaySFX(audioManager.menuClickOut);
        GameManager.Instance.spaceshipIndex = backup;
        GameManager.Instance.spaceshipColor = new Color(backupR, backupG, backupB);
        SceneManager.LoadScene("Main");
    }

    public void submit(){
        audioManager.PlaySFX(audioManager.menuClickIn);
        SceneManager.LoadScene("Main");
    }
}

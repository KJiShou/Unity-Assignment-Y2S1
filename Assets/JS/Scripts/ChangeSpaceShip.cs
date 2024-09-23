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
    void Start() {
        backup = GameManager.Instance.spaceshipIndex;
        backupR = GameManager.Instance.spaceshipColor.r;
        backupG = GameManager.Instance.spaceshipColor.g;
        backupB = GameManager.Instance.spaceshipColor.b;
        image.sprite = imageList[GameManager.Instance.spaceshipIndex];
        image.color = GameManager.Instance.spaceshipColor;
    }
    public void leftImage() {
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
        
        GameManager.Instance.spaceshipIndex = backup;
        RGBColor.r = backupR;
        RGBColor.g = backupG;
        RGBColor.b = backupB;
        GameManager.Instance.spaceshipColor = new Color(RGBColor.r, RGBColor.g, RGBColor.b);
        SceneManager.LoadScene("Main");
    }

    public void submit(){
        SceneManager.LoadScene("Main");
    }
}

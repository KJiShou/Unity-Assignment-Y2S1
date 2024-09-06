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
    private static int position = 0;
    private int backup;
    private float backupR;
    private float backupG;
    private float backupB;
    void Start() {
        backup = position;
        backupR = RGBColor.r;
        backupG = RGBColor.g;
        backupB = RGBColor.b;
        image.sprite = imageList[position];
    }
    public void leftImage() {
        if (position>0) {
            position--;
            image.sprite = imageList[position];
            image.color = new Color(RGBColor.r, RGBColor.g, RGBColor.b);
        }else {
            position = imageList.Length-1;
            image.sprite = imageList[position];
            image.color = new Color(RGBColor.r, RGBColor.g, RGBColor.b);
        }
    }

    public void rightImage() {
        if (position<imageList.Length-1) {
            position++;
            image.sprite = imageList[position];
            image.color = new Color(RGBColor.r, RGBColor.g, RGBColor.b);
        }else{
            position = 0;
            image.sprite = imageList[position];
            image.color = new Color(RGBColor.r, RGBColor.g, RGBColor.b);
        }
    }

    public void cancel() {
        
        position = backup;
        RGBColor.r = backupR;
        RGBColor.g = backupG;
        RGBColor.b = backupB;
        SceneManager.LoadScene("Main");
    }

    public void submit(){
        SceneManager.LoadScene("Main");
    }
}

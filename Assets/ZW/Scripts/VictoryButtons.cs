using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryButtons : MonoBehaviour
{
    public void returnHome()
    {
        SceneManager.LoadScene("StageSelect");
    }
    
    public void toMainScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextStage()
    {
        SceneManager.LoadScene("1-3");
    }
}

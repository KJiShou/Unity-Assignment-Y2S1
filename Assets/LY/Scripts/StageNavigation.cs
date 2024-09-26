using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;


public class StageNavigation : MonoBehaviour
{
    public static StageNavigation Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void LoadNextScene(string levelNumber)
    {
        Debug.Log(levelNumber);
        SceneManager.LoadScene(levelNumber);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;


public class StageNavigation : MonoBehaviour
{
    public static StageNavigation Instance { get; private set; }
    AudioManager audioManager;

    public void Awake()
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void LoadNextScene(string levelNumber)
    {
        Debug.Log("Enter Next Scene");
        SceneManager.LoadScene(levelNumber);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int spaceshipIndex = 0;
    public Color spaceshipColor = new Color(255, 255, 255);
    public int difficulty = 1;
    public int[] score = new int[7]{3,2,1,0,0,0,0};

    private GameObject parentObject;
    public float musicVolume=1;
    public float SFXVolume=1;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }

    }

    // calculate the game score
    public int calculateNumberOfEquationUsed() {
        int count = 0;
        parentObject = GameObject.Find("Equation UI Set");
        foreach(Transform child in parentObject.transform)
        {
            if (child.gameObject.name == "PortalContainer") {
                count++;
            }
        }
        return count;
    }

    public int GetLevelScore(int levelIndex)
    {
        if (levelIndex - 1 < score.Length)
        {
            return score[levelIndex - 1];  // Return the score for the specified level
        }
        return 0;  // Return 0 if no score is found
    }
}

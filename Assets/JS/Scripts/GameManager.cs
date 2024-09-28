using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int spaceshipIndex = 0;
    public Color spaceshipColor = new Color(255, 255, 255);
    public int difficulty = 1;
    public int[] score = new int[7]{0,0,0,0,0,0,0};

    private GameObject parentObject;
    public float musicVolume=1;
    public float SFXVolume=1;
    public int currentScore = 0; 
    public int currentStage = 1;  

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

    private void Start()
    {
        LoadPlayerData();
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

    public void AddScore(int value)
    {
        currentScore += value;
        Debug.Log("Current Score: " + currentScore);
    }

       public void CheckAndUpdateHighScore()
    {
        int stageIndex = currentStage - 1; // Convert stage number to array index (e.g., stage 1 => index 0)

        // Compare the current score to the high score of the current stage
        if (currentScore > score[stageIndex])
        {
            score[stageIndex] = currentScore; // Update the high score
            Debug.Log("New high score for Stage " + currentStage + ": " + currentScore);
        }
        else
        {
            Debug.Log("Current score did not exceed the high score for Stage " + currentStage);
        }
    }

    // Reset the current score (e.g., when starting a new stage)
    public void ResetScore()
    {
        currentScore = 0;
        Debug.Log("Score reset.");
    }

    // Method to handle stage completion (called when player reaches the stage endpoint)
    public void CompleteStage()
    {
        // Check and update the high score for the current stage
        CheckAndUpdateHighScore();

        // Reset the score for the next stage
        ResetScore();

        SavePlayerData();

        // Optionally load the next stage or victory screen here
        Debug.Log("Stage " + currentStage + " completed!");


        // Load next stage or handle game flow logic (e.g., SceneManager.LoadScene("NextStage"));
    }

    public void SetCurrentStageFromSceneName()
    {
        // Get the current scene name
        string sceneName = SceneManager.GetActiveScene().name;

        // Check if the scene name follows the expected format (e.g., "1-1", "1-2")
        if (sceneName.Contains("-"))
        {
            // Split the scene name by the hyphen
            string[] parts = sceneName.Split('-');

            // Ensure that we have two parts and the second part is an integer
            if (parts.Length == 2 && int.TryParse(parts[1], out int stage))
            {
                // Set currentStage to the second part of the scene name (e.g., "1-2" -> 2)
                currentStage = stage;
                Debug.Log($"Current stage set to: {currentStage}");
            }
            else
            {
                Debug.LogError("Invalid scene format. Expected format is like '1-1'.");
            }
        }
        else
        {
            Debug.LogError("Scene name does not contain a hyphen. Expected format is like '1-1'.");
        }
    }

    private string saveFilePath => Application.persistentDataPath + "/playerData.save";

    public void SavePlayerData()
    {
        // Create an instance of PlayerData
        PlayerData data = new PlayerData
        {
            score = score,
            spaceshipIndex = spaceshipIndex,
            musicVolume = musicVolume,
            SFXVolume = SFXVolume,
            currentStage = currentStage,
            difficulty = difficulty,
            currentScore = currentScore,
            spaceshipColor = new float[] { spaceshipColor.r, spaceshipColor.g, spaceshipColor.b, spaceshipColor.a }
        };

        // Create a file to save the data
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(saveFilePath, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }

        Debug.Log("Game data saved.");
    }

    public void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            // Load the saved data
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(saveFilePath, FileMode.Open))
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;

                // Assign the loaded data to the GameManager fields
                score = data.score;
                spaceshipIndex = data.spaceshipIndex;
                musicVolume = data.musicVolume;
                SFXVolume = data.SFXVolume;
                currentStage = data.currentStage;
                difficulty = data.difficulty;
                currentScore = data.currentScore;
                spaceshipColor = new Color(data.spaceshipColor[0], data.spaceshipColor[1], data.spaceshipColor[2], data.spaceshipColor[3]);
            }

            Debug.Log("Game data loaded.");
        }
        else
        {
            Debug.LogError("Save file not found.");
        }
    }

    public void DeleteSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }
    }
}

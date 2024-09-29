using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int[] score;
    public int spaceshipIndex;
    public float musicVolume;
    public float SFXVolume;
    public int currentStage;
    public int difficulty;
    public float[] spaceshipColor; // Store Color as an array of floats (RGBA)
    public int currentScore;
}
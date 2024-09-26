using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    
    void Start() {
        musicSlider.value = GameManager.Instance.musicVolume;
        SFXSlider.value = GameManager.Instance.SFXVolume;
    }

    public void SetMusicVolume() {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        GameManager.Instance.musicVolume = volume;
    }

    public void SetSFXVolume() {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        GameManager.Instance.SFXVolume = volume;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    // Add references to the music and SFX icons
    [SerializeField] private Image musicIcon;
    [SerializeField] private Image SFXIcon;

    // Add two colors for the active and disabled state of the icons
    private Color activeColor = Color.white;  // Default color (white)
    private Color disabledColor = new Color(1, 1, 1, 0.5f);  // Transparent or dimmed color

    void Start() {
        musicSlider.value = GameManager.Instance.musicVolume;
        SFXSlider.value = GameManager.Instance.SFXVolume;

        // Update icons at the start to match the current volume settings
        UpdateMusicIcon();
        UpdateSFXIcon();
    }

    public void SetMusicVolume() {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        GameManager.Instance.musicVolume = volume;

        // Update the music icon based on the volume value
        UpdateMusicIcon();
    }

    public void SetSFXVolume() {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        GameManager.Instance.SFXVolume = volume;

        // Update the SFX icon based on the volume value
        UpdateSFXIcon();
    }

    // Method to update the music icon based on volume
    private void UpdateMusicIcon() {
        if (musicSlider.value <= 0.0001f) {
            musicIcon.color = disabledColor;  // Dim the icon to show it's disabled
        } else {
            musicIcon.color = activeColor;  // Set the icon back to the active color
        }
    }

    // Method to update the SFX icon based on volume
    private void UpdateSFXIcon() {
        if (SFXSlider.value <= 0.0001f) {
            SFXIcon.color = disabledColor;  // Dim the icon to show it's disabled
        } else {
            SFXIcon.color = activeColor;  // Set the icon back to the active color
        }
    }
}

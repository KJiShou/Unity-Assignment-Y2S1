using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RGBColor : MonoBehaviour
{
    AudioManager audioManager;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Image spaceship;
    
    public float previousRedValue;
    public float previousGreenValue;
    public float previousBlueValue;

    private bool isPlayingSFX = false; // Track if the sound effect is currently playing

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        redSlider.value = GameManager.Instance.spaceshipColor.r;
        greenSlider.value = GameManager.Instance.spaceshipColor.g;
        blueSlider.value = GameManager.Instance.spaceshipColor.b;
        
        // Store the initial values of the sliders
        previousRedValue = redSlider.value;
        previousGreenValue = greenSlider.value;
        previousBlueValue = blueSlider.value;

        // Add listeners to detect when slider values change
        redSlider.onValueChanged.AddListener(delegate { ChangeColor(); });
        greenSlider.onValueChanged.AddListener(delegate { ChangeColor(); });
        blueSlider.onValueChanged.AddListener(delegate { ChangeColor(); });
    }

    public void ChangeColor()
    {
        // If not already playing, play the slider sound effect smoothly while dragging
        if (!isPlayingSFX)
        {
            StartCoroutine(PlaySliderSFXSmoothly());
        }

        // Get the slider values and create a new Color
        float r = redSlider.value;
        float g = greenSlider.value;
        float b = blueSlider.value;
        
        // Assign the new color to the Image component
        GameManager.Instance.spaceshipColor = new Color(r, g, b);
        spaceship.color = GameManager.Instance.spaceshipColor;
        GameManager.Instance.SavePlayerData();
    }

    private IEnumerator PlaySliderSFXSmoothly()
    {
        isPlayingSFX = true;
        audioManager.PlaySFX(audioManager.menuSlider);  // Play the slider sound effect

        // Wait a short period before allowing the sound to play again (smooth effect)
        yield return new WaitForSeconds(0.1f);  // Adjust this value for smoother sound effect timing

        isPlayingSFX = false;
    }
}

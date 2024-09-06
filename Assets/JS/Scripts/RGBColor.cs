using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBColor : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Image spaceship;
    public static float r =1;
    public static float g =1;
    public static float b =1;
    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to detect when slider values change
        redSlider.onValueChanged.AddListener(delegate { ChangeColor(); });
        greenSlider.onValueChanged.AddListener(delegate { ChangeColor(); });
        blueSlider.onValueChanged.AddListener(delegate { ChangeColor(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor()
    {
        // Get the slider values and create a new Color
        r = redSlider.value;
        g = greenSlider.value;
        b = blueSlider.value;
        // Assign the new color to the Image component
        spaceship.color = new Color(r, g, b);
    }
}

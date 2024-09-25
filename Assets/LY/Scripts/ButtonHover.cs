using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    AudioManager audioManager;
    private TextMeshProUGUI displayedText;
    private string initialText;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }



    public void Start() 
    {
        initialText = GetComponentInChildren<TextMeshProUGUI>().text;
        displayedText = GetComponentInChildren<TextMeshProUGUI>();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering!");
        audioManager.PlaySFX(audioManager.menuHover);
        displayedText.SetText(">  " + displayedText.text + "  <");
        Debug.Log(initialText);
        Debug.Log(displayedText.text);
    }





    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("No longer hovering!");
        displayedText.SetText(initialText);
    }



    // Update is called once per frame
    void Update()
    {
    
    }
}

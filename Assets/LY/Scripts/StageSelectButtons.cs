using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageSelectButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering!");
        audioManager.PlaySFX(audioManager.menuHover);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("No longer hovering!");
    }



    public void returnButton()
    {
        audioManager.PlaySFX(audioManager.menuClickOut);
        SceneManager.LoadScene("Main");
    }


    public void stageButton()
    {

    }

}

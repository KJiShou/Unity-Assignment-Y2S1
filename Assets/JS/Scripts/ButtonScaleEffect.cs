using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator animator;  // Reference to the Animator component
    private string levelNumber;
    AudioManager audioManager; 



    void Start()
    {
        // Get the Animator component on the button
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        levelNumber = GetComponentInChildren<TextMeshProUGUI>().text;


        if (animator == null)
        {
            Debug.LogError("Animator component is missing on this GameObject!");
        }
    }

    // This is called when the pointer enters the button's collider
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioManager.PlaySFX(audioManager.menuHover);

        if (animator != null)
        {
            animator.SetTrigger("Hover");  // Play the hover animation
        }
    }

    // This is called when the pointer exits the button's collider
    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Idle");  // Play the idle or normal animation
        }
    }

    // This is called when the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (animator != null)
        {
            Debug.Log(levelNumber);
            audioManager.musicSource.Stop();
            audioManager.PlaySFX(audioManager.stageSelectConfirm);
            animator.SetTrigger("Click");  // Play the click animation


            StageNavigation.Instance.LoadNextScene(levelNumber);
            AudioManager.Instance.PlayStageTheme();
        }
    }
}

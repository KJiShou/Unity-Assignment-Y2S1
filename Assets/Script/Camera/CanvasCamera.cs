using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CanvasCamera : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animations;
    AudioManager audioManager;
    private string stageNumber;
    // Start is called before the first frame update
    void Start()
    {
        animations = gameObject.GetComponent<Animator>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering!");
        animations.SetTrigger("Expand");
        audioManager.PlaySFX(audioManager.menuHover);
    }

    public void OnClick()
    {
        audioManager.PlaySFX(audioManager.stageSelectConfirm);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("No longer hovering!");
        animations.SetTrigger("Shrink");
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
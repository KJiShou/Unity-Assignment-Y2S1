using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
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


    /*public void OnClick()
    {
        audioManager.PlaySFX(audioManager.stageSelectConfirm);
    }
    */



    // Update is called once per frame
    void Update()
    {
        
    }
}

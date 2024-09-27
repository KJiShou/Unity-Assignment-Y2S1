using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBgm : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayStageTheme();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

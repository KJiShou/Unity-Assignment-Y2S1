using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryButtons : MonoBehaviour
{
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Loading");
    }

    public void returnHome()
    {
        StartCoroutine(Wait());
    }

    public void replayStage()
    {
        StartCoroutine(Wait());
    }

    public void nextStage()
    {
        StartCoroutine(Wait());
    }


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

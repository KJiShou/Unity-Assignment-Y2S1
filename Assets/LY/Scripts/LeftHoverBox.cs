using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftHoverbox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform camera;


    // Start is called before the first frame update
    void Start()
    {

    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering!");
        camera.transform.position = new Vector3(-3, 0, 0);
    }





    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("No longer hovering!");
    }



    // Update is called once per frame
    void Update()
    {

    }
}

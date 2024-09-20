using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightHoverbox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform camera;
    public float scrollSpeed = 2.0f;
    private bool isHovered;

    // Start is called before the first frame update
    void Start()
    {

    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering!");
        isHovered = true;
    }





    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("No longer hovering!");
        isHovered = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (isHovered)
        {
            camera.transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightHoverbox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform camera;
    private float scrollSpeed = 300;
    private bool isHovered;
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = camera.GetComponent<Rigidbody2D>();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering!");
        isHovered = true;
        rb.velocity = new Vector2(0.5f, 0);
    }





    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("No longer hovering!");
        isHovered = false;
        rb.velocity = new Vector2(0, 0);
    }



    // Update is called once per frame
    void Update()
    {
        if (isHovered)
        {
            rb.AddForce(Vector3.right * scrollSpeed * Time.deltaTime);
        }
    }
}

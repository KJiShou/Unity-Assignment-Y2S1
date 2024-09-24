using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoomIn : MonoBehaviour
{
    private bool isMenuOpen; 
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 5f;
    private float maxZoom = 20f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;
    [SerializeField] private Camera cam;

    // drag camera
    private Vector3 dragOrigin;
    private MainEquationButton menuButton; // Cache the button component

    void Start()
    {
        zoom = cam.orthographicSize;
        menuButton = GameObject.Find("Left Arrow").GetComponent<MainEquationButton>(); // Cache the component
    }

    // Update is called once per frame
    void Update()
    {
        isMenuOpen = menuButton.isMenuOpen; // Use the cached reference
        if (!isMenuOpen)
        {
            PanCamera();
            ZoomCamera();
        }
    }

    private void PanCamera() 
    {
        // Check for both left and right mouse button presses
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition); // Set the initial drag origin
        }

        // While either mouse button is held down
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition); // Calculate the difference between the drag origin and the current mouse position
            cam.transform.position += difference; // Move the camera based on the drag difference
        }
    }

    private void ZoomCamera() 
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }
}

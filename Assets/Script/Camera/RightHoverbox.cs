using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightHoverbox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform cameraTransform;  // Reference to the camera's transform
    public float scrollSpeed = 3f;  // Adjust the speed as needed
    public SpriteRenderer mapRenderer;  // Reference to the map sprite for boundaries

    private bool isHovered;
    private Camera cam;  // Reference to the Camera component
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Start()
    {
        cam = Camera.main;  // Get the main camera

        // Calculate map boundaries based on the mapRenderer's bounds
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
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

    private void Update()
    {
        if (isHovered)
        {
            // Move the camera to the right when hovering
            Vector3 newPosition = cameraTransform.position + Vector3.right * scrollSpeed * Time.deltaTime;

            // Clamp the camera's position within the map boundaries
            cameraTransform.position = ClampCamera(newPosition);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}

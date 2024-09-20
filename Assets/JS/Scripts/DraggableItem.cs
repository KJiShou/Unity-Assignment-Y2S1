using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public GameObject prefabToInstantiate;  // Prefab to instantiate on drop
    public GameObject menuObject;  // The object to destroy when dragging ends successfully
    private Animator animator;  // Animator to control the close animation
    [HideInInspector] public Transform parentAfterDrag;

    private Canvas dragCanvas;

    // Reference to AddEquationButton, which will be found dynamically
    private AddEquationButton addEquationButton;

    void Start()
    {
        // Find the DragCanvas in the scene
        dragCanvas = GameObject.Find("DragCanvas").GetComponent<Canvas>();

        if (dragCanvas == null)
        {
            Debug.LogError("DragCanvas not found in the scene. Make sure you have a canvas named 'DragCanvas'.");
        }

        // Get the animator on the menuObject (optional if there's no animation)
        if (menuObject != null)
        {
            animator = menuObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("MenuObject is not assigned.");
        }

        // Dynamically find the AddEquationButton script in the scene
        addEquationButton = FindObjectOfType<AddEquationButton>();
        if (addEquationButton == null)
        {
            Debug.LogError("AddEquationButton script not found in the scene.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");

        // Close the menuObject by triggering the "Close" animation (optional)
        if (menuObject != null && animator != null)
        {
            animator.SetTrigger("Close");
        }

        parentAfterDrag = transform.parent;

        // Set the parent to the DragCanvas
        transform.SetParent(dragCanvas.transform, false);

        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");

        // Move the item with the mouse
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragCanvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out position);
        transform.position = dragCanvas.transform.TransformPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");

        // Check where the item was dropped
        GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;

        if (dropTarget != null && dropTarget.GetComponent<EquationSlot>() != null)
        {
            Debug.Log("Dropped in a valid slot: " + dropTarget.name);

            // Instantiate a prefab at the drop location
            if (prefabToInstantiate != null)
            {
                // Instantiate the prefab at the current position
                GameObject instantiatedObject = Instantiate(prefabToInstantiate, dropTarget.transform.position, Quaternion.identity);

                // Optionally, parent the instantiated object to the slot or other objects
                instantiatedObject.transform.SetParent(dropTarget.transform, false);

                Debug.Log("Prefab instantiated at drop location: " + instantiatedObject.name);

                // Destroy the dragged image after the prefab is instantiated
                Destroy(gameObject);  // Deletes the dragged item itself
            }

            // Destroy the menuObject after successful drop
            if (menuObject != null)
            {
                Destroy(menuObject);
                Debug.Log("MenuObject destroyed.");

                // Update the isMenuOpen flag in the AddEquationButton
                if (addEquationButton != null)
                {
                    addEquationButton.isMenuOpen = false;  // Update the flag to reflect the menu is closed
                    Debug.Log("AddEquationButton flag updated: isMenuOpen = false");
                }
            }
        }
        else
        {
            // Destroy the dragged item if not dropped in a valid slot
            Debug.Log("Dropped in an invalid area, destroying the dragged item.");
            Destroy(gameObject);  // Deletes the dragged item itself
            if (menuObject != null)
            {
                Destroy(menuObject);
                Debug.Log("MenuObject destroyed.");

                // Update the isMenuOpen flag in the AddEquationButton
                if (addEquationButton != null)
                {
                    addEquationButton.isMenuOpen = false;  // Update the flag to reflect the menu is closed
                    Debug.Log("AddEquationButton flag updated: isMenuOpen = false");
                }
            }
        }

        image.raycastTarget = true;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FontManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TMP_Text currentText;
    public TMP_FontAsset hoverFontAsset;
    private TMP_FontAsset defaultFontAsset;
    public GameObject targetObject; // The object with the SnapToItem script
    private Animator animator;

    private bool isSliderOpen = false;
    private bool isTextSelected = false;

    private Color defaultColor;
    public Color selectedColor = Color.yellow;
    SnapToItem snapToItem;

    void Start()
    {
        currentText = GetComponent<TMP_Text>();
        defaultFontAsset = currentText.font;
        defaultColor = currentText.color;

        if (targetObject == null)
        {
            Debug.LogError("Target object is missing!");
        }
        else
        {
            animator = targetObject.GetComponent<Animator>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isTextSelected)
        {
            currentText.font = hoverFontAsset;
            currentText.ForceMeshUpdate();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isTextSelected)
        {
            currentText.font = defaultFontAsset;
            currentText.ForceMeshUpdate();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSliderOpen)
        {
            targetObject.GetComponent<SnapToItem>().NameLabel = currentText;
            // Get the SnapToItem component from the target object
            SnapToItem snapToItem = targetObject.GetComponent<SnapToItem>();
            if (snapToItem != null)
            {
                // Get the item index based on the clicked text
                int itemIndex = GetItemIndexFromText(currentText.text);

                // Call the UpdateSlider method
                snapToItem.UpdateSlider(itemIndex);
            }
            else
            {
                Debug.LogError("SnapToItem component missing on targetObject!");
            }
            OpenSlider();
        }
    }

    void Update()
    {
        if (isSliderOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!IsPointerOverUIElement())
                {
                    CloseSlider();
                }
            }
        }
    }

    private void OpenSlider()
    {
        isSliderOpen = true;
        isTextSelected = true;

        // Change text color to selected color
        currentText.color = selectedColor;

        // Open the slider (assuming you have an animation)
        if (animator != null)
        {
            animator.SetTrigger("Open");
            animator.ResetTrigger("Close");
        }
    }

    public void CloseSlider()
    {
        isSliderOpen = false;
        isTextSelected = false;

        // Revert text color
        currentText.color = defaultColor;

        // Revert font if the pointer is not over the text
        if (!IsPointerOverText())
        {
            currentText.font = defaultFontAsset;
            currentText.ForceMeshUpdate();
        }

        // Close the slider (assuming you have an animation)
        if (animator != null)
        {
            animator.SetTrigger("Close");
            animator.ResetTrigger("Open");
        }
    }

    private bool IsPointerOverUIElement()
    {
        // Check if the pointer is over any UI element, including the text or slider
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject || result.gameObject == targetObject ||
                result.gameObject.transform.IsChildOf(targetObject.transform))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsPointerOverText()
    {
        // Check if the pointer is over the text
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }
    private int GetItemIndexFromText(string text)
    {
        // Assuming your text represents the item number
        int itemNumber;
        if (int.TryParse(text, out itemNumber))
        {
            // Adjust for offset if necessary
            int itemIndex = itemNumber + 70; // Add 70 if your items start from index 70
            return itemIndex;
        }
        else
        {
            Debug.LogError("Invalid item text: " + text);
            return 70; // Default to index 70 if parsing fails
        }
    }

}


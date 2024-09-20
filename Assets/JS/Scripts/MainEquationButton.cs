using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainEquationButton : MonoBehaviour
{
    public Button button; // Assign your button here
    public GameObject targetObject; // The UI object to enable, animate, and disable
    private Animator animator; // Animator to handle animations

    public bool isMenuOpen = false; // A flag to track if the menu is open or closed

    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (targetObject != null)
        {
            animator = targetObject.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component is missing on the target object.");
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
        }
    }

    // This method is called when the button is clicked
    void OnButtonClick()
    {
        if (isMenuOpen)
        {
            // If the menu is currently open, play the 'Close' animation and close it
            animator.SetTrigger("Close");
            animator.ResetTrigger("Open");
        }
        else
        {
            // If the menu is currently closed, open it and play the 'Open' animation
            targetObject.SetActive(true); // Enable the target object
            animator.SetTrigger("Open");
            animator.ResetTrigger("Close");

        }

        // Toggle the isMenuOpen flag
        isMenuOpen = !isMenuOpen;
    }
}

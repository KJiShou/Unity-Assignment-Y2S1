using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button deleteButton;
    private Animator animator;
    private LineRendererLinear lineRendererLinear;
    void Start()
    {
        // Get the Button component attached to this GameObject
        deleteButton = GetComponent<Button>();
        animator = GetComponentInParent<Animator>();
        lineRendererLinear = FindObjectOfType<LineRendererLinear>();

        if (animator == null) {
            Debug.LogError("No animator");
        }
        if (deleteButton != null)
        {
            // Add a listener to the button click event
            deleteButton.onClick.AddListener(OnDeleteButtonClicked);
        }
        else
        {
            Debug.LogError("DeleteButtonHandler: No Button component found on this GameObject.");
        }
    }

    void OnDeleteButtonClicked()
    {
        if (animator != null)
        {
            animator.SetTrigger("Close");
        }
        

        // Do not start the coroutine or destroy the GameObject here
    }

    // This method will be called by the animation event
    public void OnAnimationComplete()
    {
        Destroy(transform.gameObject);
        if (lineRendererLinear != null)
        {
            lineRendererLinear.DestroyEquationAndPortals();
        }
    }
}

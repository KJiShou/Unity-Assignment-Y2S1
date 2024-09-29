using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button deleteButton;
    private Animator animator;
    private LineRendererLinear lineRendererLinear;
    private QuadraticLineRenderer quadraticLineRenderer;
    private TrigonometricLineRenderer trigonometricLineRenderer;
    void Start()
    {
        // Get the Button component attached to this GameObject
        deleteButton = GetComponent<Button>();
        animator = GetComponentInParent<Animator>();
        lineRendererLinear = transform.parent.GetComponentInChildren<LineRendererLinear>();
        if (lineRendererLinear == null) {
            quadraticLineRenderer = transform.parent.GetComponentInChildren<QuadraticLineRenderer>();
        }
        if (quadraticLineRenderer == null && lineRendererLinear == null) {
            trigonometricLineRenderer = transform.parent.GetComponentInChildren<TrigonometricLineRenderer>();
        }

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
            Debug.Log("DeleteButtonHandler: No Button component found on this GameObject.");
        }
    }

    void OnDeleteButtonClicked()
    {
        if (animator != null)
        {
            animator.SetTrigger("Close");
        }
        if (lineRendererLinear != null) {
            lineRendererLinear.DestroyEquationAndPortals();
        }
        if (quadraticLineRenderer != null) {
            quadraticLineRenderer.DestroyEquationAndPortals();
        }
        if (trigonometricLineRenderer != null) {
            trigonometricLineRenderer.DestroyEquationAndPortals();
        }
        // Do not start the coroutine or destroy the GameObject here
    }

    // This method will be called by the animation event
    public void OnAnimationComplete()
    {
        Destroy(transform.gameObject);
    }
}

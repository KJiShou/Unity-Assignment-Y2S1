using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator animator;  // Reference to the Animator component
    
    void Start()
    {
        // Get the Animator component on the button
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on this GameObject!");
        }
    }

    // This is called when the pointer enters the button's collider
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hover");  // Play the hover animation
        }
    }

    // This is called when the pointer exits the button's collider
    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Idle");  // Play the idle or normal animation
        }
    }

    // This is called when the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Click");  // Play the click animation
        }
    }
}

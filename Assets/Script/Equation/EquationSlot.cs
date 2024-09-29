using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquationSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop called");

        if (eventData.pointerDrag != null)
        {
            DraggableItem draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            if (draggableItem != null)
            {
                // Set the parentAfterDrag to this slot and place the dropped item
                draggableItem.parentAfterDrag = transform;
                Debug.Log("Dropped item into slot: " + gameObject.name);
            }
        }
    }
}

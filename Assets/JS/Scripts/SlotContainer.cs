using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SlotContainer : MonoBehaviour
{
    public GameObject slotPrefab;  // Prefab for the EquationSlot
    public GridLayoutGroup gridLayoutGroup;  // Reference to the GridLayoutGroup component

    private List<GameObject> slots = new List<GameObject>();  // Store all the slots
    private int maxSlots = 1;  // Initial max number of slots

    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        // Create initial slots
        for (int i = 0; i < maxSlots; i++)
        {
            CreateNewSlot();
        }
    }

    // Create a new slot and add it to the container
    void CreateNewSlot()
    {
        GameObject newSlot = Instantiate(slotPrefab, transform);
        slots.Add(newSlot);
    }

    // Check if all slots are filled with images
    bool AreAllSlotsFilled()
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount == 0) // If any slot has no image
            {
                return false;
            }
        }
        return true;
    }

    // When a new image is dropped into a slot, manage it
    public void OnImageDropped(GameObject droppedImage, GameObject targetSlot)
    {
        if (targetSlot.transform.childCount > 0)
        {
            // If the target slot already contains an image, move the current image to the next available slot
            GameObject currentImage = targetSlot.transform.GetChild(0).gameObject;
            MoveImageToNextAvailableSlot(currentImage);
        }

        // Place the dropped image in the target slot
        droppedImage.transform.SetParent(targetSlot.transform, false);

        // If all slots are filled, create a new slot
        if (AreAllSlotsFilled())
        {
            CreateNewSlot();
        }
    }

    // Move the image to the next available slot
    void MoveImageToNextAvailableSlot(GameObject image)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                // Found an empty slot, move the image here
                image.transform.SetParent(slot.transform, false);
                return;
            }
        }

        // If all slots are filled, create a new slot and move the image there
        CreateNewSlot();
        image.transform.SetParent(slots[slots.Count - 1].transform, false);
    }

    public void ResetSlots()
    {
        // Clear all existing slots and their children
        foreach (var slot in slots)
        {
            // Destroy all children of the slot (including any portal containers or other elements)
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);  // Destroy each child (including portal containers)
            }

            // Destroy the slot itself
            Destroy(slot);
        }

        slots.Clear();  // Clear the list of slots

        // Create one new slot
        CreateNewSlot();
        Debug.Log("All slots and their children cleared, one new slot created.");
    }
}

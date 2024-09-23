using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PortalCleaner : MonoBehaviour
{
    public void RunDelete()
    {
        // Find the parent object named "Equation UI"
        GameObject equationUI = GameObject.Find("Equation UI");

        if (equationUI != null)
        {
            // Search for all child objects named "PortalContainer" and delete them
            DeletePortalContainers(equationUI.transform);
        }
        else
        {
            Debug.LogError("Equation UI object not found!");
        }
    }

    // Recursive method to delete all child objects named "PortalContainer"
    public void DeletePortalContainers(Transform parentTransform)
    {
        // Iterate through all the children of the parentTransform
        foreach (Transform child in parentTransform)
        {
            // If the child is named "PortalContainer", destroy it
            if (child.name == "PortalContainer")
            {
                Destroy(child.gameObject);
                Debug.Log("Deleted PortalContainer: " + child.name);

            }
            else
            {
                // If the child is not "PortalContainer", recursively check its children
                DeletePortalContainers(child);
            }
        }
    }
}

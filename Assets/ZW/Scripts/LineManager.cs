using UnityEngine;

public class LineManager : MonoBehaviour
{
    public GameObject linePrefab;  // Prefab for the line (should have the LineRendererLinear attached)
    public SpaceshipFollowLine spaceship;  // Reference to the SpaceshipFollowLine script

    private LineRendererLinear currentLineRendererLinear;

    // Method to create a new line
    public void CreateNewLine()
    {
        GameObject newLine = Instantiate(linePrefab);  // Create a new line
        currentLineRendererLinear = newLine.GetComponent<LineRendererLinear>();  // Get the LineRendererLinear script

        if (currentLineRendererLinear != null)
        {
            Debug.Log("New line created and set for the spaceship.");
            spaceship.SetLineRenderer(currentLineRendererLinear);  // Pass the line to the spaceship
        }
        else
        {
            Debug.LogError("No LineRendererLinear component found on the line prefab.");
        }
    }

    // This method would be called when the portal animation finishes
    public void OnPortalAnimationFinish()
    {
        if (spaceship != null)
        {
            spaceship.OnPortalAnimationFinished();  // Start moving the spaceship
        }
    }
}

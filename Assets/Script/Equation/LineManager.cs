using UnityEngine;

public class LineManager : MonoBehaviour
{
    public GameObject linePrefab;  // Prefab for the line (should have the LineRendererLinear attached)
    public SpaceshipFollowLine spaceship;  // Reference to the SpaceshipFollowLine script

    private LineRendererLinear currentLineRendererLinear;
}

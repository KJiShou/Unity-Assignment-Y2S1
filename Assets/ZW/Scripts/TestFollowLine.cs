using TMPro;
using TS.DoubleSlider;
using UnityEngine;

public class TestFollowLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numPoints = 50;
    public float xStart = -10f;
    public float xEnd = 10f;
    public float m = 1f;  // Slope of the line
    public float c = 0f;  // Y-intercept
    public float yMin = -10f; // Minimum y value
    public float yMax = 10f;  // Maximum y value
    public TMP_Text slopeText;  // Reference to the TMP_Text for the slope (m)
    public TMP_Text interceptText;  // Reference to the TMP_Text for the intercept (c)
    [SerializeField] private DoubleSlider _slider;

    // Portal prefabs
    public GameObject startPortalPrefab; // Assign in Inspector
    public GameObject endPortalPrefab;   // Assign in Inspector

    // Spaceship prefab
    public GameObject spaceshipPrefab; // Assign in Inspector

    // Speed of the spaceship
    public float spaceshipSpeed = 2f;

    // Portal instances
    private GameObject startPortalInstance;
    private GameObject endPortalInstance;
    private GameObject spaceshipInstance;  // Instance of the spaceship
    private GameObject portalContainer; // We'll create a new container for each line's portals

    // Line points
    private Vector3[] positions;

    // Movement control
    private int currentPointIndex = 0; // Start at the first point

    void Awake()
    {
        _slider.OnValueChanged.AddListener(SliderDouble_ValueChanged);
    }

    void Start()
    {
        lineRenderer.positionCount = numPoints;

        // Create a new container for the portals for this line
        portalContainer = new GameObject("PortalContainer");
        portalContainer.transform.SetParent(this.transform);

        GameObject targetParent = GameObject.Find("Equation UI");
        if (targetParent != null)
        {
            // Set the portalContainer to be a child of the specific Canvas or parent
            portalContainer.transform.SetParent(targetParent.transform, false);
        }
        else
        {
            Debug.LogError("MainCanvas not found. Please make sure you have a Canvas named 'MainCanvas'.");
        }

        // Instantiate portals under the PortalContainer
        if (startPortalPrefab != null)
        {
            startPortalInstance = Instantiate(startPortalPrefab, portalContainer.transform);
        }
        if (endPortalPrefab != null)
        {
            endPortalInstance = Instantiate(endPortalPrefab, portalContainer.transform);
        }

        // Instantiate the spaceship at the start of the line
        if (spaceshipPrefab != null)
        {
            spaceshipInstance = Instantiate(spaceshipPrefab, Vector3.zero, Quaternion.identity);
        }

        // Initial draw
        UpdateEquationValues();
    }

    void Update()
    {
        UpdateEquationValues();

        // Move the spaceship along the line
        MoveSpaceshipAlongLine();
    }

    public void DrawLinearEquation()
    {
        positions = new Vector3[numPoints];
        float xStep = (xEnd - xStart) / (numPoints - 1);

        for (int i = 0; i < numPoints; i++)
        {
            float x = xStart + i * xStep;
            float y = m * x + c;  // y = mx + c (linear equation)
            y = Mathf.Clamp(y, yMin, yMax); // Clamp the y value within min and max limits
            positions[i] = new Vector3(x, y, 0);
        }

        lineRenderer.SetPositions(positions);

        // Update portal positions
        if (startPortalInstance != null)
        {
            Vector3 startWorldPosition = positions[0];
            if (!lineRenderer.useWorldSpace)
            {
                startWorldPosition = transform.TransformPoint(positions[0]);
            }
            startPortalInstance.transform.position = startWorldPosition;

            // Move spaceship to the start position of the line
            if (spaceshipInstance != null)
            {
                spaceshipInstance.transform.position = startWorldPosition;
            }
        }
        if (endPortalInstance != null)
        {
            Vector3 endWorldPosition = positions[numPoints - 1];
            if (!lineRenderer.useWorldSpace)
            {
                endWorldPosition = transform.TransformPoint(positions[numPoints - 1]);
            }
            endPortalInstance.transform.position = endWorldPosition;
        }
    }

    public void UpdateEquationValues()
    {
        // Try to parse the TMP_Text fields as floats
        if (float.TryParse(slopeText.text, out float parsedM))
        {
            m = parsedM;
        }
        else
        {
            Debug.LogError("Invalid input for slope (m). Make sure it's a valid number.");
        }

        if (float.TryParse(interceptText.text, out float parsedC))
        {
            c = parsedC;
        }
        else
        {
            Debug.LogError("Invalid input for intercept (c). Make sure it's a valid number.");
        }
        if(lineRenderer == null) {
            Destroy(transform.gameObject);
        }

        // Redraw the line after updating values
        DrawLinearEquation();
    }

    private void SliderDouble_ValueChanged(float min, float max)
    {
        xStart = min;
        xEnd = max;
    }

    public void DestroyEquationAndPortals()
    {
        // Destroy portals
        if (startPortalInstance != null)
        {
            Destroy(startPortalInstance);
            startPortalInstance = null;
        }
        if (endPortalInstance != null)
        {
            Destroy(endPortalInstance);
            endPortalInstance = null;
        }
        if (lineRenderer != null)
        {
            Destroy(lineRenderer); // Completely remove the LineRenderer component
            lineRenderer = null;
        }
    }

    private void MoveSpaceshipAlongLine()
    {
        if (spaceshipInstance == null || positions == null || positions.Length == 0)
        {
            return;
        }

        // Move towards the next point on the line
        Vector3 targetPosition = positions[currentPointIndex];
        spaceshipInstance.transform.position = Vector3.MoveTowards(
            spaceshipInstance.transform.position,
            targetPosition,
            spaceshipSpeed * Time.deltaTime
        );

        // Check if the spaceship reached the current target point
        if (Vector3.Distance(spaceshipInstance.transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex++;

            // If we've reached the end of the line, reset or stop
            if (currentPointIndex >= positions.Length)
            {
                currentPointIndex = positions.Length - 1; // Keep it at the last point (you can change this to loop)
            }
        }
    }
}

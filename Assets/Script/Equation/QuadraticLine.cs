using TMPro;
using TS.DoubleSlider;
using UnityEngine;

public class QuadraticLineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numPoints = 50;
    public float xStart = -10f;
    public float xEnd = 10f;
    public float a = 1f;  // Coefficient for x^2
    public float b = 1f;  // Coefficient for x
    public float c = 0f;  // Constant term
    public float yMin = -10f; // Minimum y value
    public float yMax = 10f;  // Maximum y value
    public TMP_Text aText;  // Reference to the TMP_Text for coefficient a
    public TMP_Text bText;  // Reference to the TMP_Text for coefficient b
    public TMP_Text cText;  // Reference to the TMP_Text for constant c
    [SerializeField] private DoubleSlider _slider;

    // Portal prefabs
    public GameObject startPortalPrefab; // Assign in Inspector
    public GameObject endPortalPrefab;   // Assign in Inspector

    // Portal instances
    private GameObject startPortalInstance;
    private GameObject endPortalInstance;
    private GameObject portalContainer; // We'll create a new container for each line's portals
    public Vector3[] positions;
    public int lineID;

    void Awake()
    {
        _slider.OnValueChanged.AddListener(SliderDouble_ValueChanged);
    }

    void Start()
    {
        lineID = GetInstanceID();
        lineRenderer.positionCount = numPoints;

        // Create a new container for the portals for this line
        portalContainer = new GameObject("PortalContainer");
        portalContainer.transform.SetParent(this.transform);
        GameObject targetParent = GameObject.Find("Equation UI Set");
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
            startPortalInstance.GetComponent<PortalController>().lineID = lineID;
        }
        if (endPortalPrefab != null)
        {
            endPortalInstance = Instantiate(endPortalPrefab, portalContainer.transform);
            endPortalInstance.GetComponent<PortalController>().lineID = lineID;
        }

        // Initial draw
        UpdateEquationValues();
    }

    void Update()
    {
        UpdateEquationValues();
    }

    public void DrawQuadraticEquation()
    {
        AdjustPointsBasedOnLineLength();
        positions = new Vector3[numPoints];
        float xStep = (xEnd - xStart) / (numPoints - 1);

        for (int i = 0; i < numPoints; i++)
        {
            float x = xStart + i * xStep;
            float y = a * x * x + b * x + c;  // y = ax^2 + bx + c (quadratic equation)
            y = Mathf.Clamp(y, yMin, yMax); // Clamp the y value within min and max limits
            positions[i] = new Vector3(x, y, 0);
        }

        lineRenderer.SetPositions(positions);

        // Update portal positions
        if (startPortalInstance != null)
        {
            // If LineRenderer uses local positions, convert to world positions
            Vector3 startWorldPosition = positions[0];
            if (!lineRenderer.useWorldSpace)
            {
                startWorldPosition = transform.TransformPoint(positions[0]);
            }
            startPortalInstance.transform.position = startWorldPosition;
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
        if (float.TryParse(aText.text, out float parsedA))
        {
            a = parsedA;
        }
        else
        {
            Debug.LogError("Invalid input for coefficient a. Make sure it's a valid number.");
        }

        if (float.TryParse(bText.text, out float parsedB))
        {
            b = parsedB;
        }
        else
        {
            Debug.LogError("Invalid input for coefficient b. Make sure it's a valid number.");
        }

        if (float.TryParse(cText.text, out float parsedC))
        {
            c = parsedC;
        }
        else
        {
            Debug.LogError("Invalid input for constant c. Make sure it's a valid number.");
        }

        if(lineRenderer == null) 
        {
            Destroy(transform.gameObject);
        }

        // Redraw the quadratic line after updating values
        DrawQuadraticEquation();
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

    private void AdjustPointsBasedOnLineLength()
    {
        float distance = Mathf.Abs(xEnd - xStart);

        // Define thresholds for changing the number of points
        if (distance < 10f)
        {
            numPoints = 50;
        }
        else if (distance > 30f)
        {
            numPoints = 500;
        }
        else
        {
            numPoints = 200;  // Default number of points
        }

        // Update the position count of the line renderer
        lineRenderer.positionCount = numPoints;
    }
}

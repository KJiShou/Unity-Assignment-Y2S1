using TMPro;
using TS.DoubleSlider;
using UnityEngine;

public class TrigonometricLineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numPoints = 100; // Increased for smoothness of the curves
    public float xStart = -10f;
    public float xEnd = 10f;
    public float amplitude = 1f;  // Coefficient for the amplitude (A)
    public float frequency = 1f;  // Coefficient for the frequency (B)
    public float phaseShift = 0f; // Coefficient for the phase shift (C)
    public float yMin = -10f; // Minimum y value
    public float yMax = 10f;  // Maximum y value
    public TMP_Text amplitudeText;  // Reference to the TMP_Text for amplitude (A)
    public TMP_Text frequencyText;  // Reference to the TMP_Text for frequency (B)
    public TMP_Text phaseShiftText; // Reference to the TMP_Text for phase shift (C)
    [SerializeField] private DoubleSlider _slider;

    public enum TrigFunction { Sine, Cosine, Tangent }
    public TrigFunction trigFunction = TrigFunction.Sine;  // Enum to choose between sin, cos, tan

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
        positions = new Vector3[numPoints];
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

    public void DrawTrigEquation()
    {
        AdjustPointsBasedOnLineLength();
        positions = new Vector3[numPoints];
        float xStep = (xEnd - xStart) / (numPoints - 1);

        for (int i = 0; i < numPoints; i++)
        {
            float x = xStart + i * xStep;
            float y = 0f;

            // Determine which trigonometric function to use
            switch (trigFunction)
            {
                case TrigFunction.Sine:
                    y = amplitude * Mathf.Sin(frequency * x + phaseShift); // y = A * sin(Bx + C)
                    break;
                case TrigFunction.Cosine:
                    y = amplitude * Mathf.Cos(frequency * x + phaseShift); // y = A * cos(Bx + C)
                    break;
                case TrigFunction.Tangent:
                    y = amplitude * Mathf.Tan(frequency * x + phaseShift); // y = A * tan(Bx + C)
                    break;
            }

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
        if (float.TryParse(amplitudeText.text, out float parsedA))
        {
            amplitude = parsedA;
        }
        else
        {
            Debug.LogError("Invalid input for amplitude (A). Make sure it's a valid number.");
        }

        if (float.TryParse(frequencyText.text, out float parsedB))
        {
            frequency = parsedB;
        }
        else
        {
            Debug.LogError("Invalid input for frequency (B). Make sure it's a valid number.");
        }

        if (float.TryParse(phaseShiftText.text, out float parsedC))
        {
            phaseShift = parsedC;
        }
        else
        {
            Debug.LogError("Invalid input for phase shift (C). Make sure it's a valid number.");
        }

        if(lineRenderer == null) 
        {
            Destroy(transform.gameObject);
        }

        // Redraw the trigonometric line after updating values
        DrawTrigEquation();
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

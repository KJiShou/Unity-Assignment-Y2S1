using TMPro;
using TS.DoubleSlider;
using UnityEngine;

public class LineRendererLinear : MonoBehaviour
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

    // Portal instances
    private GameObject startPortalInstance;
    private GameObject endPortalInstance;

    void Awake()
    {
        _slider.OnValueChanged.AddListener(SliderDouble_ValueChanged);
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;

        // Instantiate portals
        if (startPortalPrefab != null)
        {
            startPortalInstance = Instantiate(startPortalPrefab);
        }
        if (endPortalPrefab != null)
        {
            endPortalInstance = Instantiate(endPortalPrefab);
        }

        // Initial draw
        UpdateEquationValues();
    }

    void Update()
    {
        UpdateEquationValues();
    }

    public void DrawLinearEquation()
    {
        Vector3[] positions = new Vector3[numPoints];
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

        // Redraw the line after updating values
        DrawLinearEquation();
    }

    private void SliderDouble_ValueChanged(float min, float max)
    {
        xStart = min;
        xEnd = max;
    }
}

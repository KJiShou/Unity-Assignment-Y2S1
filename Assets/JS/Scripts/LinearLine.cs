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

    public Vector3[] linePoints;  // Store line points for the spaceship to follow

    // Portal prefabs
    public GameObject startPortalPrefab;
    public GameObject endPortalPrefab;

    private GameObject startPortalInstance;
    private GameObject endPortalInstance;
    private GameObject portalContainer;
    public int lineID;

    void Awake()
    {
        _slider.OnValueChanged.AddListener(SliderDouble_ValueChanged);
    }

    void Start()
    {
        lineID = GetInstanceID();
        lineRenderer.positionCount = numPoints;

        // Create a container for portals
        portalContainer = new GameObject("PortalContainer");
        portalContainer.transform.SetParent(this.transform);

        GameObject targetParent = GameObject.Find("Equation UI Set");
        if (targetParent != null)
        {
            portalContainer.transform.SetParent(targetParent.transform, false);
        }
        else
        {
            Debug.LogError("MainCanvas not found.");
        }

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

    public void DrawLinearEquation()
    {
        linePoints = new Vector3[numPoints];  // Initialize the array to store line points
        float xStep = (xEnd - xStart) / (numPoints - 1);

        for (int i = 0; i < numPoints; i++)
        {
            float x = xStart + i * xStep;
            float y = m * x + c;  // y = mx + c
            y = Mathf.Clamp(y, yMin, yMax);  // Clamp the y value
            linePoints[i] = new Vector3(x, y, 0);  // Store the point

            if (i == 0 && startPortalInstance != null)
            {
                startPortalInstance.transform.position = linePoints[0];
            }
            if (i == numPoints - 1 && endPortalInstance != null)
            {
                endPortalInstance.transform.position = linePoints[numPoints - 1];
            }
        }

        lineRenderer.SetPositions(linePoints);  // Update the LineRenderer with the new points
    }

    public void UpdateEquationValues()
    {
        if (float.TryParse(slopeText.text, out float parsedM))
        {
            m = parsedM;
        }
        else
        {
            Debug.LogError("Invalid input for slope (m).");
        }

        if (float.TryParse(interceptText.text, out float parsedC))
        {
            c = parsedC;
        }
        else
        {
            Debug.LogError("Invalid input for intercept (c).");
        }

        DrawLinearEquation();  // Redraw the line
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal.");
        }
    }
}
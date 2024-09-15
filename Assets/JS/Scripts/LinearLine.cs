using TMPro;
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
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
    }

    void Update() {
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
}

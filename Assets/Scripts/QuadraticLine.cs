using UnityEngine;

public class QuadraticLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numPoints = 50;
    public float xStart = -10f;
    public float xEnd = 10f;
    public float a = 1f; // Coefficient of x^2
    public float b = 0f; // Coefficient of x
    public float c = 0f; // Constant term
    public float yMin = -10f; // Minimum y value
    public float yMax = 10f;  // Maximum y value

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
        DrawQuadraticCurve();
    }

    void DrawQuadraticCurve()
    {
        Vector3[] positions = new Vector3[numPoints];
        float xStep = (xEnd - xStart) / (numPoints - 1);
        for (int i = 0; i < numPoints; i++)
        {
            float x = xStart + i * xStep;
            float y = a * x * x + b * x + c;
            y = Mathf.Clamp(y, yMin, yMax); // Limit y value
            positions[i] = new Vector3(x, y, 0);
        }
        lineRenderer.SetPositions(positions);
    }
}
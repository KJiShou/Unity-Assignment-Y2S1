using System.Collections;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    AudioManager audioManager;

    GameObject player;
    Animation anim;
    Rigidbody2D playerRb;
    public int lineID;
    bool inPortal;
    public bool isReversing = false; // Flag for moving from end of line to start
    private Vector3[] linePoints;
    private int currentPointIndex = 0;

    LineRendererLinear lineRendererLinear;
    QuadraticLineRenderer quadraticLineRenderer;
    TrigonometricLineRenderer trigonometricLineRenderer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRb = player.GetComponent<Rigidbody2D>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Handle BlackHole Collision
        if (CompareTag("BlackHole") && collision.CompareTag("Player")) 
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f) 
            {
                StartCoroutine(MoveInPortal());
                //player.GetComponent<SpaceshipController>().StartShrinking();
                return; // Exit, no further processing after black hole.
            }
        }

        // Normal Portal Handling
        if (collision.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                Debug.Log("Player entered portal");

                lineRendererLinear = FindLineRendererByID(lineID);
                if (lineRendererLinear == null)
                {
                    quadraticLineRenderer = FindLineRendererByIDQ(lineID);
                    if (quadraticLineRenderer == null)
                    {
                        trigonometricLineRenderer = FindLineRendererByIDT(lineID);
                        if (trigonometricLineRenderer == null)
                        {
                            Debug.LogError("No LineRenderer found");
                            return;
                        }
                        else
                        {
                            linePoints = trigonometricLineRenderer.positions;
                        }
                    }
                    else
                    {
                        linePoints = quadraticLineRenderer.positions;
                    }
                }
                else
                {
                    linePoints = lineRendererLinear.linePoints;
                }

                if (isReversing)
                {
                    currentPointIndex = linePoints.Length - 1; // Start at the last point for reverse
                }
                inPortal = true;
                player.GetComponent<SpaceshipFollowLine>().isMoving = false;
                StartCoroutine(PortalIn());
            }
        }
    }


    private LineRendererLinear FindLineRendererByID(int id)
    {
        LineRendererLinear[] allLineRenderers = FindObjectsOfType<LineRendererLinear>();
        foreach (var lineRenderer in allLineRenderers)
        {
            if (lineRenderer.lineID == id)
            {
                return lineRenderer;
            }
        }
        return null;
    }

    private QuadraticLineRenderer FindLineRendererByIDQ(int id)
    {
        QuadraticLineRenderer[] allLineRenderers = FindObjectsOfType<QuadraticLineRenderer>();
        foreach (var lineRenderer in allLineRenderers)
        {
            if (lineRenderer.lineID == id)
            {
                return lineRenderer;
            }
        }
        return null;
    }

    private TrigonometricLineRenderer FindLineRendererByIDT(int id)
    {
        TrigonometricLineRenderer[] allLineRenderers = FindObjectsOfType<TrigonometricLineRenderer>();
        foreach (var lineRenderer in allLineRenderers)
        {
            if (lineRenderer.lineID == id)
            {
                return lineRenderer;
            }
        }
        return null;
    }

    IEnumerator PortalIn()
    {
        audioManager.PlaySFX(audioManager.portalIn);
        anim.Play("Portal In");
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator PortalOut()
    {
        audioManager.PlaySFX(audioManager.portalOut);
        playerRb.simulated = true;
        anim.Play("SpaceshipOutPortal");

        // Start from the first point when exiting the portal (normal movement)
        currentPointIndex = 0;
        player.GetComponent<SpaceshipController>().ResumeMovement();
        isReversing = false; // Set flag to false when moving forward from the first point

        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator MoveInPortal()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }

    void Update()
    {
        if (inPortal && linePoints != null)
        {
            MoveSpaceship();
        }


    }

    private void MoveSpaceship()
    {
        if (player.GetComponent<SpaceshipController>().collisionCount ==0) {
            // Check if we are moving from end to start or start to end
            if (isReversing)
            {
                if (currentPointIndex < 0)
                {
                    // Stop when we reach the start of the line
                    EndPortalInteraction();
                    return;
                }
            }
            else
            {
                // Moving from start to end
                if (currentPointIndex >= linePoints.Length)
                {
                    EndPortalInteraction();
                    return;
                }
            }

            // Get the current position and the target point from the line
            Vector3 targetPoint = linePoints[currentPointIndex];
            Vector3 currentPosition = player.transform.position;

            // Calculate the direction from the current position to the target point
            Vector3 direction = (targetPoint - currentPosition).normalized;

            // Rotate the spaceship to face the direction of movement
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0, 0, angle - 90);  // Adjust for 2D rotation

            // Move the spaceship towards the next point
            player.transform.position = Vector3.MoveTowards(currentPosition, targetPoint, player.GetComponent<SpaceshipController>().speed * Time.deltaTime);

            // Use a reasonable threshold to determine if the spaceship is "close enough" to the point
            float distanceThreshold = 0.05f;

            // Check if the spaceship has reached the target point
            if (Vector3.Distance(player.transform.position, targetPoint) < distanceThreshold)
            {
                if (isReversing)
                {
                    // If moving in reverse, move to the previous point
                    currentPointIndex--;
                }
                else
                {
                    // If moving forward, move to the next point
                    currentPointIndex++;
                }
            }
            float distanceToLastPoint = Vector3.Distance(player.transform.position, linePoints[linePoints.Length - 1]);

            if (distanceToLastPoint < 1.0f)  // Change 1.0f to whatever distance you want
            {
                playerRb.simulated = false;  // Disable physics when close to the last point
            }
        }
    }

    private void EndPortalInteraction()
    {
        inPortal = false;
        player.GetComponent<SpaceshipFollowLine>().isMoving = true;
        StartCoroutine(PortalOut());
        ResetPortalState();
    }

    private void ResetPortalState()
    {
        inPortal = false;
        isReversing = false;
        linePoints = null;
        lineRendererLinear = null;
        quadraticLineRenderer = null;
        trigonometricLineRenderer = null;
        currentPointIndex = 0;
        lineID = 0;
        Debug.Log("Portal state reset, ready for the next interaction.");
    }

    
}
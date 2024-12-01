using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public BoxCollider2D bounds;  // Assign the BoxCollider2D in the Inspector
    private Camera cam;
    
    [Header("Debug")]
    public float camHeight, camWidth;
    public float minX, maxX, minY, maxY;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Calculate the camera's dimensions based on aspect ratio
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        // Get bounds of the BoxCollider2D
        Bounds boxBounds = bounds.bounds;

        // Calculate min and max values based on collider size
        minX = boxBounds.min.x + camWidth;
        maxX = boxBounds.max.x - camWidth;
        minY = boxBounds.min.y + camHeight;
        maxY = boxBounds.max.y - camHeight;
    }

    public void Confine()
    {
        // Restrict the camera's position within the calculated bounds
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // Update bounds when aspect ratio changes
    void Update()
    {
        // Only update if screen changes
        if (camWidth != cam.orthographicSize * cam.aspect)
        {
            Start(); // Recalculate on change
        }
    }
}

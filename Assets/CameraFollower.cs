using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10); // Default offset for 2D cameras
    public float followSpeed = 5f;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
    }
}

using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 lastPosition;
    [HideInInspector] public float actualSpeed;
    private float positionThreshold = 0.0001f;
    private Animator ani;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
        ani = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float distanceMoved = (rb.position - lastPosition).magnitude;

        if (distanceMoved > positionThreshold)
        {
            actualSpeed = distanceMoved / Time.fixedDeltaTime;
        }
        else
        {
            actualSpeed = 0f;
        }

        lastPosition = rb.position;

        ani.SetFloat("speed", actualSpeed);
    }
}

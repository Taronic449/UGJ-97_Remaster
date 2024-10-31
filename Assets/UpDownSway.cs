using System.Collections;
using UnityEngine;

public class UpDownSway : MonoBehaviour
{
    public float swayDistance = 1f; // Distance to sway
    public float swaySpeed = 1f; // Speed of swaying
    public bool swayUpDown = true; // Toggle between up-down and right-left

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayDistance;
        Vector3 swayDirection = swayUpDown ? Vector3.up : Vector3.right;

        transform.position = startPos + swayDirection * sway;
    }
}

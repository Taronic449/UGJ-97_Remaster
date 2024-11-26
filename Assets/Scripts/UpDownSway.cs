using System.Collections;
using UnityEngine;

public class UpDownSway : MonoBehaviour
{
    public float swayDistance = 1f; // Distance to sway
    public float swaySpeed = 1f; // Speed of swaying
    public bool swayUpDown = true; // Toggle between up-down and right-left

    private Vector3 startPos;
    private RectTransform rectTransform;

    void Start()
    {
        TryGetComponent<RectTransform>(out rectTransform);
        
        if(rectTransform == null)
        {
            startPos = transform.position;
        }
        else
        {
            startPos = GetComponent<RectTransform>().localPosition;
        }
        
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayDistance;
        Vector3 swayDirection = swayUpDown ? Vector3.up : Vector3.right;

        if(rectTransform == null)
        {
            transform.position = startPos + swayDirection * sway;
        }
        else
        {
            rectTransform.localPosition = startPos + swayDirection * sway;
        }
        
    }
}

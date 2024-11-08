using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamSearch : MonoBehaviour
{
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = FindObjectOfType<PlayerController>().transform;
    }
}

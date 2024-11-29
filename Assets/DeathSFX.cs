using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSFX : MonoBehaviour
{
    void Start()
    {
        Invoke("end", 2);
    }

    void end()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}

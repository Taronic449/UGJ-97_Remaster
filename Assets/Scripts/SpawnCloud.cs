using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCloud : MonoBehaviour
{
    public GameObject enemy;
    public void spawnEnemy()
    {
        Instantiate(enemy, transform.position + new Vector3(0,0.4f), Quaternion.Euler(0,0,0));
        Destroy(gameObject);
    }
}

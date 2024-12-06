using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public DificultyManager dificultyManager;
    private float timer;              

    void Start()
    {
        dificultyManager = DificultyManager.Instance;

        timer = Random.Range(dificultyManager.spawnTime - 1.5f, dificultyManager.spawnTime + 1.5f);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            spawnEnemy();
            timer = Random.Range(dificultyManager.spawnTime - 2, dificultyManager.spawnTime + 2);
        }
    }

    private void spawnEnemy()
    {
        Instantiate(enemy, transform.position + new Vector3(0, -0.7f), Quaternion.identity);
    }
}

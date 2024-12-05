using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float initialSpawnTime = 27f;
    public float minimumSpawnTime = 2.5f;
    private float timer;              
    public float difficulty = 1f;
    public float difficultyMultiplier = 0.1f;

    void Start()
    {
        timer = Random.Range(initialSpawnTime - 5, initialSpawnTime);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            spawnEnemy();
            difficulty += difficultyMultiplier * Mathf.Sqrt(Time.timeSinceLevelLoad) / Time.timeSinceLevelLoad;
            float newSpawnTime = Mathf.Max(initialSpawnTime - difficulty, minimumSpawnTime);
            timer = Random.Range(7f, newSpawnTime);
        }
    }

    private void spawnEnemy()
    {
        Instantiate(enemy, transform.position + new Vector3(0, -0.7f), Quaternion.identity);
    }
}

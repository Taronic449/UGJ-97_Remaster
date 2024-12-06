using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultyManager : MonoBehaviour
{
    public static DificultyManager Instance;

    public float spawnTime;
    public float enemyStreght = 1;

    [Header("Inis")]
    [SerializeField] private float initialSpawnTime = 27f;
    [SerializeField] private float minimumSpawnTime = 4f;
    [SerializeField] public float difficulty = 1f;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;

        InvokeRepeating(nameof(UpdateDificulty), 1, 2);

        spawnTime = initialSpawnTime;
    }

    void UpdateDificulty()
    {
        difficulty +=  0.385f * Mathf.Sqrt(Time.timeSinceLevelLoad) / Time.timeSinceLevelLoad;

        spawnTime = Mathf.Max(initialSpawnTime - difficulty, minimumSpawnTime);

        enemyStreght = Mathf.Min(0.85f + (difficulty / 14), 4);
    }
}

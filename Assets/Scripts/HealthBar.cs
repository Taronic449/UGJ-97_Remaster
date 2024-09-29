using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;
    public int health;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    public void setHealth(int _health)
    {
        health = _health;

        for (int i = 0; i < 6; i++)
        {
            transform.GetChild(5 -i).gameObject.SetActive(i < health);
        }
    }
}

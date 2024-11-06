using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerController> players;
    public static PlayerManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    public void InitializeAll()
    {
        Debug.Log(1);
        Debug.Log(players);


        foreach (PlayerController item in players)
        {
            item.Initialize();
        }
    }
}

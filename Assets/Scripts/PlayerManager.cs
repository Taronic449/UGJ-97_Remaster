using System;
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
        foreach (PlayerController item in players)
        {
            item.Initialize();
        }
    }
    internal void Select(PlayerController playerController)
    {
        if(FindAnyObjectByType<SelectPlayer>())
        {
            if(playerController == players[0])
            {
                Debug.Log("select P1");
                FindAnyObjectByType<SelectPlayer>().SelectP1();
            }
            else
            {
                Debug.Log("select P2");
                FindAnyObjectByType<SelectPlayer>().SelectP2();
            }
        }
    }
}

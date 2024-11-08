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
                FindAnyObjectByType<SelectPlayer>().SelectP1();
            }
            else
            {
                FindAnyObjectByType<SelectPlayer>().SelectP2();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public ushort ID;
    public enum Effect
    {
        RestoreHealth,
        KillNextEnemys,
        ProtectFromAttacks

    }
    
    public Effect effect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerController>().SetEffect(effect))
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}

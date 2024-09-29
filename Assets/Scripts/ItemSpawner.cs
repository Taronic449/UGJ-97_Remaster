using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> items;
    public LayerMask itemMask;
    void Start()
    {
        Invoke(nameof(spawnItem), Random.Range(20f,60f));
    }

    private void spawnItem()
    {
        if(!Physics2D.OverlapCircle(transform.position, 5, itemMask) && !UIManger.PAUSE)
        {
            ushort ID = (ushort)Random.Range(0,items.Count);

            Instantiate(items[ID], transform.position, Quaternion.Euler(0,0,0));

            Invoke(nameof(spawnItem), Random.Range(20f,60f));
        }
        
    }

}

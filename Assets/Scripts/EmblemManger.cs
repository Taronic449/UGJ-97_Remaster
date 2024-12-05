using System.Collections.Generic;
using UnityEngine;

public class EmblemManger : MonoBehaviour
{
    public static EmblemManger Instance;
    public List<ushort> IDs = new();

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
        
        UpdateEmblems();
        InvokeRepeating(nameof(UpdateEmblems), 1 ,0.1f);
    }

    void UpdateEmblems()
    {
        

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            if(!IDs.Contains(item.GetComponent<PowerUp>().ID))
                IDs.Add(item.GetComponent<PowerUp>().ID);
        }

        for (ushort i = 0; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(IDs.Contains(i));
        }
    }

}

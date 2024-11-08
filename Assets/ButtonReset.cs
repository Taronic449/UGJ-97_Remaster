using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReset : MonoBehaviour
{
    public void ResetAll()
    {
        foreach (var item in GetComponentsInChildren<Button>())
        {
            item.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public UIManger.Button function;
    private Button myButton;

    void Awake()
    {
        myButton = GetComponent<Button>();

        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
    }


    void OnButtonClick()
    {
        UIManger.Instance.Press(function);
    }
}

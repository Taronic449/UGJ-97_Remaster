using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UIManger.Button function;
    private Button myButton;
    private Transform text;
    public bool brownButton;
    public bool greenButton;



    void Awake()
    {
        myButton = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>().transform;
                text.GetComponent<RectTransform>().localPosition = new Vector2(0.15f, 0.4f);


        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(brownButton)
            text.GetComponent<RectTransform>().localPosition = new Vector2(0.15f, -0.6f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(brownButton)
            text.GetComponent<RectTransform>().localPosition = new Vector2(0.15f, 0.4f);

    }

    void OnButtonClick()
    {
        if(greenButton)
        {
            EventSystem.current.GetComponent<InputSystemUIInputModule>().enabled = false;
            StartCoroutine(RollUp());
        }
        else
        {
            UIManger.Instance.Press(function);
        }
    }

    private IEnumerator RollUp()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float speed = 3;

        while(rect.sizeDelta.x > 11.125f)
        {
            rect.sizeDelta -= new Vector2(Time.deltaTime * speed, 0);
            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta -= new Vector2(Time.deltaTime * speed, 0);


            speed += Time.deltaTime * speed * 4;
            yield return null;
        }

        EventSystem.current.GetComponent<InputSystemUIInputModule>().enabled = true;
        UIManger.Instance.Press(function);
    }
}

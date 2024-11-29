using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UIManger.Button function;
    private Button myButton;
    private Transform text;
    public bool brownButton;
    public string SceneNameOptinal;
    public bool greenButton;
    public AnimationCurve easeInEaseOutCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float minWidth = 33f; // Minimum width for the roll-up effect
    public float childOffset = 50f; // Offset from the right edge for the child rect

    void Awake()
    {
        myButton = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>().transform;

        // text.GetComponent<RectTransform>().localPosition = new Vector2(0.15f, 0.4f);
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
        if (brownButton)
        	text.GetComponent<RectTransform>().localPosition = new Vector2(0.15f, -0.6f * (8/GetComponent<RectTransform>().localScale.x));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (brownButton)
            text.GetComponent<RectTransform>().localPosition = new Vector2(0.15f, 0.4f);
    }

    void OnButtonClick()
    {
        if (greenButton)
        {
            EventSystem.current.GetComponent<InputSystemUIInputModule>().enabled = false;
            StartCoroutine(RollUp());
        }
        else
        {
            UIManger.Instance.Press(function,SceneNameOptinal);
        }

        MusicManager.Instance.PressSound();
    }

    private IEnumerator RollUp()
    {
        RectTransform rect = GetComponent<RectTransform>();
        RectTransform childRect = transform.GetChild(0).GetComponent<RectTransform>();
        float duration = 1f; // Duration of the animation
        float elapsedTime = 0f;

        float initialWidth = rect.sizeDelta.x;

        while (elapsedTime < duration && rect.sizeDelta.x > minWidth)
        {
            elapsedTime += Time.deltaTime;
            float t = easeInEaseOutCurve.Evaluate(elapsedTime / duration);
            float newWidth = Mathf.Lerp(initialWidth, minWidth, t);

            rect.sizeDelta = new Vector2(Mathf.Max(newWidth, minWidth), rect.sizeDelta.y);
            childRect.sizeDelta = new Vector2(Mathf.Max(newWidth - childOffset, minWidth - childOffset), childRect.sizeDelta.y);

            yield return null;
        }

        rect.sizeDelta = new Vector2(minWidth, rect.sizeDelta.y);
        childRect.sizeDelta = new Vector2(minWidth - childOffset, childRect.sizeDelta.y);

        EventSystem.current.GetComponent<InputSystemUIInputModule>().enabled = true;
        UIManger.Instance.Press(function,SceneNameOptinal);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class IntroButtonAdditions : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // Find all buttons in the scene and set interactable to false
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button btn in buttons)
        {
            btn.enabled = false;
        }
    }
}

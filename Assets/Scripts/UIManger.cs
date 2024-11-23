using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    public static UIManger Instance;
    public static bool PAUSE;
    private Image darkenImage;
    public GameObject messageCanvas;

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

        darkenImage = GetComponentInChildren<Image>();
    }
    
    public enum Button
    {
        MainMenu = 1,
        CloseGame = 2,
        StartGame = 3,
        Pause = 6,
        Unpause = 7,
        SinglePlayer = 11,
        LoadScene = 12,
        LoadSceneAdd = 13,
        Death

    }

    public void Press(Button function, string? scenename)
    {
        switch (function)
        {
            case Button.MainMenu:
                SceneManager.LoadScene("Main Menu");

                foreach (var item in FindObjectsOfType<PlayerController>())
                {
                    Destroy(item.gameObject);
                }

                Time.timeScale = 1f;
                PAUSE = false;
            break;  

            case Button.CloseGame:
                Application.Quit();
            break;

            // case Button.LoadSceneAdd:
            //     Application.Quit();
            // break;

            case Button.LoadScene:
                SceneManager.LoadScene(scenename);
            break;

            case Button.StartGame:
                SceneManager.LoadScene("Select Stage");
                PAUSE = false;
                Time.timeScale = 1f;
            break;

            case Button.SinglePlayer:
                SceneManager.LoadScene("Select Stage");
                FindAnyObjectByType<PlayerInputManager>().JoinPlayer(pairWithDevice: default);
                PAUSE = false;
                Time.timeScale = 1f;
            break;

            case Button.Pause:
                SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
                Time.timeScale = 0f;
                PAUSE = true;
            break;

            case Button.Unpause:
                Time.timeScale = 1f;
                SceneManager.UnloadSceneAsync("Pause Menu");
                PAUSE = false;
            break;
            case Button.Death:
                StartCoroutine(DeathAni());
            break;

            default:
                break;
        }
    }

    private IEnumerator DeathAni()
    {
        messageCanvas.transform.GetChild(0).localScale = Vector3.zero;
        messageCanvas.SetActive(true);
        LeanTween.scale(messageCanvas.transform.GetChild(0).gameObject, Vector3.one, 1.5f).setEase(LeanTweenType.easeInOutExpo);

        float elapsedTime = 0f;
        float timeElapsedForTimeScale = 0f;

        bool hasLoadedScene = false;

        while (elapsedTime < 5)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / 4.5f);
            darkenImage.color = new Color(0f, 0f, 0f, alpha);

            if (timeElapsedForTimeScale < 3f)
            {
                timeElapsedForTimeScale += Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Lerp(1f, 0f, timeElapsedForTimeScale / 3f);
            }


            if (!hasLoadedScene && elapsedTime >= 5f)
            {
                hasLoadedScene = true;
                SceneManager.LoadScene("Match Over", LoadSceneMode.Additive);
            }

            yield return null; 
        }

        darkenImage.color = new Color(0f, 0f, 0f, 1f);
        Time.timeScale = 1f;
    }
}

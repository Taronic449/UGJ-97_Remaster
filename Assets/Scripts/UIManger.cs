using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        MainMenu,
        CloseGame,
        StartGame,
        Credits,
        Settings,
        Pause,
        Unpause,
        Death,
        SelectPlayer,
        Controls

    }


    public void Press(Button function)
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

            case Button.SelectPlayer:
                SceneManager.LoadScene("Select Player");
            break;

            case Button.Controls:
                SceneManager.LoadScene("Controls");
            break;

            case Button.StartGame:
                SceneManager.LoadScene("Battlefield");
                PAUSE = false;
                Time.timeScale = 1f;
            break;

            case Button.Credits:
                SceneManager.LoadScene("Credits");
            break;

            case Button.Settings:
                SceneManager.LoadScene("Options");
            break;

            case Button.Pause:
                SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
                Time.timeScale = 0f;
                PAUSE = true;
            break;
            case Button.Death:
                PlayerPrefs.SetInt("Score", Mathf.Max(GameManger.Instance.score, PlayerPrefs.GetInt("Score")));
                StartCoroutine(DeathAni());
                PAUSE = true;
            break;

            case Button.Unpause:
                Time.timeScale = 1f;
                SceneManager.UnloadSceneAsync("Pause Menu");
                PAUSE = false;
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

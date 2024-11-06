using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    public CinemachineVirtualCamera cam;
    public TextMeshProUGUI scoreText;
    public int score;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            PlayerManager.Instance.InitializeAll();

        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;

            PlayerManager.Instance.InitializeAll();
        }
    }



    public void addScore(ushort amount)
    {
        score += amount;

        if (scoreText != null)
        {
            LeanTween.scale(scoreText.gameObject, Vector3.one * 1.5f, 0.15f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
            {
                LeanTween.scale(scoreText.gameObject, Vector3.one, 0.15f).setEase(LeanTweenType.easeInOutExpo);
            });

            LeanTween.alphaText(scoreText.rectTransform, 0f, 0.08f).setEase(LeanTweenType.easeInOutExpo).setLoopPingPong(1);
        }
    }

    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text  = score.ToString();
        }
    }
}

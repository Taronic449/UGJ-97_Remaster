
using Cinemachine;
using TMPro;
using UnityEngine;
using static PlayerController;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    public CinemachineVirtualCamera cam;
    public TextMeshProUGUI scoreTextP1;
    public TextMeshProUGUI scoreTextP2;

    public HealthBar  healthBarP1;
    public HealthBar  healthBarP2;


    public int scoreP1;
    public int scoreP2;


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

        if(PlayerManager.Instance.players.Count <= 1)
        {
            healthBarP2.gameObject.SetActive(false);
            scoreTextP2.gameObject.SetActive(false);
        }
    }

    public void setHealth(PlayerType playerType, int _health)
    {
        if(playerType == PlayerType.yori)
        {
            healthBarP1.setHealth(_health);
        }
        else
        {
            healthBarP2.setHealth(_health);
        }
    }



    public void addScore(PlayerType? playerType, ushort amount)
    {
        
        if(playerType == null)
            return;
            
        if(playerType == PlayerType.yori)
        {
            scoreP1 += amount;

            LeanTween.scale(scoreTextP1.gameObject, Vector3.one * 1.5f, 0.15f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
            {
                LeanTween.scale(scoreTextP1.gameObject, Vector3.one, 0.15f).setEase(LeanTweenType.easeInOutExpo);
            });

            LeanTween.alphaText(scoreTextP1.rectTransform, 0f, 0.08f).setEase(LeanTweenType.easeInOutExpo).setLoopPingPong(1);
        }
        else
        {
            scoreP2 += amount;

            LeanTween.scale(scoreTextP2.gameObject, Vector3.one * 1.5f, 0.15f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
            {
                LeanTween.scale(scoreTextP2.gameObject, Vector3.one, 0.15f).setEase(LeanTweenType.easeInOutExpo);
            });

            LeanTween.alphaText(scoreTextP2.rectTransform, 0f, 0.08f).setEase(LeanTweenType.easeInOutExpo).setLoopPingPong(1);
        }
    }

    void Update()
    {
        scoreTextP1.text  = "Score: " + scoreP1.ToString();
        scoreTextP2.text  = "Score: " + scoreP2.ToString();
    }
}

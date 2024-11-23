using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI p1,p2;
    public static int score1, score2; // not uint for playerPredfs clearity
    
    void Awake()
    {
        if(p2 == null)
        {
            p1.text = score1.ToString(); 

            PlayerPrefs.SetInt("Score", (int)Mathf.Max(PlayerPrefs.GetInt("Score"),score1));
        }
        else
        {
            p1.text = score1.ToString(); 
            p1.text = score2.ToString();

            PlayerPrefs.SetInt("Score", (int)Mathf.Max(PlayerPrefs.GetInt("Score"),score1,score2));
        }
    }


}

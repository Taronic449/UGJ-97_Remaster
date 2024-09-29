using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI scoreT;
    void Start()
    {
        scoreT.text = PlayerPrefs.GetInt("Score").ToString(); 
    }


}

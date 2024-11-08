using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public byte iP1, iP2;

    public List<TextMeshProUGUI> indicator;
    void Start()
    {
       
    }
    void Update()
    {
        if(PlayerManager.Instance.players.Count == 0)
        {
            	foreach (var item in indicator)
                {
                    item.enabled = false;
                }
        }
        else if(PlayerManager.Instance.players.Count == 1)
        {
            iP1 = PlayerManager.Instance.players[0].indicator;

            indicator[0].enabled = true;
            indicator[1].enabled = true;

            if(iP1 == 0)
            {
                indicator[0].color = Color.red;
                indicator[1].color = Color.gray;

            }
            else
            {
                indicator[0].color = Color.gray;
                indicator[1].color = Color.red;
            }
        }
        else
        {
            iP1 = PlayerManager.Instance.players[0].indicator;
            iP2 = PlayerManager.Instance.players[1].indicator;

            indicator[0].enabled = true;
            indicator[1].enabled = true;
            indicator[2].enabled = true;
            indicator[3].enabled = true;

            if(iP1 == 0)
            {
                indicator[0].color = Color.red;
                indicator[1].color = Color.gray;

            }
            else
            {
                indicator[0].color = Color.gray;
                indicator[1].color = Color.red;
            }

            if(iP2 == 0)
            {
                indicator[2].color = Color.blue;
                indicator[3].color = Color.gray;

            }
            else
            {
                indicator[2].color = Color.gray;
                indicator[3].color = Color.blue;
            }
        }
    }

    public void SelectP1()
    {
        if(iP1 == 0 && image1.color.a > 0.8f)
        {
            PlayerManager.Instance.players[0].selected = true;
            image1.color = new Color(1,1,1,0.7f);
        }
        else if(iP1 == 1 && image2.color.a > 0.8f)
        {
            PlayerManager.Instance.players[0].selected = true;
            image2.color = new Color(1,1,1,0.7f);
        }
    }

    public void SelectP2()
    {
        if(iP1 == 0 && image1.color.a > 0.8f)
        {
            PlayerManager.Instance.players[1].selected = true;
            image1.color = new Color(1,1,1,0.7f);
        }
        else if(iP1 == 1 && image2.color.a > 0.8f)
        {
            PlayerManager.Instance.players[1].selected = true;
            image2.color = new Color(1,1,1,0.7f);
        }
    }

}

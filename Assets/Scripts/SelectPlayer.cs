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
            indicator[2].enabled = false;
            indicator[3].enabled = false;

            for (int i = 0; i < 2; i++)
            {
                //black (and other invisible) if selected otherwise red and gray
                indicator[i].color = i == iP1 ? (PlayerManager.Instance.players[0].selected ? Color.black : Color.red) : (PlayerManager.Instance.players[0].selected ? new Color(0,0,0,0) : Color.gray);
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
            
            for (int i = 0; i < 2; i++)
            {
                //black (and other invisible) if selected otherwise red and gray
                indicator[i].color = i == iP1 ? (PlayerManager.Instance.players[0].selected ? Color.black : Color.red) : (PlayerManager.Instance.players[0].selected ? new Color(0,0,0,0) : Color.gray);
            } 

            for (int i = 2; i < 4; i++)
            {
                //black (and other invisible) if selected otherwise red and gray
                indicator[i].color = i-2 == iP2 ? (PlayerManager.Instance.players[1].selected ? Color.black : Color.blue) : (PlayerManager.Instance.players[1].selected ? new Color(0,0,0,0) : Color.gray);
            }
        }
    }

    public void SelectP1()
    {
        if(iP1 == 0 && image1.color.a > 0.8f)
        {
            PlayerManager.Instance.players[0].selected = true;
            image1.color = new Color(1,1,1,0.7f);

            PlayerManager.Instance.players[0].SetPlayerType(0);
        }
        else if(iP1 == 1 && image2.color.a > 0.8f)
        {
            PlayerManager.Instance.players[0].selected = true;
            image2.color = new Color(1,1,1,0.7f);

            PlayerManager.Instance.players[0].SetPlayerType(1);

        }
    }

    public void SelectP2()
    {
        if(iP1 == 0 && image1.color.a > 0.8f)
        {
            PlayerManager.Instance.players[1].selected = true;
            image1.color = new Color(1,1,1,0.7f);

            PlayerManager.Instance.players[1].SetPlayerType(0);
        }
        else if(iP1 == 1 && image2.color.a > 0.8f)
        {
            PlayerManager.Instance.players[1].selected = true;
            image2.color = new Color(1,1,1,0.7f);

            PlayerManager.Instance.players[0].SetPlayerType(0);

        }
    }

}


using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour
{
    void Start()
    {
        if(FindAnyObjectByType<PlayerController>() == null)
        {
            SceneManager.LoadScene("Main Menu");

            foreach (var item in FindObjectsOfType<PlayerController>())
            {
                Destroy(item.gameObject);
            }

            Time.timeScale = 1f;
            UIManger.PAUSE = false;
        }
    }


    public void GailyGreens()
    {
        int n = 0;

        foreach (PlayerController item in PlayerManager.Instance.players)
        {
            item.transform.position = new Vector2(n*2,0);
            n++;
        }

        SceneManager.LoadScene("Gaily Greens");
    }

    public void DanujaDojo()
    {
        int n = 0;

        foreach (PlayerController item in PlayerManager.Instance.players)
        {
            item.transform.position = new Vector2(n*2,0);
            n++;
        }

        SceneManager.LoadScene("Danuja Dojo");
    }

    public void VolatileVolcano()
    {
        int n = 0;

        foreach (PlayerController item in PlayerManager.Instance.players)
        {
            item.transform.position = new Vector2(n*2,0);
            n++;
        }

        SceneManager.LoadScene("Volatile Volcano");
    }
}

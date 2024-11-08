using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    public byte count = 0;

    void Start()
    {
        p1.SetActive(false);
        p2.SetActive(false);
    }

    public void NewPlayer()
    {
        count++;
        UpdatePlayer();
    }

    public void PlayerLeave()
    {
        count++;
        UpdatePlayer();
    }

    private void UpdatePlayer()
    {
        p1.SetActive(count > 0);
        p2.SetActive(count > 1);
    }
}

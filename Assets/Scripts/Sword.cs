using UnityEngine;
using static PlayerController;

public class Sword : MonoBehaviour
{
    public string targetTag;
    public PlayerType playerType = PlayerType.none;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            collision.GetComponent<Health>().damage(1, (collision.transform.position - transform.position).normalized, playerType != PlayerType.none, playerType);
        }
    }
}

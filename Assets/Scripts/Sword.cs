using UnityEngine;

public class Sword : MonoBehaviour
{
    public string targetTag;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            collision.GetComponent<Health>().damage(1, (collision.transform.position - transform.position).normalized);
        }
    }
}

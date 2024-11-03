
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    private Animator ani;
    private SpriteRenderer sprite;
    private int force = 520;
    public float timer;

    public Vector2 startoff;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerController>().transform;
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        startoff = Random.insideUnitCircle * 1;
    }


    void FixedUpdate()
    {
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position + (Vector3) startoff);

        sprite.flipX = player.position.x - rb.position.x > 0;

        if(((Vector2)player.position - rb.position).magnitude < 1.6f && !ani.GetBool("attack") && timer < 0)
        {
            timer = 3;
            rb.velocity = Vector2.zero;
            ani.SetBool("attack", true);
            force = 0;
        }

        timer -= Time.deltaTime;
    }

    public void Death()
    {
        GameManger.Instance.addScore(10);
        Destroy(gameObject);
    }

    public void hit()
    {
        ani.SetTrigger("hit");
    }
    public void resetForce()
    {
        ani.SetBool("attack", false);
        force = 520;
    }
}

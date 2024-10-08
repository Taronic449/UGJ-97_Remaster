using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    private Animator ani;
    private SpriteRenderer sprite;
    private int force = 520;
    public float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

    }


    void FixedUpdate()
    {
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        rb.AddForce(direction * force * Time.deltaTime);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 1.2f);

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

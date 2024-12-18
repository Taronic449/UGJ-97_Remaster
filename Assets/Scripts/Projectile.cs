using Lean.Pool;
using UnityEngine;
using static PlayerController;

public class Projectile : MonoBehaviour, IPoolable
{
    public float speed = 5f;  // Speed of the projectile
    private Rigidbody2D rb2d;
    public ushort damage;
    public Vector2 vel;
    public float timer;
    public PlayerType playerType;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize(PlayerController player, PlayerType newplayerType)
    {
        // Rotate towards mouse
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 direction = (mousePosition - transform.position).normalized;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        playerType = newplayerType;

        rb2d.velocity = (player.GetComponent<SpriteRenderer>().flipX ? new Vector2(1,0) : new Vector2(-1,0)) * speed;

        vel = player.GetComponent<SpriteRenderer>().flipX ? new Vector2(1,0) : new Vector2(-1,0);
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if(timer > 0.85f)
        {
            Lean.Pool.LeanPool.Despawn(this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().damage(damage, vel, true, playerType);
        }

        Lean.Pool.LeanPool.Despawn(this);
    }

    public void OnSpawn()
    {
        GetComponentInChildren<TrailRenderer>().Clear();
        GetComponentInChildren<TrailRenderer>().enabled = true;
        timer = 0;
    }

    public void OnDespawn()
    {
        GetComponentInChildren<TrailRenderer>().enabled = false;
        timer = 0;

    }
}

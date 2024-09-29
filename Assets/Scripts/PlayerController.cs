using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static PowerUp;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothTime = 0.08f;
    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private Vector2 targetVelocity;
    public ushort damage;
    private Health health;
    private SpriteRenderer spriteR;
    private bool alive = true;
    public bool fish;
    public GameObject fishProjectile;
    public GameObject shurikenProjectile;
    public Transform projectilePoint;
    private Animator ani;
    private float stun;
    private float colldown;


    [Header("Debug")]
    public Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        spriteR = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        HealthBar.Instance.setHealth(health.health);
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void OnFire(InputValue inputValue)
    {
        stun = 0.25f;
        ani.SetTrigger("attack");
    }
    void OnThrow(InputValue inputValue)
    {
        if(colldown < 0)
        {
            GameObject go = Lean.Pool.LeanPool.Spawn(fish ? fishProjectile : shurikenProjectile, projectilePoint.position, Quaternion.Euler(0,0,0));
            go.GetComponent<Projectile>().Initialize();

            colldown = fish ? 3 : 5;
        }

    }

    void Update()
    {
        if(UIManger.PAUSE)
        {
            targetVelocity = Vector2.zero;
            return;
        }

        targetVelocity = moveInput * moveSpeed;

        if (rb.velocity.magnitude > 0.01f)
        {
            if (moveInput.x < 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
        }

        colldown -= Time.deltaTime;

        stun -= Time.deltaTime;


        ani.SetFloat("speed", rb.velocity.magnitude);
    }

    void FixedUpdate()
    {
        if(stun <0)
        {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
    }


    //Damage
    public void hit()
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulseWithForce(1);
        HealthBar.Instance.setHealth(health.health);

        ani.SetTrigger("hit");

    }
    public void Death()
    {
        if(alive)
        {
            alive = false;
            GetComponent<CinemachineImpulseSource>().GenerateImpulseWithForce(3);
            UIManger.Instance.Press(UIManger.Button.Death);
        }
        
    }

    //Effects
    public bool SetEffect(Effect effect)
    {
        switch (effect)
        {
            case Effect.RestoreHealth:
                GetComponent<Health>().heal(999);
                HealthBar.Instance.setHealth(health.health);
            break;

            case Effect.KillNextEnemys:
                Invoke(nameof(deactivateFish), 10);
                fish = true;
                colldown = 0;
            break;

            case Effect.ProtectFromAttacks:
                GetComponent<Health>().activateShield();
            break;

            default:
            break;
        }

        return true;
    }

    void deactivateFish()
    {
        fish = false;
    }
}

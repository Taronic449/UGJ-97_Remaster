using Cinemachine;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
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
    public Animator swordAni;

    private float stun;
    private float colldown;
    public Transform rotPivot;

    private CinemachineVirtualCamera cam;
    public byte indicator;
    public bool selected;
    private byte playerType;

    public AnimatorController yori,Mei;


    [Header("Debug")]
    public Vector2 moveInput;

    public void SetPlayerType(byte type)
    {
        if(type == 0)
        {
            ani.runtimeAnimatorController = yori;
        }
        else
        {
            ani.runtimeAnimatorController = Mei;
        }


        playerType = type;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        spriteR = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();

        transform.position = new Vector2(10000,10000);

        PlayerManager.Instance.players.Add(this);


        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        // HealthBar.Instance.setHealth(health.health);
        
        cam = GameManger.Instance.cam;
        cam.Follow = transform;
    }
    
    
    void Start()
    {
 
    }

    void OnDestroy()
    {
        PlayerManager.Instance.players.Remove(this);
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();

        if(!selected)
            UpdateIndicator(moveInput.x);
    }

    void UpdateIndicator(float xInput)
    {
        if (xInput > 0)
        {
            indicator += 1;
        }
        else if (xInput < 0)
        {
            indicator -= 1;
        }

        indicator = (byte)Mathf.Clamp(indicator, 0f, 1f);
    }

    void OnFire(InputValue inputValue)
    {
        swordAni.Play("attac");

        
    }

    void OnThrow(InputValue inputValue)
    {
        if(colldown < 0)
        {
            GameObject go = Lean.Pool.LeanPool.Spawn(fish ? fishProjectile : shurikenProjectile, projectilePoint.position, Quaternion.Euler(0,0,0));
            go.GetComponent<Projectile>().Initialize();

            colldown = fish ? 3 : 5;
        }

        PlayerManager.Instance.Select(this);

    }

    void Update()
    {
        if(UIManger.PAUSE)
        {
            targetVelocity = Vector2.zero;
            return;
        }

        targetVelocity = moveInput * moveSpeed;

        spriteR.flipX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rotPivot.rotation = Quaternion.Euler(0, 0, angle + 180);

        rotPivot.GetComponent<SortingGroup>().sortingOrder = angle > 50 || angle < -95 ? 0 : 1;

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
            UIManger.Instance.Press(UIManger.Button.Death,null);
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

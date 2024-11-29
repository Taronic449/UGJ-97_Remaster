using System.Linq;
using UnityEngine;
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
    public bool alive = true;
    public bool fish;
    public GameObject fishProjectile;
    public GameObject shurikenProjectile;
    public Transform projectilePoint;
    private Animator ani;
    public Animator swordAni;
    public Camera realCam;

    private float stun;
    private float colldown;
    public Transform rotPivot;

    private Camera cam;
    public byte indicator;
    public bool selected;
    public RuntimeAnimatorController yoriC,meiC;
    public Sprite yoriS,meiS;
    public Sprite yoriA,meiA;
    public PlayerType playerType;
    private AudioSource audioSource;

    [Header("Clips")]
    public AudioClip attack;
    public AudioClip powerUp;
    public AudioClip shuriken;
    public AudioClip[] stepClips;

    public enum PlayerType
    {
        yori,
        mei,
        none
    }


    [Header("Debug")]
    public Vector2 moveInput;

    public void SetPlayerType(PlayerType type)
    {
        GetComponentInChildren<Sword>().playerType = type;

        if(type == PlayerType.yori)
        {
            ani.runtimeAnimatorController = yoriC;
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = yoriS;
            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = yoriA;
        }
        else
        {
            ani.runtimeAnimatorController = meiC;
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = meiS;
            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = meiA;
        }

        playerType = type;

    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        spriteR = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();

        transform.position = new Vector2(10000,10000);

        PlayerManager.Instance.players.Add(this);


        DontDestroyOnLoad(gameObject);
    }

    public void Attack()
    {
        audioSource.PlayOneShot(attack);
        
    }

    public void Initialize()
    {
        // HealthBar.Instance.setHealth(health.health);
        
        cam = GameManger.Instance.cam;
        cam.GetComponent<CameraFollower>().target = transform;
    }


    void OnDestroy()
    {
        PlayerManager.Instance.players.Remove(this);
    }

void OnAim(InputValue inputValue)
{
    Vector2 inputVector = inputValue.Get<Vector2>();

    Vector3 aimPosition;

    if(realCam == null)
        return;

    if (IsUsingMouseInput())
    {
        // Mouse aiming
        aimPosition = realCam.ScreenToWorldPoint(inputVector);
    }
    else
    {
        // Gamepad aiming
        Vector3 stickDirection = new Vector3(inputVector.x, inputVector.y, 0);
        aimPosition = transform.position + stickDirection;
    }

    aimPosition.z = 0;

    Vector3 direction = (aimPosition - transform.position).normalized;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    rotPivot.rotation = Quaternion.Euler(0, 0, angle + 180);
    rotPivot.GetComponent<SortingGroup>().sortingOrder = angle > 50 || angle < -95 ? 0 : 1;

    spriteR.flipX = aimPosition.x > transform.position.x;
}

// Helper method to check input device type
private bool IsUsingMouseInput()
{
    PlayerInput playerInput = GetComponent<PlayerInput>();

    if (playerInput != null)
    {
        return playerInput.currentControlScheme == "Keyboard&Mouse";
    }

    Debug.LogWarning("PlayerInput component not found!");
    return false;
}



    void OnSelect(InputValue inputValue)
    {
         PlayerManager.Instance.Select(this);
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
        if(!alive)
            return;
        

        swordAni.Play("attac");
    }

    void OnThrow(InputValue inputValue)
    {
        if(!alive)
            return;

        if(colldown < 0)
        {
            GameObject go = Lean.Pool.LeanPool.Spawn(fish ? fishProjectile : shurikenProjectile, projectilePoint.position, Quaternion.Euler(0,0,0));
            go.GetComponent<Projectile>().Initialize(this, playerType);

            colldown = fish ? 1.5f : 2.5f;
        }

       audioSource.PlayOneShot(shuriken);

    }

    void Update()
    {
        if(UIManger.PAUSE)
        {
            targetVelocity = Vector2.zero;
            return;
        }

        targetVelocity = moveInput * moveSpeed;

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

    public void Step()
    {
        audioSource.PlayOneShot(stepClips[Random.Range(0,stepClips.Count())]);
    }

    //Damage
    public void hit()
    {
        if(!alive)
            return;

        GameManger.Instance.setHealth(playerType, health.health);

        ani.SetTrigger("hit");

    }
    public void Death()
    {
        if(alive)
        {
            alive = false;
            GetComponent<Health>().enabled = false;

            foreach (Enemy item in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
            {
                item.ResetPlayer();
            }

            GameManger.Instance.AddDeath();

            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            ani.SetBool("dead", true);
        }
        
    }

    //Effects
    public bool SetEffect(Effect effect)
    {
        audioSource.PlayOneShot(powerUp);

        switch (effect)
        {
            case Effect.RestoreHealth:
                GetComponent<Health>().heal(999);
                GameManger.Instance.setHealth(playerType, health.health);
            break;

            case Effect.KillNextEnemys:
                Invoke(nameof(deactivateFish), 10);
                fish = true;
                colldown = 0;
            break;

            case Effect.ProtectFromAttacks:
                GetComponent<Health>().activateShield();
            break;

            case Effect.KillAllEnemys:
                foreach (Enemy item in FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
                {
                    item.Death(null);
                }
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


using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private Animator ani;
    private SpriteRenderer sprite;
    public float timer;
    private NavMeshAgent agent;

    public Vector2 startoff;
    public bool reingage;
    public BrainType brainType;
    public float smartTimer = 15f;


    public enum BrainType
    {
        agressive,
        passive,
        smart,
        speed
    }

    void Start()
    {
        brainType = (BrainType)Random.Range(0,4);

        player = FindAnyObjectByType<PlayerController>().transform;
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();

        StartBrainEvent();
        
    }


    void FixedUpdate()
    {
        if(brainType == BrainType.smart)
        {
            if(smartTimer <= 0)
            {
               
                GetComponent<NavMeshAgent>().SetDestination(player.transform.position + (Vector3) startoff);
            }
            else
            {
                if(transform.position.x > player.transform.position.x)
                {
                    startoff = new Vector2(0.3f,0);
                }
                else
                {
                    startoff = new Vector2(-0.3f,0);
                }
                
                GetComponent<NavMeshAgent>().SetDestination(player.transform.position + (Vector3) startoff * 5);
            }
        }
        else
        {
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position + (Vector3) startoff);
        }

        sprite.flipX = player.position.x - transform.position.x > 0;

        if((player.position - transform.position).magnitude < 1.6f && !ani.GetBool("attack") && timer < 0)
        {
            timer = 3;
            ani.SetBool("attack", true);
            reingage = false;   
            BrainEvent();

            agent.isStopped = true;
        }

        if(!reingage && timer < 0)
        {
            reingage = true;

            BrainReturnEvent();
        }
        
        smartTimer -= Time.deltaTime;
        timer -= Time.deltaTime;
    }

    public void BrainReturnEvent()
    {
        switch (brainType)
        {
            case BrainType.agressive:
            return;

            case BrainType.passive:

                if(transform.position.x > player.transform.position.x)
                {
                    startoff = new Vector2(0.3f,0);

                }
                else
                {
                    startoff = new Vector2(-0.3f,0);
                }

            return;

            case BrainType.smart:

                if(transform.position.x > player.transform.position.x)
                {
                    startoff = new Vector2(0.3f,0);

                }
                else
                {
                    startoff = new Vector2(-0.3f,0);
                }

            return;

            case BrainType.speed:
                startoff = (transform.position - player.transform.position).normalized * 0.4f;
            return;

        }
    }

    public void BrainEvent()
    {
        switch (brainType)
        {
            case BrainType.agressive:
            return;

            case BrainType.passive:

                startoff = (transform.position - player.transform.position).normalized;

            return;

            case BrainType.smart:

                startoff = (transform.position - player.transform.position).normalized * 3;

            return;

            case BrainType.speed:
                timer = 1.5f;
                startoff = (transform.position - player.transform.position).normalized * 3;
            return;

        }
    }

    public void StartBrainEvent()
    {
        switch (brainType)
        {
            case BrainType.agressive:
                startoff = Random.insideUnitCircle * 1;
            return;

            case BrainType.passive:

                if(transform.position.x > player.transform.position.x)
                {
                    startoff = new Vector2(0.3f,0);

                }
                else
                {
                    startoff = new Vector2(-0.3f,0);
                }

            return;

            case BrainType.smart:

                GetComponent<NavMeshAgent>().speed = 1.8f;
                GetComponent<Animator>().speed = 1.4f;

            return;

            case BrainType.speed:
                GetComponent<NavMeshAgent>().speed = 1.5f;
                GetComponent<Animator>().speed = 1.5f;
            return;

        }
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
        agent.isStopped = false;
    }
}

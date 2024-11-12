
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
                
                GetComponent<NavMeshAgent>().SetDestination(player.transform.position + (Vector3) startoff * 20);

                if((player.position - transform.position).magnitude < 2f && !ani.GetBool("attack") && timer < 0)
                {
                    timer = 3;
                    ani.SetBool("attack", true);
                    reingage = false;   
                    BrainEvent();

                    agent.isStopped = true;
                }
            }
        }
        else
        {
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position + (Vector3) startoff);
        }

        sprite.flipX = player.position.x - transform.position.x > 0;

        if((player.position - transform.position).magnitude < 1.3f && !ani.GetBool("attack") && timer < 0)
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

                GetComponent<NavMeshAgent>().speed = 1.5f;
                GetComponent<NavMeshAgent>().avoidancePriority = 1;

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

                smartTimer = -1f;
                GetComponent<NavMeshAgent>().speed = 2.5f;
                GetComponent<NavMeshAgent>().avoidancePriority = 1;
                startoff = (transform.position - player.transform.position).normalized * 3;

            return;

            case BrainType.speed:
                startoff = (transform.position - player.transform.position).normalized * 4.5f;
            return;

        }
    }

    public void StartBrainEvent()
    {
        switch (brainType)
        {
            case BrainType.agressive:
                
                if(transform.position.x > player.transform.position.x)
                {
                    startoff = new Vector2(0.3f,0);

                }
                else
                {
                    startoff = new Vector2(-0.3f,0);
                }
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

                GetComponent<NavMeshAgent>().speed = 1.5f;
                GetComponent<NavMeshAgent>().avoidancePriority = 10;

                GetComponent<Animator>().speed = 1.3f;

            return;

            case BrainType.speed:
            
                if(transform.position.x > player.transform.position.x)
                {
                    startoff = new Vector2(0.3f,0);

                }
                else
                {
                    startoff = new Vector2(-0.3f,0);
                }

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

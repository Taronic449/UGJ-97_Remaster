using UnityEngine;
using UnityEngine.Events;
using static PlayerController;

public class Health : MonoBehaviour
{
    public ushort maxHealth;
    public int health;
    public UnityEvent<PlayerType?> deathEvent;
    public UnityEvent damageEvent;
    public GameObject damageObj;
    [SerializeField] private bool shield;
    public GameObject shieldT;

    void Awake()
    {
        health = maxHealth;

        if(shieldT != null)
            deactivateShield();
    }
    public void damage(ushort _damage, Vector2 knockback, bool player, PlayerType? playerType)
    {
        if(!shield)
        {
            health -= _damage;

            if(health <= 0)
            {
                deathEvent.Invoke(playerType);
            }

            damageEvent.Invoke();
            
            showNumber((Vector2) transform.position + new Vector2(Random.Range(0,0.6f) - 0.3f,GetComponent<SpriteRenderer>().bounds.size.y), -_damage);
        }       

        
    }

    public void showNumber(Vector2 pos, int _damage)
    {
        GameObject go = Lean.Pool.LeanPool.Spawn(damageObj,pos,Quaternion.Euler(0,0,0));
        go.GetComponent<DamageNumber>().Initialize(_damage);
    }
    public void heal(ushort amount)
    {
        Debug.Log("heal");
        showNumber((Vector2) transform.position + new Vector2(0,GetComponent<SpriteRenderer>().bounds.size.y), Mathf.Clamp(health + amount, 0,maxHealth) - health);
        health = Mathf.Clamp(health + amount, 0,maxHealth);
    }
    public void activateShield()
    {
        shield = true;
        Invoke(nameof(deactivateShield), 10);
        shieldT.SetActive(true);
    }
    public void deactivateShield()
    {
        shieldT.SetActive(false);
        shield = false;
    }
}

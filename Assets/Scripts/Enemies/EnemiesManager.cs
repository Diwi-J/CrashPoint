using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    protected float Health;
    protected float Damage;
    protected float MoveSpeed;


    protected float DetectionRange;
    protected float AttackRange;
    protected float AttackCooldown;

    protected Transform Player;

    public virtual void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        if (Player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }
    }

    protected bool PlayerInDetectionRange()
    {
        return Vector2.Distance(transform.position, Player.position) <= DetectionRange;
    }

    public void MoveTowardsPlayer()
    {
        if (Player == null) return;
        transform.position = Vector2.MoveTowards(transform.position, Player.position, MoveSpeed * Time.deltaTime);
    }
 
    public void TakeDamage(float DamageAmount)
    {
        Health -= DamageAmount;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void StopMoving()
    {
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

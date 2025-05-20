using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] protected float Health;
    [SerializeField] protected float Damage;
    [SerializeField] protected float MoveSpeed;


    [SerializeField] protected float DetectionRange;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected float AttackCooldown;

    protected Transform Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
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

    private void Die()
    {
        Destroy(gameObject);
    }
}

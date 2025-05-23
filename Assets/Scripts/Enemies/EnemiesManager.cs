using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected float Health;
    [SerializeField] protected float Damage;
    [SerializeField] protected float MoveSpeed;

    [Header("Combat")]
    [SerializeField] protected float DetectionRange = 10f;
    [SerializeField] protected float AttackRange = 2f;
    [SerializeField] protected float AttackCooldown = 1f;

    protected Transform Player;
    protected float NextAttackTime;

    protected virtual void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        if (Player == null)
            Debug.LogError("Player not found!");
    }

    protected bool PlayerInDetectionRange() =>
        Vector2.Distance(transform.position, Player.position) <= DetectionRange;

    public void MoveTowardsPlayer()
    {
        if (Player == null) return;
        transform.position = Vector2.MoveTowards( transform.position, Player.position, MoveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0) Die();
    }

    protected virtual void Die() => Destroy(gameObject);
    
}

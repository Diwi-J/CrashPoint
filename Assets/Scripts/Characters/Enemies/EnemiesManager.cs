using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected float Health;
    [SerializeField] protected float Damage;
    [SerializeField] protected float MoveSpeed;

    [Header("Combat")]
    [SerializeField] protected float DetectionRange;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected float AttackCooldown;

    protected Transform Player;
    protected float NextAttackTime;

    protected virtual void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        if (Player == null)
            Debug.LogError("Player not found!");
    }

    protected void RotateTowardsPlayer()
    {
        Vector2 direction = (Player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected bool PlayerInDetectionRange() =>
        Vector2.Distance(transform.position, Player.position) <= DetectionRange;

    public void MoveTowardsPlayer()
    {
        if (Player == null) return;
        transform.position = Vector2.MoveTowards(transform.position, Player.position, MoveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0) Die();

        Debug.Log("Enemy taking Damage");
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining HP: {Health}");
    }

    protected virtual void Die() => Destroy(gameObject);
    
}

using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 2f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 50;

    private float lastAttackTime;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (distance > attackRange)
            {
                MoveTowardPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
    }

    void MoveTowardPlayer()
    {
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("Enemy- PlayerHealth component NOT found" + player.name);
            }
            else
            {
                Debug.Log("Enemy attacks player");
                playerHealth.TakeDamage(attackDamage);
            }

            lastAttackTime = Time.time;
        }
    }

}

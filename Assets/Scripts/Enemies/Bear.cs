using UnityEngine;

public class Bear : EnemiesManager
{

    public void Awake()
    {
        Damage = 300f;
        Health = 2000f;
        MoveSpeed = 5f;

        DetectionRange = 10f;
        AttackRange = 1.2f;
        AttackCooldown = 2f;
    }

    void Update()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        if (PlayerInDetectionRange())
        {
            if (Vector2.Distance(transform.position, Player.position) <= AttackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                if (Time.time > AttackCooldown)
                {
                    AttackPlayer();
                }
            }
        }
    }

    void AttackPlayer()
    {
        PlayerManager.Instance.TakeDamage(Damage);
    }
}

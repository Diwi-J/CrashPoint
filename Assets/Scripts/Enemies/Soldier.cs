using UnityEngine;

public class Soldier : EnemiesManager
{
    public GameObject Bullet;
    public Transform FirePoint;

    float BulletSpeed = 10f;
    float FireDelay = 2f;
    float FireTime;

    public override void Awake()
    {
        base.Awake();

        Damage = 100f;
        Health = 1000f;
        MoveSpeed = 0f;

        DetectionRange = 10f;
        AttackRange = 10f;
        AttackCooldown = 2f;

        FireTime = Time.time + FireDelay;
    }

    void Update()
    {
        if (Player == null) return;

        RotateTowardsPlayer();

        if (PlayerInDetectionRange())
        {
            Aim();
        }

    }

    void Aim()
    {
        float DistanceToPlayer = Vector2.Distance(transform.position, Player.position);

        if (DistanceToPlayer <= AttackRange && Time.time >= FireTime)
        {
            Shoot();
            FireTime = Time.time + AttackCooldown;
        }
    }

    void Shoot()
    {
        if (FirePoint == null) return;

        Vector2 firePosition = FirePoint.position;
        Vector2 playerPosition = Player.position;

        GameObject bullet = Instantiate(Bullet, firePosition, Quaternion.identity);
        Bullets bulletScript = bullet.GetComponent<Bullets>();

        if (bulletScript != null)
        {
            Vector2 direction = (playerPosition - firePosition).normalized;
            bulletScript.Setup(direction, BulletSpeed, Damage);
        }
    }

    void RotateTowardsPlayer()
    {
        if (Player == null) return;

        Vector2 direction = (Player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
using UnityEngine;

public class Soldier : EnemiesManager
{
    [Header("Soldier Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 12f;

    protected override void Awake()
    {
        base.Awake();

        Health = 150f;
        Damage = 20f;
        MoveSpeed = 0f;

        DetectionRange = 15f;
        AttackRange = 10f;
        AttackCooldown = 1.8f;
    }

    void Update()
    {
        if (Player == null) return;

        RotateTowardsPlayer();

        if (PlayerInDetectionRange() && Vector2.Distance(transform.position, Player.position) <= AttackRange &&
            Time.time >= NextAttackTime)
        {
            Shoot();
            NextAttackTime = Time.time + AttackCooldown;
        }
    }

    void Shoot()
    {
        Vector2 firePosition = firePoint.position;
        Vector2 direction = ((Vector2)Player.position - firePosition).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePosition, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(direction, bulletSpeed, Damage);
    }
}
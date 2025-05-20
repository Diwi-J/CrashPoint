using UnityEngine;

public class Soldier : EnemiesManager
{
    public GameObject Bullet;
    public Transform FirePoint;

    public void Awake()
    {
        Damage = 100f;
        Health = 1000f;
        MoveSpeed = 3f;

        DetectionRange = 200f;
        AttackRange = 7f;
        AttackCooldown = 2f;
    }

    public void Behaviour()
    {
        if (PlayerInDetectionRange())
        {
            if (Vector2.Distance(transform.position, Player.position) <= AttackRange)
            {
                Vector2 direction = (Player.position - transform.position).normalized;
                if (Time.time > AttackCooldown)
                {
                    Shoot(direction);
                }
            }
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject bulletInstance = Instantiate(Bullet, FirePoint.position, Quaternion.identity);
        bulletInstance.GetComponent<Bullets>().Setup(direction, Damage);
    }
}
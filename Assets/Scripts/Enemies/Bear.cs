using UnityEngine;

public class Bear : EnemiesManager
{
    [Header("Bear Settings")]
    [SerializeField] private float lungeSpeed = 8f;
    [SerializeField] private float lungeDuration = 0.4f;

    private bool isLunging;
    private float lungeTimer;

    private Vector2 lungeDirection;
    private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
                
        Health = 300f;
        Damage = 40f;
        MoveSpeed = 4f;
        AttackRange = 1.8f;
    }

    void Update()
    {
        if (Player == null) return;

        RotateTowardsPlayer();

        if (!isLunging && PlayerInDetectionRange())
            Behavior();
    }

    void FixedUpdate()
    {
        if (isLunging)
            HandleLunge();
    }

    void Behavior()
    {
        float distance = Vector2.Distance(transform.position, Player.position);

        if (distance > AttackRange)
        {
            MoveTowardsPlayer();
        }
        else if (Time.time >= NextAttackTime)
        {
            StartLunge();
            NextAttackTime = Time.time + AttackCooldown;
        }
    }

    void StartLunge()
    {
        isLunging = true;
        lungeTimer = lungeDuration;
        lungeDirection = (Player.position - transform.position).normalized;
    }

    void HandleLunge()
    {
        lungeTimer -= Time.fixedDeltaTime;

        if (lungeTimer > 0)
        {
            rb.linearVelocity = lungeDirection * lungeSpeed;
        }
        else
        {
            isLunging = false;
            rb.linearVelocity = Vector2.zero;

            if (Vector2.Distance(transform.position, Player.position) <= AttackRange * 1.2f)
                PlayerManager.Instance.TakeDamage(Damage);
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 direction = (Player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
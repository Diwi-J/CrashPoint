using UnityEngine;
using UnityEngine.UIElements;

public class Bear : EnemiesManager
{
    [SerializeField] float JumpSpeed = 10f;
    [SerializeField] float JumpDuration = 0.5f;

    bool IsJumping = false;
    float JumpTime;
    Vector2 JumpTarget;

    Rigidbody2D rb;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();

        Health = 1000f;
        Damage = 300f;
        MoveSpeed = 4f;

        DetectionRange = 7f;
        AttackRange = 2f;
        AttackCooldown =2f;
    }

    void Update()
    {
        if (Player == null) return;

        RotateTowardsPlayer();

        if (!IsJumping)
        {
            Behaviour();
        }
 
    }

    private void FixedUpdate()
    {
        if (IsJumping)
        {
            JumpTime -= Time.fixedDeltaTime;

            if (JumpTime > 0f)
            {
                rb.linearVelocity = JumpTarget * JumpSpeed;
            }
            else
            {
                IsJumping = false;
                rb.linearVelocity = Vector2.zero;

                if (Vector2.Distance(transform.position, Player.position) <= AttackRange * 1.2f)
                {
                    PlayerManager.Instance.TakeDamage(Damage);
                }
            }
        }   
    }


    public void Behaviour()
    {
        if (PlayerInDetectionRange())
        {
            if (Vector2.Distance(transform.position, Player.position) > AttackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                StopMoving();
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        if (!IsJumping && Time.time > AttackCooldown)
        {
            JumpTarget = (Player.position - transform.position).normalized;
            JumpTime = JumpDuration;
            IsJumping = true;
            AttackCooldown = Time.time + AttackCooldown;
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
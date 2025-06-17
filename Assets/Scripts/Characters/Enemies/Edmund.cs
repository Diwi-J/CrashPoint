using UnityEngine;

public class Edmund : EnemiesManager
{
    [Header("Boss Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;

    [Header("Charge Attack")]
    [SerializeField] float chargeCooldown = 10f;
    [SerializeField] float chargeDuration = 0.3f;
    [SerializeField] float chargeUpTime = 0.5f;

    private float nextChargeTime = 0f;
    private bool IsStabbing = false;

    private Vector2 chargeTarget;
    
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Health = 1000f;
        Damage = 30f;
        MoveSpeed = 0f;

        DetectionRange = 15f;
        AttackRange = 12f;
        AttackCooldown = 0.5f;
    }

    void Update()
    {
        if (Player == null) return;

        if (!IsStabbing)
        {
            RotateTowardsPlayer();
            
            if (PlayerInDetectionRange() && Time.time >= NextAttackTime)
            {
            Shoot();
            NextAttackTime = Time.time + AttackCooldown;
            }
        }


        if (Time.time >= nextChargeTime)
        {
            StartCoroutine(ChargeAttack());
            nextChargeTime = Time.time + chargeCooldown;
        }
    }

    void Shoot()
    {
        Vector2 firePosition = firePoint.position;
        Vector2 direction = ((Vector2)Player.position - firePosition).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePosition, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(direction, bulletSpeed, Damage);
    }

    System.Collections.IEnumerator ChargeAttack()
    {
        IsStabbing = true;
        animator.SetBool("IsStabbing", true);

        yield return new WaitForSeconds(0.4f);

        chargeTarget = Player.position;

        float elapsed = 0f;
        float flashTime = 0f;
        Vector2 startPos = transform.position;
        
        while (flashTime < chargeUpTime)
        {
            flashTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time * 6f, 1));
            yield return null;
        }

        spriteRenderer.color = Color.white;

        while (elapsed < chargeDuration)
        {
            transform.position = Vector2.Lerp(startPos, chargeTarget, elapsed / chargeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = chargeTarget;

        animator.SetBool("IsStabbing", false);
        IsStabbing = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsStabbing && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>()?.TakeDamage(Damage * 3);
        }
    }
}


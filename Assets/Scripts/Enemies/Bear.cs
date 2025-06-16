using UnityEngine;

public class Bear : EnemiesManager
{
    [Header("Bear Settings")]
    [SerializeField] private float chargeUpTime = 0.4f;
    [SerializeField] private float chargeDuration = 0.3f;
    [SerializeField] private float chargeCooldown = 2f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public PlayerManager playerManager;

    private bool IsCharging = false;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
                
        Health = 300f;
        Damage = 40f;
        MoveSpeed = 3f;
        DetectionRange = 10f;
        AttackRange = 4f;
        AttackCooldown = chargeCooldown;
    }

    private void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Player == null) return;

        if (!IsCharging)
        {
            RotateTowardsPlayer();
        }
        
        float distance = Vector2.Distance(transform.position, Player.position);

        if (distance <= DetectionRange && !IsCharging)
        {
            if (distance > AttackRange)
            {
                MoveTowardsPlayer();
            }
            else if (Time.time >= NextAttackTime)
            {
                StartCoroutine(ChargeAtPlayer());
                NextAttackTime = Time.time + AttackCooldown;
            }
        }
    }

    private System.Collections.IEnumerator ChargeAtPlayer()
    {
        IsCharging = true;

        Vector2 start = transform.position;
        Vector2 target = Player.position;

        float elapsed = 0f;
        float flashTime = 0f;
        
        while (flashTime < chargeUpTime)
        {
            flashTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time * 6f, 1));
            yield return null;
        }

        spriteRenderer.color = Color.white;

        while (elapsed < chargeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / chargeDuration;
            transform.position = Vector2.Lerp(start, target, t);

            yield return null;
        } 

        IsCharging = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsCharging)
        {
            playerManager.TakeDamage(Damage);
            Debug.Log("Bear hit player!");
        }
    }
}
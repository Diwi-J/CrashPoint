using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float HitDamage = 100f;
    public float ShootDamage = 30f;
    public float bulletSpeed = 12f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform WeaponPoint;
    [SerializeField] private PlayerController playercontorller;

    Animator animator;
    public GameObject AttackPoint;

    bool Hitting = false;
    bool IsArmed => playercontorller.IsArmed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playercontorller = GetComponent<PlayerController>();
    }

    void Start()
    {
        AttackPoint = GameObject.Find("AttackPoint");
        AttackPoint.SetActive(false);

        if (AttackPoint == null)
            Debug.LogError("AttackPoint GameObject not found!");

        WeaponPoint = GameObject.Find("WeaponPoint").transform;
    }

    void Update()
    {
        Hit();
        Shoot();
    }
    
    void Hit()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Hitting)
        {
            animator.SetTrigger("IsHitting");
            Hitting = true;

            StartCoroutine(EnableAttackPointTemporarily());
        }
    }
    
    void EndHit()
    {
        Hitting = false;
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsArmed)
        {
                        
            Vector2 ShootPosition = WeaponPoint.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - ShootPosition).normalized;

            GameObject bullet = Instantiate(bulletPrefab, ShootPosition, Quaternion.identity);
            bullet.GetComponent<Bullet>().Initialize(direction, bulletSpeed, ShootDamage);
        }
    }

    IEnumerator EnableAttackPointTemporarily()
    {
        AttackPoint.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        AttackPoint.SetActive(false);
    }
}

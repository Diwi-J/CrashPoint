using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction;
    private float speed;
    private float damage;

    public PlayerManager playerManager;
    public EnemiesManager enemiesManager;

    private void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        //enemiesManager = GameObject.Find("Managers").GetComponent<EnemiesManager>();
    }

    public void Initialize(Vector2 dir, float spd, float dmg)
    {
        direction = dir;
        speed = spd;
        damage = dmg;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        float time = 0.0f;
        time += Time.deltaTime;
        if(time >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bullet hit the player!");

            playerManager.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit an enemy!");
            EnemiesManager enemy = other.GetComponent<EnemiesManager>();
                      
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

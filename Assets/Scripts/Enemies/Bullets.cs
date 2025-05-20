using UnityEngine;

public class Bullets : Soldier
{
    public float LifeTime = 5f;

    Vector2 Direction;
   
    float Speed = 10f;

    public void Setup(Vector2 direction, float speed, float damage)
    {
        Direction = direction;
        Speed = speed;
        Damage = damage;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Destroy(gameObject, LifeTime);
    }

    void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy") && !other.isTrigger) // Ignore enemies and triggers
        {
            Destroy(gameObject);
        }
    }
}

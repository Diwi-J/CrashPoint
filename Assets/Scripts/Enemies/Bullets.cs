using UnityEngine;

public class Bullets : Soldier
{
    public float speed = 10f;

    Vector2 Direction;

    public void Setup(Vector2 dir, float dmg)
    {
        Direction = dir;
        Damage = dmg;
        Destroy(gameObject, 5f);
    }

    public override void Update()
    {
        transform.Translate(Direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.Instance.TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

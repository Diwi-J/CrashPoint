using UnityEngine;

public abstract class EnemiesManager : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] protected float Health;
    [SerializeField] protected float Damage;
    [SerializeField] protected float MoveSpeed;


    [SerializeField] protected float DetectionRange;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected float AttackCooldown;

    protected Transform Player;

    protected virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected bool PlayerInDetectionRange()
    {
        return Vector2.Distance(transform.position, Player.position) <= DetectionRange;
    }

    public abstract void Behaviour();
    // Update is called once per frame
    public virtual void  Update()
    {
        Behaviour();
    }
}

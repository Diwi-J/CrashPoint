using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    
    public float AttckCooldown = 1f;
    private float LastAttcktime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= LastAttcktime + AttckCooldown)
        {
            LastAttcktime = Time.time;
            Attack();
        }
    }
    
    void Attack()
    {
        animator.SetTrigger("Attack");
    }
}

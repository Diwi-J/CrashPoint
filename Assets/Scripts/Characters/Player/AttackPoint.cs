using UnityEngine;
using System;

public class AttackPoint : MonoBehaviour
{
    PlayerCombat playerCombat;
    float hitdamage;

    private void Awake()
    {
        playerCombat = GetComponentInParent<PlayerCombat>();

    }

    private void Start()
    {
        hitdamage = playerCombat.HitDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit!");

            EnemiesManager enemy = other.GetComponent<EnemiesManager>();
            if (enemy != null)
            {
                enemy.TakeDamage(hitdamage);
            }
        }
    }
}
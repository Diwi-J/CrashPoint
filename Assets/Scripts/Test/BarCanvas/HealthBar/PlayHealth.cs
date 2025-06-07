using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1000;
    private int currentHealth;

    public HealthBar2 healthBarUI;  // References the UI script

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Applying damage: {damage}");

        Debug.Log($"Player health: {currentHealth}");

        if (healthBarUI != null)
            healthBarUI.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }



    void Die()
    {
        Debug.Log("Player died!");
        
    }
}

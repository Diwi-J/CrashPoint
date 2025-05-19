using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth;
    public int MaxHunger = 1000;
    public int currentHunger;
    public int MaxHydration = 1000;
    public int currentHydration;
    public int MaxSleep = 1000;
    public int currentSleep;
    public int MaxInsanity = 2000;
    public int currentInsanity;


    public HealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentHunger = MaxHunger;
       

        currentHydration = MaxHydration;
        currentSleep = MaxSleep;
        currentInsanity = MaxInsanity;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player is dead");
        }
        else
        {
            Debug.Log("Player took damage, current health: " + currentHealth);
        }
    }
}

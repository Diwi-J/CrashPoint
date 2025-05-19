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
    public HungerBar hungerBar; // Added a reference to HungerBar
    public HydrationBar hydrationBar; // Assuming similar references for other bars
    public SleepBar sleepBar;
    public InsanityBar insanityBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentHunger = MaxHunger;
        hungerBar.SetMaxHunger(MaxHunger); // Fixed by using the instance reference

        currentHydration = MaxHydration;
        hydrationBar.SetMaxHydration(MaxHydration);

        currentSleep = MaxSleep;
        sleepBar.SetMaxSleep(MaxSleep);

        currentInsanity = MaxInsanity;
        insanityBar.SetMaxInsanity(MaxInsanity);
    }

    void Update() //Testing to see that health bar is working. It is.
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

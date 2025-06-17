using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    #region Fields
    [Header("Player Stats")]
    [SerializeField] float MaxHealth    = 1000f;
    [SerializeField] float MaxHunger    = 1000f;
    [SerializeField] float MaxHydration = 1000f;
    [SerializeField] float MaxSleep     = 1000f;
    [SerializeField] float MaxInsanity  = 2000f;

    public float Health;
    public float Hunger;
    public float Hydration;
    public float Sleep;
    public float Insanity;

    [Space]
    [Header("Drain Rates (per second)")]
    public float HungerDrainRate    = 1f;
    public float HydrationDrainRate = 1.5f;
    public float SleepDrainRate     = 0.5f;
    public float InsanityGainRate   = 0.1f;

    [Space]
    [Header("Insanity Increases")]
    [SerializeField] float InsanityFromHunger   = 0.3f;
    [SerializeField] float InsanityFromThirst   = 0.2f;
    [SerializeField] float InsanityFromSleep    = 0.1f;
    [SerializeField] float InsanityFromDamage   = 5f;
    [SerializeField] float InsanityFromDeath    = 10f;

    [Space]
    [Header("Damage When Depleted")]
    [SerializeField] float HealthDamageWhenHungry           = 5f;
    [SerializeField] float HealthDamageWhenThirsty          = 7f;
    [SerializeField] float HealthDamageWhenSleepDeprived    = 2f;

    [Space]
    [Header("Lives")]
    public int Lives = 5;

    [Space]
    [Header("Respawn")]
    public Vector2 RespawnPosition;

    [Space]
    [Header("Post Processing")]

    [SerializeField] Volume volume;
    [SerializeField] float flashDuration = 0.5f;
    [SerializeField] float maxIntensity = 0.4f;

    Coroutine vignetteRoutine;
    Vignette vignette;
    ChromaticAberration chromaticAberration;
    #endregion Fields

    void Start()
    {

        Health      = MaxHealth;
        Hunger      = MaxHunger;
        Hydration   = MaxHydration;
        Sleep       = MaxSleep;
        Insanity    = 0f;

        RespawnPosition = transform.position;

        volume = GameObject.Find("PostProcessingVolume").GetComponent<Volume>();

        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0f;
        }

        if (volume.profile.TryGet(out chromaticAberration))
        {
            chromaticAberration.intensity.value = 0f;
        }
    }

    void Update()
    {
        DrainStats();
        UpdateInsanityEffects();
    }

    void DrainStats()
    {
        Hunger      -= HungerDrainRate * Time.deltaTime;
        Hydration   -= HydrationDrainRate * Time.deltaTime;
        Sleep       -= SleepDrainRate * Time.deltaTime;

        Hunger      = Mathf.Clamp(Hunger, 0, MaxHunger);
        Hydration   = Mathf.Clamp(Hydration, 0, MaxHydration);
        Sleep       = Mathf.Clamp(Sleep, 0, MaxSleep);
        Insanity    = Mathf.Clamp(Insanity, 0, MaxInsanity);
        Health      = Mathf.Clamp(Health, 0, MaxHealth);

        if (Hunger <= 250f)
        {
            Insanity += InsanityFromHunger * Time.deltaTime;
        }
        if (Hydration <= 250f)
        {
            Insanity += InsanityFromThirst * Time.deltaTime;
        }
        if (Sleep <= 250f)
        {
            Insanity += InsanityFromSleep * Time.deltaTime;
        }
        
        if (Hunger <= 0)
        {
            Health -= HealthDamageWhenHungry * Time.deltaTime;
        }
        if (Hydration <= 0)
        {
            Health -= HealthDamageWhenThirsty * Time.deltaTime;
        }
        if (Sleep <= 0)
        {
            Health -= HealthDamageWhenSleepDeprived * Time.deltaTime;
        }

        if (Health <= 0)
        {
            LoseLife();
        }
 
        //Debug.Log($"Health: {Health} | Hunger: {Hunger} | Hydration: {Hydration} | Sleep: {Sleep} | Insanity: {Insanity} | Lives: {Lives}");
    }

    void LoseLife()
    {
        Lives--;

        if (Lives <= 0)
        {
            Debug.Log("GAME OVER");
        }
        else
        {
            Debug.Log("Player died. Respawning...");
            Respawn();
        }
    }
    void Respawn()
    {
        transform.position = RespawnPosition;

        Health      = MaxHealth;
        Hunger      = MaxHunger;
        Hydration   = MaxHydration;
        Sleep       = MaxSleep;
        Insanity    += InsanityFromDeath;


        Debug.Log("Player respawned.");
    }
    public void TakeDamage(float damage)
    {
        Health      -= damage;
        Insanity    += InsanityFromDamage;

        Debug.Log($"Player took {damage} damage. Remaining Health: {Health}");
        FlashRedVignette();
    }

    #region Post Processing Methods
    public void FlashRedVignette()
    {
        if (vignette != null)
        {
            if (vignetteRoutine != null)
                StopCoroutine(vignetteRoutine);
            vignetteRoutine = StartCoroutine(FlashVignette());
        }
        else
        {
            Debug.Log(FlashVignette() + " - Vignette not found in volume profile.");
        }
    }

    System.Collections.IEnumerator FlashVignette()
    {
        float elapsed = 0f;

        while (elapsed < flashDuration / 2)
        {
            elapsed += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(0f, maxIntensity, elapsed / (flashDuration / 2));
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < flashDuration / 2)
        {
            elapsed += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(maxIntensity, 0f, elapsed / (flashDuration / 2));
            yield return null;
        }

        vignette.intensity.value = 0f;

        Debug.Log("Vignette flash complete.");
    }

    void UpdateInsanityEffects()
    {
        if (chromaticAberration != null)
        {
            float insanityNormalized = Mathf.Clamp01(Insanity / MaxInsanity);
            chromaticAberration.intensity.value = insanityNormalized;
        }
        //Debug.Log("insanity effects updated. Current Insanity: " + Insanity);
    }
    #endregion 
}

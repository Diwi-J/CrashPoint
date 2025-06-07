using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HydrationSystem : MonoBehaviour
{
    [Header("Hydration Settings")]
    public float maxHydration = 100f;
    public float depletionRate = 5f;
    public float lowHydrationThreshold = 20f;
    public float flashSpeed = 5f;

    [Header("UI Elements")]
    public Slider hydrationSlider;
    public Image fillImage;

    [Header("Color Settings")]
    public Color highColor = new Color(0f, 10f, 255f); //blue
    public Color mediumColor = new Color(51f, 64f, 176f); //light blue
    public Color lowColor = new Color(7f, 24f, 72f); //dark blue

    [Header("Respawn & Death")]
    public Transform respawnPoint;
    public GameObject playerObject; // reference to the player object
    public GameObject deathEffect;  // optional: particle or animation
    private bool isDead = false;

    private float currentHydration;
    private bool isInShelter = false;
    private bool isFlashing = false;

    void Start()
    {
        currentHydration = maxHydration;
        hydrationSlider.maxValue = maxHydration;
        hydrationSlider.value = currentHydration;

        if (fillImage != null)
            fillImage.color = highColor;

        if (playerObject == null)
            playerObject = this.gameObject;
    }

    void Update()
    {
        if (isDead) return;

        if (!isInShelter)
        {
            currentHydration -= depletionRate * Time.deltaTime;
            currentHydration = Mathf.Clamp(currentHydration, 0, maxHydration);
        }

        hydrationSlider.value = currentHydration;
        UpdateSliderColor();

        if (currentHydration <= lowHydrationThreshold && !isFlashing)
        {
            isFlashing = true;
            StartCoroutine(FlashSlider());
        }

        if (currentHydration <= 0 && !isDead)
        {
            HandleDeath();
        }
    }

    void UpdateSliderColor()
    //Changes slider colour depending on the hydration level
    {
        if (fillImage == null) return;

        if (currentHydration > 50f)
            fillImage.color = highColor;
        else if (currentHydration > lowHydrationThreshold)
            fillImage.color = mediumColor;
        else
            fillImage.color = lowColor;
    }

    IEnumerator FlashSlider()
    // Flashes the slider when hydration is low
    {
        while (currentHydration <= lowHydrationThreshold)
        {
            if (fillImage != null)
                fillImage.enabled = !fillImage.enabled;

            yield return new WaitForSeconds(1f / flashSpeed);
        }

        if (fillImage != null)
            fillImage.enabled = true;

        isFlashing = false;
    }

    public void RefillHydration(float amount)
    // Refills hydration by a specified amount
    {
        if (isDead) return;

        currentHydration += amount;
        currentHydration = Mathf.Clamp(currentHydration, 0, maxHydration);
    }

    public void SetShelterState(bool inShelter)
    // Sets whether the player is in a shelter, affecting hydration depletion
    {
        isInShelter = inShelter;
    }

    void HandleDeath()
    // Handles player death due to dehydration
    {
        isDead = true;
        Debug.Log("Player has died of dehydration.");

        // Optional: Play death effect
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Disable player (movement, controls, etc.)
        if (playerObject != null)
            playerObject.SetActive(false);

        // Optionally respawn after delay
        StartCoroutine(RespawnAfterDelay(2f));
    }

    IEnumerator RespawnAfterDelay(float delay)
    // Respawns the player after a delay at the specified respawn point
    {
        yield return new WaitForSeconds(delay);

        if (respawnPoint != null)
        {
            playerObject.transform.position = respawnPoint.position;
            playerObject.SetActive(true);
            currentHydration = maxHydration;
            hydrationSlider.value = currentHydration;
            isDead = false;

            // Reset velocity
            Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;
        }
    }
}

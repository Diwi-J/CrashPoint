using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HungerBar : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxHunger = 1000f;
    public float depletionRate = 10f;
    public float lowHungerThreshold = 20f;
    public float flashSpeed = 5f;

    [Header("UI Elements")]
    public Slider hungerSlider;
    public Image fillImage;

    [Header("Color Settings")]
    public Color highColor = new Color(255f, 143f, 0f); //orange
    public Color mediumColor = new Color(204f, 152f, 66f); //light orange
    public Color lowColor = new Color(147f, 70f, 0f); //dark orange

    [Header("Respawn & Death")]
    public Transform respawnPoint;
    public GameObject playerObject; // reference to the player object
    public GameObject deathEffect;  //particle or animation
    private bool isDead = false;

    private float currentHunger;
    private bool isInShelter = false;
    private bool isFlashing = false;

    void Start()
    {
        currentHunger = maxHunger;
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;

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
            currentHunger -= depletionRate * Time.deltaTime;
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        }

        hungerSlider.value = currentHunger;
        UpdateSliderColor();

        if (currentHunger <= lowHungerThreshold && !isFlashing)
        {
            isFlashing = true;
            StartCoroutine(FlashSlider());
        }

        if (currentHunger <= 0 && !isDead)
        {
            HandleDeath();
        }
    }

    void UpdateSliderColor()
    //Changes the colour of the slider depending on the hunger level
    {
        if (fillImage == null) return;

        if (currentHunger > 500f)
            fillImage.color = highColor;
        else if (currentHunger > lowHungerThreshold)
            fillImage.color = mediumColor;
        else
            fillImage.color = lowColor;
    }

    IEnumerator FlashSlider()
    //Slider flashes when hunger is low
    {
        while (currentHunger <= lowHungerThreshold)
        {
            if (fillImage != null)
                fillImage.enabled = !fillImage.enabled;

            yield return new WaitForSeconds(1f / flashSpeed);
        }

        if (fillImage != null)
            fillImage.enabled = true;

        isFlashing = false;
    }

    public void RefillHunger(float amount)
    //Refills the hunger bar by a certain amount
    {
        if (isDead) return;

        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }

    public void SetShelterState(bool inShelter)
        //Chechs if the player is in the shelter or not
    {
        isInShelter = inShelter;
    }

    void HandleDeath()
    //Checks if the player has died of starvation.
    {
        isDead = true;
        Debug.Log("Player has died of starvation.");

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
    //Respawns the player after a delay
    {
        yield return new WaitForSeconds(delay);

        if (respawnPoint != null)
        {
            playerObject.transform.position = respawnPoint.position;
            playerObject.SetActive(true);
            currentHunger = maxHunger;
            hungerSlider.value = currentHunger;
            isDead = false;

            // Reset velocity
            Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;
        }
    }
}
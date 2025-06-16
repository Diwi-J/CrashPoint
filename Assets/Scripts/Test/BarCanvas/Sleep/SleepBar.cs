using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SleepSystem : MonoBehaviour
{
    [Header("Sleep Settings")]
    public float maxSleep = 1000f;
    public float depletionRate = 3f;
    public float lowSleepThreshold = 200f;
    public float flashSpeed = 5f;

    [Header("UI Elements")]
    public Slider sleepSlider;
    public Image fillImage;

    [Header("Color Settings")]
    public Color restedColor = new Color(44f, 11f, 80f);// dark purple
    public Color tiredColor = new Color(83f, 20f, 155f);// purple
    public Color exhaustedColor = new Color(134f, 94f, 134f); // light purple

    [Header("Respawn & Death")]
    public Transform respawnPoint;
    public GameObject playerObject;
    public GameObject sleepEffect;
    private bool isAsleep = false;

    private float currentSleep;
    private bool isResting = false;
    private bool isFlashing = false;

    void Start()
    {
        currentSleep = maxSleep;
        sleepSlider.maxValue = maxSleep;
        sleepSlider.value = currentSleep;

        if (fillImage != null)
            fillImage.color = restedColor;

        if (playerObject == null)
            playerObject = this.gameObject;
    }

    void Update()
    {
        if (isAsleep || sleepSlider == null) return;

        if (!isResting)
        {
            currentSleep -= depletionRate * Time.deltaTime;
            currentSleep = Mathf.Clamp(currentSleep, 0, maxSleep);
            sleepSlider.value = currentSleep;
        }

        UpdateSliderColor();

        if (currentSleep <= lowSleepThreshold && !isFlashing)
        {
            isFlashing = true;
            StartCoroutine(FlashSlider());
        }

        if (currentSleep <= 0 && !isAsleep)
        {
            FallAsleep();
        }
    }

    void UpdateSliderColor()
    //Slider changes colour based on the sleep level
    {
        if (fillImage == null) return;

        if (currentSleep > 500f)
            fillImage.color = restedColor;
        else if (currentSleep > lowSleepThreshold)
            fillImage.color = tiredColor;
        else
            fillImage.color = exhaustedColor;
    }

    IEnumerator FlashSlider()
    //Slider flashes when the sleep level is low
    {
        while (currentSleep <= lowSleepThreshold)
        {
            if (fillImage != null)
                fillImage.enabled = !fillImage.enabled;

            yield return new WaitForSeconds(1f / flashSpeed);
        }

        if (fillImage != null)
            fillImage.enabled = true;

        isFlashing = false;
    }

    public void RefillSleep(float amount)
    // Adds sleep to the current sleep level
    {
        if (isAsleep) return;

        currentSleep += amount;
        currentSleep = Mathf.Clamp(currentSleep, 0, maxSleep);
    }

    public void SetRestingState(bool resting)
    // Sets the resting state of the player
    {
        isResting = resting;
    }

    void FallAsleep()
    // Player falls asleep when sleep level reaches 0
    {
        isAsleep = true;
        Debug.Log("Player fell asleep from exhaustion.");

        if (sleepEffect != null)
            Instantiate(sleepEffect, transform.position, Quaternion.identity);

        if (playerObject != null)
            playerObject.SetActive(false);

        StartCoroutine(RespawnAfterDelay(3f));
    }

    IEnumerator RespawnAfterDelay(float delay)
    // Respawns the player after a delay at the respawn point
    {
        yield return new WaitForSeconds(delay);

        if (respawnPoint != null)
        {
            playerObject.transform.position = respawnPoint.position;
            playerObject.SetActive(true);
            currentSleep = maxSleep;
            sleepSlider.value = currentSleep;
            isAsleep = false;

            Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InsanitySystem : MonoBehaviour
{
    [Header("Insanity Settings")]
    public float maxInsanity = 100f;
    public float baseIncreaseRate = 2f;
    public float darknessMultiplier = 2f;
    public float lightRecoveryRate = 3f;
    public float highInsanityThreshold = 80f;

    [Header("UI Elements")]
    public Slider insanitySlider;
    public Image fillImage;

    [Header("Color Settings")]
    public Color saneColor = new Color(170f, 170f, 170f);//grey
    public Color mediumColor = new Color(149f, 149f, 149f); //light grey
    public Color insaneColor = new Color(89f, 89f, 89f); //dark grey

    [Header("Insanity Trigger")]
    public GameObject playerObject;
    public GameObject insanityEffect;

    private float currentInsanity = 0f;
    private bool isInDarkness = false;
    private bool isInLight = false;
    private bool isInsane = false;

    void Start()
    {
        insanitySlider.minValue = 0f;
        insanitySlider.maxValue = maxInsanity;
        insanitySlider.value = currentInsanity;

        if (fillImage != null)
            fillImage.color = saneColor;

        if (playerObject == null)
            playerObject = this.gameObject;
    }

    void Update()
    {
        if (isInsane) return;

        float rate = baseIncreaseRate;

        if (isInDarkness)
            rate *= darknessMultiplier;

        if (isInLight)
        {
            currentInsanity -= lightRecoveryRate * Time.deltaTime;
        }
        else
        {
            currentInsanity += rate * Time.deltaTime;
        }

        currentInsanity = Mathf.Clamp(currentInsanity, 0, maxInsanity);
        insanitySlider.value = currentInsanity;
        UpdateSliderColor();

        if (currentInsanity >= maxInsanity && !isInsane)
        {
            GoInsane();
        }
    }

    void UpdateSliderColor()
    //Slider colour changes depending on the insanity level
    {
        if (fillImage == null) return;

        if (currentInsanity < 40f)
            fillImage.color = saneColor;
        else if (currentInsanity < highInsanityThreshold)
            fillImage.color = mediumColor;
        else
            fillImage.color = insaneColor;
    }

    void GoInsane()
    //Triggers the insanity effect
    {
        isInsane = true;
        Debug.Log("Player has gone insane!");

        if (insanityEffect != null)
            Instantiate(insanityEffect, transform.position, Quaternion.identity);

        if (playerObject != null)
            playerObject.SetActive(false);

    }

    public void ReduceInsanity(float amount)
    //Reduces the insanity level by a specified amount
    {
        if (isInsane) return;

        currentInsanity -= amount;
        currentInsanity = Mathf.Clamp(currentInsanity, 0, maxInsanity);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    //Checks if the player enters a dark or light zone
    {
        if (other.CompareTag("DarkZone"))
            isInDarkness = true;
        if (other.CompareTag("LightZone"))
            isInLight = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    //Checks if the player exits a dark or light zone
    {
        if (other.CompareTag("DarkZone"))
            isInDarkness = false;
        if (other.CompareTag("LightZone"))
            isInLight = false;
    }*/
}

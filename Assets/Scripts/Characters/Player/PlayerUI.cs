using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Stat Sliders")]
    public Slider healthSlider;
    public Slider hungerSlider;
    public Slider hydrationSlider;
    public Slider sleepSlider;
    public Slider insanitySlider;

    PlayerManager player;

    void Start()
    {
        player = PlayerManager.Instance;

        // Set max values once at start
        healthSlider.maxValue = 1000f;
        hungerSlider.maxValue = 1000f;
        hydrationSlider.maxValue = 1000f;
        sleepSlider.maxValue = 1000f;
        insanitySlider.maxValue = 2000f;
    }

    void Update()
    {
        if (player != null)
        {
            healthSlider.value = player.Health;
            hungerSlider.value = player.Hunger;
            hydrationSlider.value = player.Hydration;
            sleepSlider.value = player.Sleep;
            insanitySlider.value = player.Insanity;
        }
    }
}

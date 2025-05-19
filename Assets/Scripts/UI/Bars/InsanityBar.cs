using UnityEngine;
using UnityEngine.UI;

public class InsanityBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxInsanity(int insanity)
    {
        slider.maxValue = insanity;
        slider.value = insanity;
    }

    public void SetInsanity(int insanity)
    {
        slider.value = insanity;
    }
}
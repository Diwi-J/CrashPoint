using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHydration(int hydrate)
    {
        slider.maxValue = hydrate;
        slider.value = hydrate;
    }

    public void SetHydration(int hydrate)
    {
        slider.value = hydrate;
    }
}

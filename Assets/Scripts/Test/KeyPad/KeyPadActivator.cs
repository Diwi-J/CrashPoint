using UnityEngine;

public class KeypadActivator : MonoBehaviour
{
    public GameObject button;

    void OnMouseDown()
    // This method is called when the button is clicked
    {
        if (button != null)
        {
            button.SetActive(true);
            Time.timeScale = 0; // Optional: pause game
            Debug.Log("Keypad opened");
        }
    }
}


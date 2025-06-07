using UnityEngine;

public class Keypad : MonoBehaviour
{
    public Door door;                  //Reference to the door to unlock
    public string correctCode = "1234";  //Unlock code to unlock the door

    private string inputCode = "";

    public void EnterDigit(string digit)
    //Method is called when a digit button is pressed on the keypad
    {
        inputCode += digit;
        Debug.Log("Entered: " + inputCode);

        if (inputCode.Length >= correctCode.Length)
        {
            CheckCode();
        }
    }

    private void CheckCode()
    //Method checks if the entered code matches the correct code
    {
        if (inputCode == correctCode)
        {
            door.UnlockDoor();
            Debug.Log("Correct code entered!");
        }
        else
        {
            Debug.Log("Wrong code. Try again.");
        }

        inputCode = "";  //Resets after checking
    }
   
    public void ClearInput()
    //Method clears the input code
    {
        inputCode = "";
        Debug.Log("Input cleared");
    }

    public void OpenKeypad()
    //Method to open the keypad UI
    {
        gameObject.SetActive(true);
        Time.timeScale = 0; // Optional: pause the game
        Debug.Log("Keypad opened");
    }
    public void CloseKeypad()
    //Method to close the keypad UI
    {
        gameObject.SetActive(false); //Use "X" button to deactivate the keypad
        Time.timeScale = 1; //Unpause
        Debug.Log("Keypad closed");
    }

}

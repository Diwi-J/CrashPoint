using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMPro.TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject nextButton;
    public float wordSpeed;
    public bool playerIsClose;

    void Update()
    {
        // This function checks if the player is close to the NPC and if the 'T' key is pressed to trigger dialogue
        if (Input.GetKeyDown(KeyCode.T) && playerIsClose)
        {
            // If the dialogue panel is not active, activate it and start typing the first line of dialogue
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else
            {
                if (index < dialogue.Length - 1)
                {
                    NextLine();
                }
                else
                {
                    dialoguePanel.SetActive(false);
                    zeroText();
                }
            }
        }

        // This function checks if the current dialogue text matches the current index of the dialogue array
        if (dialogueText.text == dialogue[index])
        {
            nextButton.SetActive(true);
        }
    }


    public void zeroText()
    {
        // This function resets the dialogue text and index when the dialogue ends or the player exits the NPC's proximity
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        // This function types out the dialogue letter by letter
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        // This function is called when the next button is pressed
        nextButton.SetActive(false);

        // Check if there are more lines of dialogue to display
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Detects how close the player is to the NPC in order to bring up dialogue box
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        //Detects when the player leaves the NPC's proximity
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
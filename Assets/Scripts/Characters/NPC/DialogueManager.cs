using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private float wordSpeed = 0.05f;

    private string[] currentDialogue;
    private int index = 0;
    private bool isTyping = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("DialogueManager already exists, destroying this one: " + gameObject.name);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("DialogueManager set: " + gameObject.name);
            Instance = this;
        }
    }


    public void StartDialogue(string[] dialogueLines)
    {
        if (dialoguePanel.activeInHierarchy || isTyping) return;

        currentDialogue = dialogueLines;
        index = 0;
        dialoguePanel.SetActive(true);
        dialogueText.text = "";
        StartCoroutine(TypeLine());
    }

    public void NextLine()
    {
        if (isTyping) return;

        index++;
        if (index < currentDialogue.Length)
        {
            dialogueText.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char letter in currentDialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isTyping = false;
        nextButton.SetActive(true);
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        nextButton.SetActive(false);
    }
}

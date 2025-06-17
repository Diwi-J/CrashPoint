using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public GameObject player;

    private int step = 0;
    private PlayerController playerController;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        ShowStep(0);
    }

    void Update()
    {
        switch (step)
        {
            case 0:
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    NextStep();
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    NextStep();
                }
                break;
        }
    }

    void ShowStep(int stepNum)
    {
        switch (stepNum)
        {
            case 0:
                tutorialText.text = "Use W/S or A/D to Move";
                break;
            case 1:
                tutorialText.text = "Press W/S/A/D + Shift to run";
                break;
            case 2:
                tutorialText.text = "Press Ctrl to crouch";
                break;
            case 3:
                tutorialText.text = "Press T to interact with NPC's";
                break;
            case 4:
                tutorialText.text = "Press E to open your inventory";
                break;
            case 5:
                tutorialText.text = "Press F to shoot/attack";
                break;

        }
    }

    void NextStep()
    {
        step++;
        ShowStep(step);
    }
}


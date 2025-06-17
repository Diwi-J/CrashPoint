using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = true;
    public Animator animator;  
    public GameObject Keypad;
    public GameObject uiCanvas;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Keypad.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    public void UnlockDoor()
        //Method unlocks door
    {
        isLocked = false;
        Debug.Log("Door unlocked!");
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}

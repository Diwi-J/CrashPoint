using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = true;
    public Animator animator;  // Optional: animate door opening

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
            // Simple alternative: disable collider and hide door
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

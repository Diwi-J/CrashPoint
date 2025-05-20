using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Check if the player entered
        {
            SceneManager.LoadScene(2); // Load the scene
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Renamed field to resolve ambiguity  

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pauses the game  
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resumes the game  
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Ensure time is normal before restarting  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        Time.timeScale = 1f; // Ensure time is normal before going home  
        SceneManager.LoadScene("MainMenu");
    }
}

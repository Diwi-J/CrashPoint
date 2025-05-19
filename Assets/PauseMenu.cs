using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;

    public void Pause()
    {
        PauseMenu.SetActive(true);
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

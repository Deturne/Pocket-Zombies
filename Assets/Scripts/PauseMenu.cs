using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (isPaused && Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
            isPaused = false;
        }

    }
    public void Pause()
    {
        
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
       
        
    }

    public void Resume()
    {
       
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    

    public GameObject Menu()
    {
        return pauseMenu;
    }
}

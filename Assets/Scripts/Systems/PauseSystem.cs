using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool isPaused;

    private void Awake()
    {
        Resume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public bool IsPaused()
    {
        return isPaused;
    }





}

using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject canvas;

    private void Awake()
    {
        isPaused = false;
    }

    public void ShowMenu()
    {
        canvas.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void HideMenu()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void ToggleMenu()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        canvas.SetActive(isPaused);
    }

    public void Retry()
    {
        GameOverHandler.RetryLastLevelStatic();
    }

    public void MainMenu()
    {
        GameOverHandler.LoadScene("MainMenu");
    }
}
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public static string lastLevel;
    public string nextLevel;
    public bool isFinal = false;

    public void OnDeath()
    {
        LoadScene("DeathScene");
    }

    public void OnVictory()
    {
        if (isFinal || nextLevel == "")
        {
            OnFinalVictory();
        }
        else
        {
            lastLevel = nextLevel;
            LoadScene(nextLevel);
        }
    }

    public static void OnFinalVictory()
    {
        LoadScene("WinScene");
    }

    public void RetryLastLevel()
    {
        LoadScene(lastLevel);
    }

    public void LoadBonusLevel()
    {
        lastLevel = "World98";
        LoadScene("World98");
    }
    
    public static void RetryLastLevelStatic()
    {
        LoadScene(lastLevel);
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
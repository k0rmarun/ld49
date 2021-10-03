using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public string nextLevel;
    public bool isFinal = false;

    public void OnDeath()
    {
        LoadScene("DeathScene");
    }

    public void OnVictory()
    {
        if (isFinal)
        {
            OnFinalVictory();
        }
        else
        {
            LoadScene(nextLevel);
        }
    }

    public static void OnFinalVictory()
    {
        LoadScene("WinScene");
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
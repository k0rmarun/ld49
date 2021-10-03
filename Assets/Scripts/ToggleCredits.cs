using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCredits : MonoBehaviour
{
    public GameObject howToPlay;
    public GameObject credits;
    public Button button;

    private bool showCredits = false;
    public void toggleCredits()
    {
        showCredits = !showCredits;
        howToPlay.SetActive(!showCredits);
        credits.SetActive(showCredits);

        button.GetComponentInChildren<Text>().text = showCredits ? "How to play" : "Credits";
    }
}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public float goalReached = Single.PositiveInfinity;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Goal trigger" + other.name);
        if (other.name == "Player")
        {
            goalReached = 1;
            Debug.Log("Victoria");
            RandomizedSound.Play(other.transform, RandomizedSound.NEXT_LEVEL, true);
            other.GetComponent<PlayerMovement>().freezePlayer();
        }
    }

    private void Update()
    {
        goalReached -= Time.deltaTime;

        if (goalReached < 0)
        {
            GameObject.FindWithTag("Player").GetComponent<GameOverHandler>().OnVictory();
        }
    }
}
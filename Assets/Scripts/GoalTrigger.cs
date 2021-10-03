using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            GameObject.FindWithTag("Player").GetComponent<GameOverHandler>().OnVictory();
        }
    }
}

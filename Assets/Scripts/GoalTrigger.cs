using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Goal trigger" + other.name);
        if (other.name == "Player")
        {
            Debug.Log("Victoria");
            GameObject.FindWithTag("Player").GetComponent<GameOverHandler>().OnVictory();
        }
    }
}
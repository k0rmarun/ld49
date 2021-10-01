using UnityEngine;

public class DecayAnimation : MonoBehaviour
{
    public float timeAlive = 5;

    void Update()
    {
        transform.Rotate(Vector3.up, 360 * Time.deltaTime);
        timeAlive -= Time.deltaTime;

        if (timeAlive < 0)
        {
            Destroy(gameObject);
        }
    }
}
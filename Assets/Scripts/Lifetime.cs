using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float originalTime = 10F;
    private float timer;

    void Update()
    {
        if (timer >= originalTime)
        {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
    }
}
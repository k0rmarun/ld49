using System;
using UnityEngine;

public class DecayAnimation : MonoBehaviour
{
    public float timeAlive = 5;
    public float maxTravelUp = 0.1f;
    public float maxTravelDown = 0.2f;
    private Vector3 initialPosition;
    private Vector3 velocity;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        timeAlive -= Time.deltaTime;

        if (timeAlive > 0 && timeAlive < 5)
        {
            var x = 5 - timeAlive;
            var y = -Mathf.Sin(x * x);
            y *= Mathf.Lerp(0, y >= 0 ? maxTravelUp : maxTravelDown, x / 5);
            transform.position = initialPosition + Vector3.up * y;
        }

        if (timeAlive < 0)
        {
            transform.position -= velocity;
            velocity += 0.098f * Vector3.up;
        }

        if (timeAlive < -10)
        {
            Destroy(gameObject);
        }
    }
}
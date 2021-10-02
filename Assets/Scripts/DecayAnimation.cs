using System;
using UnityEngine;

public class DecayAnimation : MonoBehaviour
{
    public float timeAlive = 5;
    private Vector3 velocity;

    private void Start()
    {
        var component = GetComponent<Animator>();
        if (component)
        {
            component.StartPlayback();
        }
    }

    void Update()
    {
        timeAlive -= Time.deltaTime;

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
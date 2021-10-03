using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public Camera camera;

    void Update()
    {
        Vector3 lookAt = transform.position;
        lookAt -= (camera.transform.position - lookAt);
        // Debug.DrawLine(transform.position, lookAt, Color.red);
        lookAt.y = transform.position.y;
        // Debug.DrawLine(transform.position, lookAt, Color.green);
        transform.LookAt(lookAt);
        // transform.Rotate(0, 180, 0);
    }
}
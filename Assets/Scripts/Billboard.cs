using UnityEngine;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    public Camera camera;
    public bool autoDetectCamera = true;
    
    void Update()
    {
        if (autoDetectCamera && !camera)
        {
            camera = FindObjectOfType<Camera>();
        }

        Vector3 lookAt = transform.position;
        lookAt -= (camera.transform.position - lookAt);
        // Debug.DrawLine(transform.position, lookAt, Color.red);
        lookAt.y = transform.position.y;
        // Debug.DrawLine(transform.position, lookAt, Color.green);
        transform.LookAt(lookAt);
        // transform.Rotate(0, 180, 0);
    }
}
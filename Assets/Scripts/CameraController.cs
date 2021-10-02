using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position + new Vector3(1, 1, -1) * 10;
        transform.LookAt(player.transform);
    }
}
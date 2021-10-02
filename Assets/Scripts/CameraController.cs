using UnityEngine;

public class CameraController : MonoBehaviour
{
   
    public float lerpValue = 2.25f;
    private GameObject player;
    private Vector3 target_Offset;
     
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position + new Vector3(1, 1, -1) * 10;
        target_Offset = transform.position - player.transform.position;
        transform.LookAt(player.transform);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position+target_Offset, lerpValue * Time.deltaTime);
    }
}
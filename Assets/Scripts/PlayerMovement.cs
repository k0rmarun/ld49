using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int rotation = 0;

    void Update()
    {
        Vector3 nextPosition = transform.position;
        bool moveAttempt = false;
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotation = -90;
            nextPosition += Vector3.left;
            moveAttempt = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            rotation = 0;
            nextPosition += Vector3.forward;
            moveAttempt = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rotation = 90;
            nextPosition += Vector3.right;
            moveAttempt = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rotation = 180;
            nextPosition += Vector3.back;
            moveAttempt = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //TODO: Build
        }

        if (moveAttempt)
        {
            transform.rotation = Quaternion.AngleAxis(90 * rotation, Vector3.up);
            if (DecayManager.canWalkOn(nextPosition))
            {
                transform.position = nextPosition;
            }
            
        }

        if (!DecayManager.canWalkOn(transform.position))
        {
            Debug.LogError("Game Over");
            //GameOver
            transform.position += Vector3.down;
        }
    }
}
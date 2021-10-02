using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int rotation = 0;
    public Vector3 lookDirection = Vector3.zero;
    public GameObject inventory;

    void Update()
    {
        bool moveAttempt = false;
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotation = -90;
            lookDirection = Vector3.left;
            moveAttempt = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            rotation = 0;
            lookDirection = Vector3.forward;
            moveAttempt = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rotation = 90;
            lookDirection = Vector3.right;
            moveAttempt = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rotation = 180;
            lookDirection = Vector3.back;
            moveAttempt = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 lookAt = transform.position + lookDirection + Vector3.down;
            if (inventory)
            {
                if (DecayManager.canPlace(lookAt))
                {
                    inventory.transform.SetParent(null);
                    inventory.transform.position = lookAt;
                    DecayManager.addDecayableBlock(inventory);
                    inventory = null;
                }
            }
        }

        if (moveAttempt)
        {
            Vector3 nextPosition = transform.position + lookDirection;
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
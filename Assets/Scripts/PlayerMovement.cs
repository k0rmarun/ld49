using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int rotation = 0;
    public Vector3 lookDirection = Vector3.zero;
    public Pickupable inventory;
    public GameObject buildIndicator;

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

        Vector3 interactPosition = transform.position + lookDirection;
        Vector3 buildPosition = interactPosition + Vector3.down;
        UpdateCursor(buildPosition, interactPosition);

        Vector3 standOnPosition = transform.position + Vector3.down;
        Vector3 walkOnPosition = standOnPosition + lookDirection;

        if (moveAttempt)
        {
            transform.GetChild(0).rotation = Quaternion.AngleAxis(rotation, Vector3.up);
            if (DecayManager.canWalkOn(walkOnPosition))
            {
                transform.position = walkOnPosition + Vector3.up;
            }
        }

        var walkOverDecay = DecayManager.getWalkOverDecay(standOnPosition);
        if (walkOverDecay)
        {
            DecayManager.adjustDecay(standOnPosition, walkOverDecay.remainingLifeTime);
        }

        if (!DecayManager.canWalkOn(standOnPosition))
        {
            //GameOver
            transform.position += 0.3f * Vector3.down;
        }
    }

    private void UpdateCursor(Vector3 buildPosition, Vector3 interactPosition)
    {
        buildIndicator.transform.position = buildPosition + Vector3.up * 0.2f;
        bool canDoSomething = false;

        SetCursorColor(Color.red);
        if (inventory)
        {
            if (DecayManager.canPlace(buildPosition))
            {
                SetCursorColor(Color.green);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject gameObject = inventory.OnPlace(buildPosition);
                    DecayManager.addDecayableBlock(gameObject);
                    inventory = null;
                }
            }
            else if (DecayManager.canDrop(interactPosition))
            {
                SetCursorColor(Color.white);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject gameObject = inventory.OnDrop(interactPosition);
                    DecayManager.addDecayableBlock(gameObject);
                    inventory = null;
                }
            }
        }
        else
        {
            SetCursorColor(Color.white);
            Pickupable pickupable = DecayManager.getPickupable(interactPosition);
            if (pickupable)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    inventory = pickupable;
                    DecayManager.removeDecayableBlock(interactPosition);
                    pickupable.OnPickup();
                }
            }
        }
    }

    private void SetCursorColor(Color color)
    {
        buildIndicator.GetComponent<Renderer>().material.color = color;
    }
}
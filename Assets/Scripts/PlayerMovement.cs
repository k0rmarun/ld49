using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int prevRotation = 0;
    public int rotation = 0;
    public Vector3 lookDirection = Vector3.zero;
    public Pickupable inventory;
    public GameObject buildIndicator;

    public float movementLock = 0;

    void Update()
    {
        movementLock -= Time.deltaTime;
        bool moveAttempt = false;
        if (Input.GetButtonDown("Horizontal") || (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.3 && movementLock < 0))
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                rotation = -90;
                lookDirection = Vector3.left;
                moveAttempt = true;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                rotation = 90;
                lookDirection = Vector3.right;
                moveAttempt = true;
            }
        }
        else if (Input.GetButtonDown("Vertical") || (Mathf.Abs(Input.GetAxis("Vertical")) > 0.3 && movementLock < 0))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                rotation = 0;
                lookDirection = Vector3.forward;
                moveAttempt = true;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                rotation = 180;
                lookDirection = Vector3.back;
                moveAttempt = true;
            }
        }

        Vector3 interactPosition = transform.position + lookDirection;
        Vector3 buildPosition = interactPosition + Vector3.down;
        UpdateCursor(buildPosition, interactPosition);

        Vector3 standOnPosition = transform.position + Vector3.down;
        Vector3 walkOnPosition = standOnPosition + lookDirection;

        if (moveAttempt)
        {
            transform.GetChild(0).rotation = Quaternion.AngleAxis(rotation, Vector3.up);
            if (rotation == prevRotation)
            {
                if (DecayManager.canWalkOn(walkOnPosition))
                {
                    transform.position = walkOnPosition + Vector3.up;
                }
            }

            movementLock = 0.3f;
            prevRotation = rotation;
        }

        var walkOverDecay = DecayManager.getWalkOverDecay(standOnPosition);
        if (walkOverDecay)
        {
            DecayManager.adjustDecay(standOnPosition, walkOverDecay.remainingLifeTime);
        }

        if (!DecayManager.canWalkOn(standOnPosition))
        {
            transform.position += 0.3f * Vector3.down;
            if (transform.position.y < -20)
            {
                GameObject.FindWithTag("Player").GetComponent<GameOverHandler>().OnDeath();
            }
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
                if (Input.GetButtonDown("Fire1"))
                {
                    inventory.OnPlace(buildPosition);
                    inventory = null;
                }
            }
            else if (DecayManager.canDrop(interactPosition))
            {
                SetCursorColor(Color.white);
                if (Input.GetButtonDown("Fire1"))
                {
                    inventory.OnDrop(interactPosition);
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
                if (Input.GetButtonDown("Fire1"))
                {
                    inventory = pickupable.OnPickup(interactPosition);
                }
            }
        }
    }

    private void SetCursorColor(Color color)
    {
        buildIndicator.GetComponent<Renderer>().material.color = color;
    }
}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int prevRotation = 0;
    public int rotation = 0;
    public Vector3 lookDirection = Vector3.zero;
    public Pickupable inventory;
    public GameObject buildIndicator;

    public bool isFalling;
    public float movementLock = 0;
    private bool frozen;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Camera.main.GetComponent<PauseMenu>().ToggleMenu();
        }

        if (PauseMenu.isPaused)
        {
            return;
        }

        if (frozen)
        {
            return;
        }

        movementLock -= Time.deltaTime;
        Vector3 standOnPosition = Vector3.down;
        if (!isFalling)
        {
            bool moveAttempt = false;
            if (Input.GetButtonDown("Horizontal") || (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.6 && movementLock < 0))
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
            else if (Input.GetButtonDown("Vertical") ||
                     (Mathf.Abs(Input.GetAxis("Vertical")) > 0.6 && movementLock < 0))
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

            standOnPosition = transform.position + Vector3.down;
            Vector3 walkOnPosition = standOnPosition + lookDirection;

            if (moveAttempt)
            {
                RandomizedSound.Play(transform, RandomizedSound.MOVEMENT);
                transform.GetChild(0).rotation = Quaternion.AngleAxis(rotation, Vector3.up);
                if (rotation == prevRotation)
                {
                    if (DecayManager.canWalkOn(walkOnPosition))
                    {
                        transform.position = walkOnPosition + Vector3.up;
                    }

                    movementLock = 0.3f;
                }
                else
                {
                    movementLock = 0.1f;
                }

                prevRotation = rotation;
            }

            var walkOverDecay = DecayManager.getWalkOverDecay(standOnPosition);
            if (walkOverDecay)
            {
                walkOverDecay.OnWalkOver();
                DecayManager.adjustDecay(standOnPosition, walkOverDecay.remainingLifeTime);
            }
        }

        if (isFalling || !DecayManager.canWalkOn(standOnPosition))
        {
            if (!isFalling)
            {
                RandomizedSound.Play(transform, RandomizedSound.DIE);
            }

            transform.position += 0.3f * Vector3.down;
            if (transform.position.y < -20)
            {
                GameObject.FindWithTag("Player").GetComponent<GameOverHandler>().OnDeath();
            }

            isFalling = true;
        }
    }

    private void UpdateCursor(Vector3 buildPosition, Vector3 interactPosition)
    {
        buildIndicator.transform.position = buildPosition + Vector3.up * 0.2f;
        SetCursorColor(Color.red);
        if (inventory)
        {
            if (DecayManager.canPlace(buildPosition) && inventory.canPlace())
            {
                SetCursorColor(Color.green);
                if (Input.GetButtonDown("Fire1"))
                {
                    RandomizedSound.Play(transform, RandomizedSound.PLACE);
                    inventory.OnPlace(buildPosition);
                    inventory = null;
                }
            }
            else if (DecayManager.canDrop(interactPosition) && inventory.canDrop())
            {
                SetCursorColor(inventory.dropColor);
                if (Input.GetButtonDown("Fire1"))
                {
                    RandomizedSound.Play(transform, RandomizedSound.DROP);
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
                    RandomizedSound.Play(transform, RandomizedSound.DROP);
                    inventory = pickupable.OnPickup(interactPosition);
                }
            }
        }
    }

    private void SetCursorColor(Color color)
    {
        buildIndicator.GetComponent<Renderer>().material.color = color;
    }

    public void freezePlayer()
    {
        frozen = true;
    }
}
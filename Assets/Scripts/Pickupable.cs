using Actions;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public GameObject pickedPrefab;
    public GameObject placedPrefab;
    public GameObject droppedPrefab;

    public Color dropColor = Color.white;

    public Pickupable OnPickup(Vector3 pickupPosition)
    {
        if (pickedPrefab == null)
        {
            return null;
        }
        DecayManager.removeDecayableBlock(pickupPosition);
        var hidden = GameObject.Find("Inventory");
        GameObject newObject = Instantiate(pickedPrefab, hidden.transform);
        newObject.transform.localPosition = Vector3.up;
        newObject.transform.localRotation = Quaternion.identity;
        
        foreach (var action in GetComponents<IPickupAction>())
            action.Pickup(pickupPosition, newObject);
        
        Destroy(gameObject);
        
        return newObject.GetComponent<Pickupable>();
    }

    public void OnPlace(Vector3 placePosition)
    {
        if (placedPrefab == null)
        {
            return;
        }
        GameObject newObject = Instantiate(placedPrefab, placePosition, Quaternion.identity);
        
        foreach (var action in GetComponents<IPlaceAction>())
            action.Place(placePosition, newObject);
        
        Destroy(gameObject);
        DecayManager.addDecayableBlock(newObject);
    }

    public void OnDrop(Vector3 dropPosition)
    {
        if (droppedPrefab == null)
        {
            return;
        }
        GameObject newObject = Instantiate(droppedPrefab, dropPosition, Quaternion.identity);
        
        foreach (var action in GetComponents<IDropAction>())
            action.Drop(dropPosition, newObject);
        
        Destroy(gameObject);
        DecayManager.addDecayableBlock(newObject);
    }

    public bool canPlace()
    {
        return placedPrefab != null;
    }

    public bool canDrop()
    {
        return droppedPrefab != null;
    }
}
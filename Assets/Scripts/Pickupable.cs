using Actions;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public GameObject pickedPrefab;
    public GameObject placedPrefab;
    public GameObject droppedPrefab;

    public Pickupable OnPickup(Vector3 pickupPosition)
    {
        DecayManager.removeDecayableBlock(pickupPosition);
        var hidden = GameObject.Find("Inventory");
        GameObject newObject = Instantiate(pickedPrefab, hidden.transform);
        newObject.transform.localPosition = Vector3.up;
        newObject.transform.localRotation = Quaternion.identity;
        
        foreach (var action in GetComponents<IPickupAction>())
            action.Pickup(pickupPosition);
        
        Destroy(gameObject);
        
        return newObject.GetComponent<Pickupable>();
    }

    public void OnPlace(Vector3 placePosition)
    {
        GameObject newObject = Instantiate(placedPrefab, placePosition, Quaternion.identity);
        
        foreach (var action in GetComponents<IPlaceAction>())
            action.Place(placePosition);
        
        Destroy(gameObject);
        DecayManager.addDecayableBlock(newObject);
    }

    public void OnDrop(Vector3 dropPosition)
    {
        GameObject newObject = Instantiate(droppedPrefab, dropPosition, Quaternion.identity);
        
        foreach (var action in GetComponents<IDropAction>())
            action.Drop(dropPosition);
        
        Destroy(gameObject);
        DecayManager.addDecayableBlock(newObject);
    }
}
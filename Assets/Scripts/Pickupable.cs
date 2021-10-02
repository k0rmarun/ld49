using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public GameObject pickedPrefab;
    public GameObject placedPrefab;
    public GameObject droppedPrefab;

    public Pickupable OnPickup()
    {
        var hidden = GameObject.Find("Inventory");
        GameObject newObject = Instantiate(pickedPrefab, hidden.transform);
        newObject.transform.localPosition = Vector3.up;
        newObject.transform.localRotation = Quaternion.identity;
        Destroy(gameObject);
        return newObject.GetComponent<Pickupable>();
        // Destroy(gameObject);
        // transform.SetParent(hidden.transform, false);
        // transform.localPosition = Vector3.up;
        // transform.localRotation = Quaternion.identity;
    }

    public GameObject OnPlace(Vector3 buildPosition)
    {
        GameObject newObject = Instantiate(placedPrefab, buildPosition, Quaternion.identity);
        Destroy(gameObject);
        return newObject;
    }

    public GameObject OnDrop(Vector3 buildPosition)
    {
        GameObject newObject = Instantiate(droppedPrefab, buildPosition, Quaternion.identity);
        Destroy(gameObject);
        return newObject;
    }
}
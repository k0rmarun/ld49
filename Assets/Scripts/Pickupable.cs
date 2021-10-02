using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public GameObject placedPrefab;
    public GameObject droppedPrefab;

    public void OnPickup()
    {
        var hidden = GameObject.Find("Inventory");
        transform.SetParent(hidden.transform, false);
        transform.localPosition = Vector3.up;
        transform.localRotation = Quaternion.identity;
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
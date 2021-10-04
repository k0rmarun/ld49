using UnityEngine;

public class WalkOverDecay : MonoBehaviour
{
    public float remainingLifeTime = 5;
    public Material material;

    public void OnWalkOver()
    {
        GetComponentInChildren<MeshRenderer>().material = material;
    }
}

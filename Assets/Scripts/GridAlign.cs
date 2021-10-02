using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridAlign : MonoBehaviour
{
    private Vector3 _lastLocation;

    // Start is called before the first frame update
    public void Awake()
    {
        Reposition();

        if (Application.isPlaying)
        {
            enabled = false;
        }
    }

    public void Update() => Reposition();

    private void Reposition()
    {
        if (transform.position.Equals(_lastLocation)) return;

        transform.position = new Vector3((int) transform.position.x, (int) transform.position.y,
            (int) transform.position.z);
        _lastLocation = transform.position;
    }
}
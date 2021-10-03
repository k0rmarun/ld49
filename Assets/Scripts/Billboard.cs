using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public Camera camera;
    
    void Update()
    {
         transform.LookAt(camera.transform);
         transform.Rotate(0, 180,0);
    }
}

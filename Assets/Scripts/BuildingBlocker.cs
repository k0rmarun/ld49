using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlocker : MonoBehaviour
{
    public void Awake()
    {
        var meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.enabled = false;
    }
}

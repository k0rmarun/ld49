using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintMarkerFader : MonoBehaviour
{
    void Update()
    {
        var color = GetComponent<SpriteRenderer>().color;
        color.a = 0.4f + Mathf.PingPong(Time.time / 3f, 0.2f);
        GetComponent<SpriteRenderer>().color = color;
    }
}
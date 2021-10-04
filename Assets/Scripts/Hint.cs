using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Hint : MonoBehaviour
{
    public HintAnimation hintAnimation;

    private HintIconSwitcher switcher;
    // Start is called before the first frame update
    void Start()
    {
        switcher = gameObject.GetComponentInChildren<HintIconSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switcher && hintAnimation != switcher.hintAnimation)
        {
            switcher.hintAnimation = hintAnimation;
        }
    }
}

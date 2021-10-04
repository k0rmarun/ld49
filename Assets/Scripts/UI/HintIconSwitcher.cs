using System;
using UnityEngine;

public class HintIconSwitcher : MonoBehaviour
{
    public HintAnimation hintAnimation;
    public int currentIcon;
    public float remainingTime;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime < 0 && hintAnimation)
        {
            currentIcon = (currentIcon + 1) % hintAnimation.icons.Length;
            HintIconData data = hintAnimation.icons[currentIcon];
            spriteRenderer.sprite = data.sprite;
            remainingTime = data.duration;
        }
    }
}
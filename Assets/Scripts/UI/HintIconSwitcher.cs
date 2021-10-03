using UnityEngine;

public class HintIconSwitcher : MonoBehaviour
{
    public HintIconData[] icons;
    public int currentIcon;
    public float remainingTime;

    private void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime < 0)
        {
            currentIcon = (currentIcon + 1) % icons.Length;
            HintIconData data = icons[currentIcon];
            GetComponent<SpriteRenderer>().sprite = data.sprite;
            remainingTime = data.duration;
        }
    }
}
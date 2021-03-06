using UnityEngine;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}

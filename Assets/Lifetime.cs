using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float Time = 10F;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= Time)
        {
            Destroy(gameObject);
        }

        timer += UnityEngine.Time.deltaTime;
    }
}
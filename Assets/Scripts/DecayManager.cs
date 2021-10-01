using UnityEngine;

public class DecayManager : MonoBehaviour
{
    public const int MAX_WORLD_SIZE_X = 100;
    public const int MAX_WORLD_SIZE_Y = 10;
    public const int MAX_WORLD_SIZE_Z = 100;

    public static float[,,] remainingBlockLive;

    // Start is called before the first frame update
    void Start()
    {
        remainingBlockLive = new float[MAX_WORLD_SIZE_X, MAX_WORLD_SIZE_Y, MAX_WORLD_SIZE_Z];

        for (int x = 0; x < MAX_WORLD_SIZE_X; x++)
        {
            for (int y = 0; y < MAX_WORLD_SIZE_Y; y++)
            {
                for (int z = 0; z < MAX_WORLD_SIZE_Z; z++)
                {
                    GameObject ground = GameObject.Find("G-" + x + "-" + y + "-" + z);

                    if (ground)
                    {
                        var decayInitializer = ground.GetComponent<DecayInitializer>();
                        if (decayInitializer)
                        {
                            remainingBlockLive[x, y, z] = decayInitializer.decayTime;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < MAX_WORLD_SIZE_X; x++)
        {
            for (int y = 0; y < MAX_WORLD_SIZE_Y; y++)
            {
                for (int z = 0; z < MAX_WORLD_SIZE_Z; z++)
                {
                    remainingBlockLive[x, y, z] -= Time.deltaTime;

                    if (remainingBlockLive[x, y, z] > 0 && remainingBlockLive[x, y, z] < 5)
                    {
                        GameObject ground = GameObject.Find("G-" + x + "-" + y + "-" + z);
                        var decayAnimation = ground.GetComponent<DecayAnimation>();
                        if (!decayAnimation)
                        {
                            ground.AddComponent<DecayAnimation>();
                        }
                    }
                }
            }
        }
    }
}
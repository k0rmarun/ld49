using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayManager : MonoBehaviour
{
    public const int MAX_WORLD_SIZE_X = 100;
    public const int MAX_WORLD_SIZE_Y = 100;
    public const int MAX_WORLD_SIZE_Z = 10;

    public static float[,,] remainingBlockLive;

    // Start is called before the first frame update
    void Start()
    {
        remainingBlockLive = new float[MAX_WORLD_SIZE_X, MAX_WORLD_SIZE_Y, MAX_WORLD_SIZE_Z];

        for (int x = 0; x < MAX_WORLD_SIZE_X; x++)
        {
            for (int y = 0; y < MAX_WORLD_SIZE_Z; y++)
            {
                for (int z = 0; y < MAX_WORLD_SIZE_Z; y++)
                {
                    GameObject ground = GameObject.Find("G-" + x + "-" + y + "-" + z);

                    if (ground)
                    {
                        remainingBlockLive[x, y, z] = ground.GetComponent<DecayInitializer>().decayTime;
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
            for (int y = 0; y < MAX_WORLD_SIZE_Z; y++)
            {
                for (int z = 0; y < MAX_WORLD_SIZE_Z; y++)
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
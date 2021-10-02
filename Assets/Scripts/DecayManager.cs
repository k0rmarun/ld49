using UnityEngine;

public class DecayManager : MonoBehaviour
{
    public const int MAX_WORLD_SIZE_X = 100;
    public const int MAX_WORLD_SIZE_Y = 10;
    public const int MAX_WORLD_SIZE_Z = 100;

    public static bool[,,] hasDecayableBlock = new bool[MAX_WORLD_SIZE_X, MAX_WORLD_SIZE_Y, MAX_WORLD_SIZE_Z];
    public static float[,,] remainingBlockLive = new float[MAX_WORLD_SIZE_X, MAX_WORLD_SIZE_Y, MAX_WORLD_SIZE_Z];

    // Start is called before the first frame update
    void Start()
    {
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
                            hasDecayableBlock[x, y, z] = true;
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
                    if (!hasDecayableBlock[x, y, z])
                    {
                        continue;
                    }

                    remainingBlockLive[x, y, z] -= Time.deltaTime;
                    if (y != 0)
                    {
                        remainingBlockLive[x, y, z] =
                            Mathf.Min(remainingBlockLive[x, y - 1, z], remainingBlockLive[x, y, z]);
                        Debug.Log("G-" + x + "-" + y + "-" + z + ": " + remainingBlockLive[x, y, z]);
                    }

                    if (remainingBlockLive[x, y, z] > 0 && remainingBlockLive[x, y, z] < 5)
                    {
                        GameObject ground = GameObject.Find("G-" + x + "-" + y + "-" + z);
                        var decayAnimation = ground.GetComponent<DecayAnimation>();
                        if (!decayAnimation)
                        {
                            decayAnimation = ground.AddComponent<DecayAnimation>();
                            decayAnimation.timeAlive = remainingBlockLive[x, y, z];
                        }
                    }

                    if (remainingBlockLive[x, y, z] < 0)
                    {
                        hasDecayableBlock[x, y, z] = false;
                    }
                }
            }
        }
    }

    public static void addDecayableBlock(int x, int y, int z, float remainingLife)
    {
        hasDecayableBlock[x, y, z] = true;
        remainingBlockLive[x, y, z] = remainingLife;
    }

    public static void addDecayableBlock(GameObject gameObject)
    {
        if (gameObject)
        {
            int x = (int) gameObject.transform.position.x;
            int y = (int) gameObject.transform.position.y;
            int z = (int) gameObject.transform.position.z;

            var decayInitializer = gameObject.GetComponent<DecayInitializer>();
            if (decayInitializer)
            {
                hasDecayableBlock[x, y, z] = true;
                remainingBlockLive[x, y, z] = decayInitializer.decayTime;
            }
        }
    }

    public static void removeDecayableBlock(int x, int y, int z)
    {
        hasDecayableBlock[x, y, z] = false;
        remainingBlockLive[x, y, z] = 0;
    }

    public static bool canWalkOn(Vector3 position)
    {
        int x = (int) position.x;
        int y = (int) position.y - 1;
        int z = (int) position.z;

        if (x < 0 || y < 0 || z < 0 || x >= MAX_WORLD_SIZE_X || y >= MAX_WORLD_SIZE_Y || z >= MAX_WORLD_SIZE_Z)
        {
            return false;
        }

        return hasDecayableBlock[x, y, z] && remainingBlockLive[x, y, z] > 0;
    }

    public static bool canPlace(Vector3 position)
    {
        int x = (int) position.x;
        int y = (int) position.y;
        int z = (int) position.z;

        if (x < 0 || y < 0 || z < 0 || x >= MAX_WORLD_SIZE_X || y >= MAX_WORLD_SIZE_Y || z >= MAX_WORLD_SIZE_Z)
        {
            return false;
        }

        return !hasDecayableBlock[x, y, z];
    }
}
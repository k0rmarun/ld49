using System;
using UnityEngine;

public class DecayManager : MonoBehaviour
{
    public const int MAX_WORLD_SIZE_X = 100;
    public const int MAX_WORLD_SIZE_Y = 20;
    public const int MAX_WORLD_SIZE_Z = 100;

    public static bool[,,] hasDecayableBlock = new bool[MAX_WORLD_SIZE_X, MAX_WORLD_SIZE_Y, MAX_WORLD_SIZE_Z];
    public static bool[,,] buildBlocker = new bool[MAX_WORLD_SIZE_X, MAX_WORLD_SIZE_Y, MAX_WORLD_SIZE_Z];
    public static float[,,] remainingBlockLive = new float[MAX_WORLD_SIZE_X, MAX_WORLD_SIZE_Y, MAX_WORLD_SIZE_Z];
    public static GameObject[,,] objects = new GameObject[MAX_WORLD_SIZE_X, MAX_WORLD_SIZE_Y, MAX_WORLD_SIZE_Z];
    public static bool[,,] falling = new bool[MAX_WORLD_SIZE_X, MAX_WORLD_SIZE_Y, MAX_WORLD_SIZE_Z];

    // Start is called before the first frame update
    void Start()
    {
        var ground = GameObject.Find("Ground");
        foreach (var decayInitializer in ground.GetComponentsInChildren<DecayInitializer>())
        {
            addDecayableBlock(decayInitializer.gameObject);
        }

        foreach (var buildingBlocker in ground.GetComponentsInChildren<BuildingBlocker>())
        {
            addBuildBlocker(buildingBlocker.gameObject);
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
                    if (canDecay(x, y, z))
                    {
                        if (hasDecayableBlock[x, y, z] &&
                            ((falling[x, y, z]) || (y > 0 && !hasDecayableBlock[x, y - 1, z])))
                        {
                            GameObject ground = objects[x, y, z];
                            if (ground)
                            {
                                DropBlock(ground, x, y, z);
                            }
                        }
                    }

                    if (remainingBlockLive[x, y, z] > 0 && remainingBlockLive[x, y, z] < 5)
                    {
                        GameObject ground = objects[x, y, z];
                        AddDecayAnimation(ground, remainingBlockLive[x, y, z]);
                    }

                    if (remainingBlockLive[x, y, z] < 0)
                    {
                        hasDecayableBlock[x, y, z] = false;
                    }
                }
            }
        }
    }

    private static void AddDecayAnimation(GameObject ground, float initialRemainingLive)
    {
        if (ground)
        {
            var decayAnimation = ground.GetComponent<DecayAnimation>();
            if (!decayAnimation)
            {
                decayAnimation = ground.AddComponent<DecayAnimation>();
                decayAnimation.timeAlive = initialRemainingLive;
            }
        }
    }

    private bool canDecay(int x, int y, int z)
    {
        return remainingBlockLive[x, y, z] < 100_000;
    }

    private static void DropBlock(GameObject ground, int x, int y, int z)
    {
        var newPosition = ground.transform.position + Vector3.down * 0.1f;
        int newY = (int) newPosition.y;

        if (newY != y && hasDecayableBlock[x, newY, z])
        {
            return;
        }

        ground.transform.position = newPosition;

        if (newY != y)
        {
            if (y != 0)
            {
                falling[x, y - 1, z] = true;
            }
            else
            {
                AddDecayAnimation(ground, 0);
            }

            falling[x, y, z] = false;
            if (y != 0)
            {
                hasDecayableBlock[x, y - 1, z] = true;
            }

            hasDecayableBlock[x, y, z] = false;

            if (y != 0)
            {
                remainingBlockLive[x, y - 1, z] = remainingBlockLive[x, y, z];
            }

            remainingBlockLive[x, y, z] = 0;

            if (y != 0)
            {
                buildBlocker[x, y - 1, z] = buildBlocker[x, y, z];
            }

            buildBlocker[x, y, z] = false;

            if (y != 0)
            {
                objects[x, y - 1, z] = objects[x, y, z];
            }

            objects[x, y, z] = null;
        }
    }

    public static void addDecayableBlock(GameObject gameObject)
    {
        if (gameObject)
        {
            int x = (int) gameObject.transform.position.x;
            int y = (int) gameObject.transform.position.y;
            int z = (int) gameObject.transform.position.z;

            if (x < 0 || y < 0 || z < 0 || x > MAX_WORLD_SIZE_X || y > MAX_WORLD_SIZE_Y || z > MAX_WORLD_SIZE_Z)
            {
                return;
            }

            var decayInitializer = gameObject.GetComponent<DecayInitializer>();
            if (decayInitializer)
            {
                hasDecayableBlock[x, y, z] = true;
                remainingBlockLive[x, y, z] = decayInitializer.decayTime;
                objects[x, y, z] = gameObject;
            }
        }
    }

    private void addBuildBlocker(GameObject gameObject)
    {
        if (gameObject)
        {
            int x = (int) gameObject.transform.position.x;
            int y = (int) gameObject.transform.position.y;
            int z = (int) gameObject.transform.position.z;

            if (x < 0 || y < 0 || z < 0 || x > MAX_WORLD_SIZE_X || y > MAX_WORLD_SIZE_Y || z > MAX_WORLD_SIZE_Z)
            {
                return;
            }

            buildBlocker[x, y, z] = true;
            objects[x, y, z] = gameObject;
        }
    }

    public static void removeDecayableBlock(GameObject gameObject)
    {
        removeDecayableBlock(gameObject.transform.position);
    }

    public static void removeDecayableBlock(Vector3 position)
    {
        int x = (int) position.x;
        int y = (int) position.y;
        int z = (int) position.z;
        removeDecayableBlock(x, y, z);
    }

    public static void removeDecayableBlock(int x, int y, int z)
    {
        hasDecayableBlock[x, y, z] = false;
        remainingBlockLive[x, y, z] = 0;
        objects[x, y, z] = null;
    }

    public static void DecayBlocks(Vector3 position, int distance, float baseDecayLeft, Func<GameObject, bool> filter)
    {
        int x = (int) position.x;
        int y = (int) position.y;
        int z = (int) position.z;
        DecayBlocks(x, y, z, distance, baseDecayLeft, filter);
    }

    public static void DecayBlocks(int x, int y, int z, int distance, float baseDecayLeft,
        Func<GameObject, bool> filter)
    {
        Vector2 planeOrigin = new Vector2(x, z);
        Vector2 planeLoc = new Vector2();

        for (int lX = x - distance; lX <= x + distance; lX++)
        {
            for (int lY = y - distance; lY <= y + distance; lY++)
            {
                for (int lZ = z - distance; lZ <= z + distance; lZ++)
                {
                    if (lX is < 0 or >= MAX_WORLD_SIZE_X
                        || lY is < 0 or >= MAX_WORLD_SIZE_Y
                        || lZ is < 0 or >= MAX_WORLD_SIZE_Z)
                    {
                        continue;
                    }

                    var currentObj = objects[lX, lY, lZ];
                    if (currentObj && filter(currentObj))
                    {
                        planeLoc.Set(lX, lZ);
                        remainingBlockLive[lX, lY, lZ]
                            = baseDecayLeft * Mathf.Max(Vector2.Distance(planeLoc, planeOrigin) / 2, 1f);
                    }
                }
            }
        }
    }

    public static bool canWalkOn(Vector3 position)
    {
        int x = (int) position.x;
        int y = (int) position.y;
        int z = (int) position.z;

        if (x < 0 || y < 0 || z < 0 || x >= MAX_WORLD_SIZE_X || y + 1 >= MAX_WORLD_SIZE_Y || z >= MAX_WORLD_SIZE_Z)
        {
            return false;
        }

        return hasDecayableBlock[x, y, z] && remainingBlockLive[x, y, z] > 0 && !buildBlocker[x, y, z] &&
               !hasPickupable(x, y + 1, z) && !hasDecayableBlock[x, y + 1, z];
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

        return !hasDecayableBlock[x, y, z] && !buildBlocker[x, y, z];
    }

    public static bool canDrop(Vector3 position)
    {
        int x = (int) position.x;
        int y = (int) position.y;
        int z = (int) position.z;

        if (x < 0 || y < 1 || z < 0 || x >= MAX_WORLD_SIZE_X || y >= MAX_WORLD_SIZE_Y || z >= MAX_WORLD_SIZE_Z)
        {
            return false;
        }

        return hasDecayableBlock[x, y - 1, z] && !hasDecayableBlock[x, y, z] && !buildBlocker[x, y, z] &&
               !buildBlocker[x, y - 1, z];
    }

    public static Pickupable getPickupable(Vector3 position)
    {
        int x = (int) position.x;
        int y = (int) position.y;
        int z = (int) position.z;

        if (x < 0 || y < 0 || z < 0 || x >= MAX_WORLD_SIZE_X || y >= MAX_WORLD_SIZE_Y || z >= MAX_WORLD_SIZE_Z)
        {
            return null;
        }

        GameObject ground = objects[x, y, z];
        if (ground)
        {
            return ground.GetComponent<Pickupable>();
        }

        return null;
    }

    public static bool hasPickupable(int x, int y, int z)
    {
        GameObject ground = objects[x, y, z];
        if (ground)
        {
            return ground.GetComponent<Pickupable>();
        }

        return false;
    }

    public static void adjustDecay(Vector3 position, float remainingLifeTime)
    {
        int x = (int) position.x;
        int y = (int) position.y;
        int z = (int) position.z;

        if (x < 0 || y < 0 || z < 0 || x >= MAX_WORLD_SIZE_X || y >= MAX_WORLD_SIZE_Y || z >= MAX_WORLD_SIZE_Z)
        {
            return;
        }

        if (hasDecayableBlock[x, y, z])
        {
            remainingBlockLive[x, y, z] = Mathf.Min(remainingBlockLive[x, y, z], remainingLifeTime);
        }
    }

    public static WalkOverDecay getWalkOverDecay(Vector3 position)
    {
        int x = (int) position.x;
        int y = (int) position.y;
        int z = (int) position.z;

        if (x < 0 || y < 0 || z < 0 || x >= MAX_WORLD_SIZE_X || y >= MAX_WORLD_SIZE_Y || z >= MAX_WORLD_SIZE_Z)
        {
            return null;
        }

        GameObject ground = objects[x, y, z];
        if (ground)
        {
            return ground.GetComponent<WalkOverDecay>();
        }

        return null;
    }
}
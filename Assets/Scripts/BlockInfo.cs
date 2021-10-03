using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BlockInfo : MonoBehaviour
    {
        public List<BlockType> tags = new List<BlockType>();

        public static BlockInfo GetInfo(GameObject gObject)
        {
            return gObject ? gObject.GetComponent<BlockInfo>() : null; 
        }

        public static BlockInfo GetInfo(int x, int y, int z)
        {
            return GetInfo(DecayManager.objects[x, y, z]);
        } 
    }
}
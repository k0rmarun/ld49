using UnityEngine;

namespace Actions
{
    public class CliffExplosion : MonoBehaviour, IDropAction
    {
         public int ExplosionRange = 1;
        
        public void Drop(Vector3 actionLocation)
        {
            DecayManager.DecayBlocks(actionLocation, 3);
        }
    }
}
using System;
using UnityEngine;

namespace Behaviour
{
    public class Explode : MonoBehaviour
    {
        public int ExplosionRange = 3;
        public float delay = 3f;

        private float delayTimer;
        
        private Vector3 location;
        
        private void Start()
        {
            location.Set((int) transform.position.x, (int) transform.position.y, (int) transform.position.z);
        }

        private void Update()
        {
            delayTimer += Time.deltaTime;

            if (delayTimer > delay)
            {
                DecayManager.DecayBlocks(location, ExplosionRange, 0.4f);
                Destroy(this);
            }
        }
    }
}
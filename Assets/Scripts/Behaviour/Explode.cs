using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Behaviour
{
    public class Explode : MonoBehaviour
    {
        public GameObject explodeEffect;
        public int ExplosionRange = 3;
        public float delay = 3f;
        public List<BlockType> AffectBlockType;

        private float delayTimer;

        private Vector3 location;

        private void Start()
        {
            RandomizedSound.Play(transform, RandomizedSound.FUSE);
            location.Set((int) transform.position.x, (int) transform.position.y, (int) transform.position.z);
        }

        private void Update()
        {
            delayTimer += Time.deltaTime;

            if (delayTimer > delay)
            {
                RandomizedSound.Play(transform, RandomizedSound.EXPLODE, true);
                DecayManager.DecayBlocks(location, ExplosionRange, 0.4f, o
                    =>
                {
                    var info = BlockInfo.GetInfo(o);
                    return info && info.tags
                        .Select(x => x)
                        .Intersect(AffectBlockType)
                        .Any();
                });

                if (explodeEffect)
                {
                    Instantiate(explodeEffect, location, new Quaternion());
                }

                DecayManager.removeDecayableBlock(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
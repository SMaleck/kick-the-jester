using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.App.ParticleEffects
{
    public enum ParticleEffect { ImpactDust };

    public class PfxService
    {
        private readonly List<ParticleSystem> pool;

        public PfxService()
        {
            pool = new List<ParticleSystem>();
        }


        public void PlayAt(GameObject pfx, Vector3 position)
        {
            ParticleSystem pfxSlot = GetSlotFor(pfx);

            pfxSlot.transform.position = position;
            pfxSlot.Play();
        }



        private ParticleSystem GetSlotFor(GameObject pfx)
        {
            var slot = FindFreeSlotInPool(pfx.name);

            if (slot == null)
            {
                slot = CreateNewSlot(pfx);
            }

            return slot;
        }


        // Looks for a free slot of the same effect name in the exisiting pool
        private ParticleSystem FindFreeSlotInPool(string pfxName)
        {
            return pool.FirstOrDefault(e => !e.isPlaying && e.gameObject.name.Equals(pfxName));
        }


        // Creates a new slot in the pool
        private ParticleSystem CreateNewSlot(GameObject pfx)
        {
            var go = GameObject.Instantiate(pfx);
            go.name = pfx.name;
            Object.DontDestroyOnLoad(go);

            pool.Add(go.GetComponent<ParticleSystem>());

            return pool.Last();
        }
    }
}

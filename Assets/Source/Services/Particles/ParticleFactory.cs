using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Services.Audio;
using Assets.Source.Util;
using UnityEngine;

namespace Assets.Source.Services.Particles
{
    public class ParticleFactory : IPrefabFactory<PoolableParticleSystem>
    {
        public PoolableParticleSystem CreateResource(GameObject prefab)
        {
            var go = GameObject.Instantiate(prefab);
            go.name = prefab.name;

            Object.DontDestroyOnLoad(go);

            var particleSystem = go.GetComponent<ParticleSystem>();

            return new PoolableParticleSystem(particleSystem);
        }
    }
}

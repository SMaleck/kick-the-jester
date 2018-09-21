using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Util;

namespace Assets.Source.Services.Particles
{
    public class ParticleFactory : IFactory<PoolableParticle>
    {
        public PoolableParticle CreateResource()
        {
            throw new NotImplementedException();
        }
    }
}

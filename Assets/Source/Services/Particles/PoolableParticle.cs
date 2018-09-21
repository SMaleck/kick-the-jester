using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Util.Poolable;

namespace Assets.Source.Services.Particles
{
    public class PoolableParticle : IPoolableResource, IStoppable
    {
        public bool IsFree { get; }
        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}

using Assets.Source.Entities.GenericComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Entities.Jester.Components
{
    public class SoundEffect : AbstractPausableComponent<JesterEntity>
    {
        public SoundEffect(JesterEntity owner)
            : base(owner)
        {
        }
    }
}

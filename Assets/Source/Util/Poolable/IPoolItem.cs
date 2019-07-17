using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Util.Poolable
{
    public interface IPoolItem
    {
        bool IsFree { get; }
    }
}

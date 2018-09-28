using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Mvc.Views;

namespace Assets.Source.Mvc.Controllers
{
    class PauseController : ClosableController
    {
        public PauseController(PauseView view)
            : base(view)
        {

        }
    }
}

using Assets.Source.Mvc.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Mvc.Controllers
{
    public class CreditsController : AbstractController
    {
        private readonly CreditsView _creditsView;

        public CreditsController(CreditsView creditsView)
        {
            _creditsView = creditsView;            
        }
    }
}

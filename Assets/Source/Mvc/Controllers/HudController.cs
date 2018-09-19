using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;

namespace Assets.Source.Mvc.Controllers
{
    public class HudController : AbstractController
    {
        private readonly HudView _view;
        private readonly FlightStatsModel _model;

        public HudController(HudView view, FlightStatsModel model)
        {
            _view = view;
            _view.Initialize();

            _model = model;
        }
    }
}

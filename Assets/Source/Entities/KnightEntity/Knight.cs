using Assets.Source.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using Zenject;

namespace Assets.Source.Entities.KnightEntity
{
    public class Knight : AbstractMonoEntity
    {
        private UserControlService _userControlService;

        [Inject]
        public void Initialize(UserControlService userControlService)
        {
            _userControlService = userControlService;

            _userControlService.OnKick
                .Subscribe(_ => OnKick())
                .AddTo(this);
        }

        private void OnKick()
        {

        }
    }
}

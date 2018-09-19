using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Source.Services
{
    public class UserControlService
    {
        public ReactiveCommand OnKick = new ReactiveCommand();
        public ReactiveCommand OnPause = new ReactiveCommand();

        public UserControlService()
        {
            
        }
    }
}

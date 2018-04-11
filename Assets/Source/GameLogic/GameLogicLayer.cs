using Assets.Source.GameLogic.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameLogicLayer : MonoBehaviour
    {
        private List<IPausable> PausableComponents = new List<IPausable>();
        private bool IsPaused = false;

        void Start()
        {
            IsPaused = false;
        }

        void Update() { }


        public void RegisterPausable(IPausable pausable)
        {
            if(pausable != null)
            {
                PausableComponents.Add(pausable);
            }            
        }


        public void UpdatePausable()
        {
            IsPaused = !IsPaused;

            foreach(IPausable pausable in PausableComponents)
            {
                pausable.SetPause(IsPaused);
            }
        }
    }
}

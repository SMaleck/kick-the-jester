using UniRx;
using UnityEngine;

namespace Assets.Source.Services
{
    // ToDo PauseView needs to block input
    // ToDo Kick/Shot is triggered when clicking pause button
    public class UserControlService
    {
        public ReactiveCommand OnKick = new ReactiveCommand();
        public ReactiveCommand OnPause = new ReactiveCommand();

        public UserControlService()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => OnUpdate());            
        }

        
        private void OnUpdate()
        {            
            if (Input.GetButtonDown("Kick") || Input.GetMouseButtonDown(0))
            {                
                OnKick.Execute();
            }

            if (Input.GetButtonDown("Pause"))
            {
                OnPause.Execute();
            }
        }
    }
}

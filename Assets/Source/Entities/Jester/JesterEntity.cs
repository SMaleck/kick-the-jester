using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester
{
    public class JesterEntity : AbstractMonoEntity
    {
        private Rigidbody2D _goBody;
        public Rigidbody2D GoBody
        {
            get
            {
                if (_goBody == null)
                {
                    _goBody = gameObject.GetComponent<Rigidbody2D>();
                }

                return _goBody;
            }
        }

        public ReactiveCommand OnKicked = new ReactiveCommand();
        public ReactiveCommand OnShot = new ReactiveCommand();


        public override void Initialize()
        {
            
        }
    }
}

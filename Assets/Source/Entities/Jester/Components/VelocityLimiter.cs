using Assets.Source.Entities.GenericComponents;
using Assets.Source.Features.PlayerData;
using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class VelocityLimiter : AbstractPausableComponent<JesterEntity>
    {
        private readonly PlayerAttributesModel _playerAttributesModel;

        private float VelocityX
        {
            get { return Owner.GoBody.velocity.x; }
        }

        private float VelocityY
        {
            get { return Owner.GoBody.velocity.y; }
        }


        public VelocityLimiter(JesterEntity owner, PlayerAttributesModel playerAttributesModel)
            : base(owner)
        {
            _playerAttributesModel = playerAttributesModel;

            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(owner);
        }


        private void OnLateUpdate()
        {
            float clampedX = Mathf.Clamp(VelocityX, 0, _playerAttributesModel.MaxVelocityX.Value);
            float clampedY = Mathf.Clamp(VelocityY, -float.MaxValue, _playerAttributesModel.MaxVelocityY.Value);

            Owner.GoBody.velocity = new Vector2(clampedX, clampedY);
        }
    }
}

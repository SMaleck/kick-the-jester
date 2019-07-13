using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Features.PlayerData
{
    public class PlayerAttributesModel : AbstractDisposable
    {
        private readonly ReactiveProperty<float> _kickForce;
        public IReadOnlyReactiveProperty<float> KickForce => _kickForce;

        private readonly ReactiveProperty<float> _shootForce;
        public IReadOnlyReactiveProperty<float> ShootForce => _shootForce;

        private readonly ReactiveProperty<int> _projectileCount;
        public IReadOnlyReactiveProperty<int> ProjectileCount => _projectileCount;

        private readonly ReactiveProperty<float> _maxVelocityX;
        public IReadOnlyReactiveProperty<float> MaxVelocityX => _maxVelocityX;

        private readonly ReactiveProperty<float> _maxVelocityY;
        public IReadOnlyReactiveProperty<float> MaxVelocityY => _maxVelocityY;

        public PlayerAttributesModel()
        {
            _kickForce = new ReactiveProperty<float>().AddTo(Disposer);
            _shootForce = new ReactiveProperty<float>().AddTo(Disposer);
            _projectileCount = new ReactiveProperty<int>().AddTo(Disposer);
            _maxVelocityX = new ReactiveProperty<float>().AddTo(Disposer);
            _maxVelocityY = new ReactiveProperty<float>().AddTo(Disposer);
        }

        public void SetKickForce(float value)
        {
            _kickForce.Value = value;
        }

        public void SetShootForce(float value)
        {
            _shootForce.Value = value;
        }

        public void SetShots(int value)
        {
            _projectileCount.Value = value;
        }

        public void SetMaxVelocityX(float value)
        {
            _maxVelocityX.Value = value;
        }

        public void SetMaxVelocityY(float value)
        {
            _maxVelocityY.Value = value;
        }
    }
}

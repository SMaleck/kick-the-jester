using DG.Tweening;
using System;
using UniRx;

namespace Assets.Source.Util
{
    public enum TweenDisposalBehaviour
    {
        None,
        Complete,
        Rewind
    }

    public class TweenDisposable : IDisposable
    {
        private Tween _tween;
        private readonly TweenDisposalBehaviour _disposalBehaviour;

        public TweenDisposable(Tween tween, TweenDisposalBehaviour disposalBehaviour)
        {
            _tween = tween;
            _disposalBehaviour = disposalBehaviour;
        }

        public void Dispose()
        {
            if (_tween != null)
            {
                switch (_disposalBehaviour)
                {
                    case TweenDisposalBehaviour.None:
                        _tween.Kill();
                        break;
                    case TweenDisposalBehaviour.Complete:
                        _tween.Kill(true);
                        break;
                    case TweenDisposalBehaviour.Rewind:
                        _tween.Rewind();
                        _tween.Kill();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _tween = null;
        }
    }

    public static class DoTweenDisposableExtensions
    {
        public static Tween AddTo(
            this Tween tween,
            CompositeDisposable disposer,
            TweenDisposalBehaviour tweenDisposalBehaviour = TweenDisposalBehaviour.None)
        {
            new TweenDisposable(tween, tweenDisposalBehaviour).AddTo(disposer);
            return tween;
        }
    }
}

using UniRx;

namespace Assets.Source.App
{
    public class AppState
    {
        public BoolReactiveProperty IsPausedProperty = new BoolReactiveProperty(false);
    }
}

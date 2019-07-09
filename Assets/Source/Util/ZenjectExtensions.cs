using Zenject;

namespace Assets.Source.Util
{
    public static class ZenjectExtensions
    {
        public static IfNotBoundBinder AsSingleNonLazy(this ScopeConcreteIdArgConditionCopyNonLazyBinder binder)
        {
            return binder.AsSingle().NonLazy();
        }

        public static ConditionCopyNonLazyBinder BindPrefabFactory<TContract, TFactory>(
            this DiContainer diContainer)
            where TFactory : PlaceholderFactory<UnityEngine.Object, TContract>
        {
            return diContainer.BindFactory<UnityEngine.Object, TContract, TFactory>()
                .FromFactory<PrefabFactory<TContract>>();
        }
    }
}

using UnityEngine;
using System.Collections;
using Zenject;
using Assets.Source.UI.Panels;
using System;

public class PanelFactory : IFactory<UnityEngine.Object, AbstractPanel>
{
    private readonly DiContainer _container;

    public PanelFactory(DiContainer container)
    {
        _container = container;
    }

    public AbstractPanel Create(UnityEngine.Object prefab)
    {
        return _container.InstantiatePrefabForComponent<AbstractPanel>(prefab);
    }
}

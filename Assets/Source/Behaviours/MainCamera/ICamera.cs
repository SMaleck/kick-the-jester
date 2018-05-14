using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera
{
    public interface ICamera
    {
        Camera UCamera { get; }

        float Height { get; }
        float Width { get; }

        void FadeIn(NotifyEventHandler callback);
        void FadeOut(NotifyEventHandler callback);
    }
}

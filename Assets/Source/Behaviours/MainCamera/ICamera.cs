using Assets.Source.Util;
using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera
{
    public interface ICamera
    {
        Camera UCamera { get; }

        float Height { get; }
        float Width { get; }
    }
}

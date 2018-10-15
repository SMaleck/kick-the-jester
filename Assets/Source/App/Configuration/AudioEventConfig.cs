using Assets.Source.Mvc.ServiceControllers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.App.Configuration
{
    // ToDo [IMPORTANT] Set this config up
    [CreateAssetMenu(fileName = "ViewAudioConfig", menuName = "KTJ/Config/ViewAudioConfig")]
    public class AudioEventConfig : ScriptableObject
    {
        public List<ViewAudioEventSetting> ViewAudioEventSettings;
        public List<ButtonAudioEventSetting> ButtonAudioEventSettings;
    }

    [Serializable]
    public class ViewAudioEventSetting
    {
        public ViewAudioEvent AudioEventType;
        public AudioClip AudioClip;
    }

    [Serializable]
    public class ButtonAudioEventSetting
    {
        public ButtonAudioEvent AudioEventType;
        public AudioClip AudioClip;
    }
}

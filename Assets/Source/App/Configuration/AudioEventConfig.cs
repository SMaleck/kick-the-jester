using Assets.Source.Mvc.ServiceControllers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.App.Configuration
{
    [CreateAssetMenu(fileName = "ViewAudioConfig", menuName = Constants.PROJECT_MENU_ROOT + "/ViewAudioConfig")]
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

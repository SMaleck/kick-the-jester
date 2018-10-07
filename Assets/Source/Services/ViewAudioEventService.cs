using Assets.Source.Services.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Services
{
    // ToDo Add sound effects to config and listen to Message Broker    
    public class ViewAudioEventService
    {
        private readonly AudioService _audioService;

        public ViewAudioEventService(AudioService audioService)
        {
            _audioService = audioService;
        }
    }
}

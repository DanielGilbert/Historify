using HistoryForSpotify.Core.AudioServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistoryForSpotify.Core.AudioServices.Delegates;

namespace HistoryForSpotify.Core.AudioServices
{
    public class SpotifyAudioService : IAudioService
    {
        public string Name
        {
            get
            {
                return "Spotify";
            }
        }

        public event NewTrackDelegate OnNewSong = delegate { };
        public event NewTrackTimeDelegate OnNewTrackTime = delegate { };
        public event ServiceConnectedDelegate OnServiceConnected = delegate { };
        public event ServiceDisconnectedDelegate OnServiceDisconnected = delegate { };

        public void Connect()
        {
            
        }

        public void Disconnect()
        {

        }
    }
}

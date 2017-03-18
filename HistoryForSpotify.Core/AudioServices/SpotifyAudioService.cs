using HistoryForSpotify.Core.AudioServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistoryForSpotify.Core.AudioServices.Delegates;
using HistoryForSpotify.Commons.Logging.Interfaces;
using SpotifyAPI.Local;

namespace HistoryForSpotify.Core.AudioServices
{
    public class SpotifyAudioService : IAudioService
    {
        private ILog _log;
        private SpotifyLocalAPI _spotify;

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

        public SpotifyAudioService(ILog log)
        {
            _log = log;
            _spotify = new SpotifyLocalAPI();
        }

        public void Connect()
        {
            
            if (!SpotifyLocalAPI.IsSpotifyRunning())
                return; //Make sure the spotify client is running

            if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning())
                return; //Make sure the WebHelper is running

            if (!_spotify.Connect())
                return; //We need to call Connect before fetching infos, this will handle Auth stuff

            OnServiceConnected();
            //StatusResponse status = _spotify.GetStatus(); //status contains infos
        }

        public void Disconnect()
        {

        }
    }
}

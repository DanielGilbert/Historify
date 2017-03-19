using HistoryForSpotify.Core.AudioServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistoryForSpotify.Core.AudioServices.Delegates;
using HistoryForSpotify.Commons.Logging.Interfaces;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Models;

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
            Task.Factory.StartNew(() => ConnectInternal());
        }

        private void ConnectInternal()
        {
            bool isRunning = false;

            while (!isRunning)
            {
                isRunning = SpotifyLocalAPI.IsSpotifyRunning() && SpotifyLocalAPI.IsSpotifyWebHelperRunning() && _spotify.Connect();
            }
            OnServiceConnected();

            StatusResponse status = _spotify.GetStatus(); //status contains infos        
        }

        public void Disconnect()
        {

        }
    }
}

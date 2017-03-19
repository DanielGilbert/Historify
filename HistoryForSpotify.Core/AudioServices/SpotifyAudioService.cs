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
using HistoryForSpotify.Commons.Models;

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

        public event NewHistoryItemDelegate OnNewHistoryItem = delegate { };
        public event NewHistoryItemTrackTimeDelegate OnNewHistoryItemTrackTime = delegate { };
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
            _spotify.ListenForEvents = true;
            _spotify.OnTrackChange += OnTrackChange;
            _spotify.OnTrackTimeChange += OnTrackTimeChange;

            while (!isRunning)
            {
                isRunning = SpotifyLocalAPI.IsSpotifyRunning() && SpotifyLocalAPI.IsSpotifyWebHelperRunning() && _spotify.Connect();
            }

            OnServiceConnected();

            StatusResponse status = _spotify.GetStatus(); //status contains infos        
        }

        private void OnTrackTimeChange(object sender, TrackTimeChangeEventArgs e)
        {
            OnNewHistoryItemTrackTime(e.TrackTime);
        }

        private void OnTrackChange(object sender, TrackChangeEventArgs e)
        {
            if (e.NewTrack.IsAd()) return;

            StatusResponse status = _spotify.GetStatus(); //status contains infos 
            HistoryItem historyItem = new HistoryItem();
            historyItem.Album = e.NewTrack.AlbumResource.Name;
            historyItem.AlbumUri = new Uri(e.NewTrack.AlbumResource.Location.Og);
            historyItem.Artist = e.NewTrack.ArtistResource.Name;
            historyItem.ArtistUri = new Uri(e.NewTrack.ArtistResource.Location.Og);
            historyItem.Name = e.NewTrack.TrackResource.Name;
            historyItem.TrackUri = new Uri(e.NewTrack.TrackResource.Location.Og);
            historyItem.TrackLength = status.Track.Length;
            historyItem.IsCompleted = false;
            
            OnNewHistoryItem(historyItem);
        }

        public void Disconnect()
        {

        }
    }
}

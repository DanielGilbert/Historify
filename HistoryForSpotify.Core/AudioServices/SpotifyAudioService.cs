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
        private HistoryItem _currentHistoryItem;

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
        }

        private void OnTrackTimeChange(object sender, TrackTimeChangeEventArgs e)
        {
            OnNewHistoryItemTrackTime(e.TrackTime);
        }

        private void OnTrackChange(object sender, TrackChangeEventArgs e)
        {
            if (e.NewTrack.IsAd()) return;

            OnNewHistoryItem(GetHistoryItemFromTrack(e.NewTrack));
        }

        private HistoryItem GetHistoryItemFromTrack(Track spotifyTrack)
        {
            HistoryItem historyItem = new HistoryItem();
            historyItem.Album = spotifyTrack.AlbumResource.Name;
            historyItem.AlbumUri = new Uri(spotifyTrack.AlbumResource.Location.Og);
            historyItem.Artist = spotifyTrack.ArtistResource.Name;
            historyItem.ArtistUri = new Uri(spotifyTrack.ArtistResource.Location.Og);
            historyItem.Name = spotifyTrack.TrackResource.Name;
            historyItem.TrackUri = new Uri(spotifyTrack.TrackResource.Location.Og);
            historyItem.TrackLength = spotifyTrack.Length;
            historyItem.IsCompleted = false;

            return historyItem;
        }

        public void Disconnect()
        {

        }

        public HistoryItem GetCurrentHistoryItem()
        {
            
            if (_currentHistoryItem == null)
            {
                StatusResponse status = _spotify.GetStatus();   
                _currentHistoryItem = GetHistoryItemFromTrack(status.Track);
            }

            return _currentHistoryItem;
        }
    }
}

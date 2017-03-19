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
        private DateTime _lastSubmitTime;

        private Track _currentTrack;

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
            _lastSubmitTime = DateTime.Now;
        }

        public void Connect()
        {
            Task.Factory.StartNew(() => ConnectInternal());
        }

        private void ConnectInternal()
        {
            bool isRunning = false;
            _spotify.ListenForEvents = true;
            _spotify.OnTrackTimeChange += OnTrackTimeChange;

            while (!isRunning)
            {
                isRunning = SpotifyLocalAPI.IsSpotifyRunning() && SpotifyLocalAPI.IsSpotifyWebHelperRunning() && _spotify.Connect();
            }

            OnServiceConnected();  
        }

        private void OnTrackTimeChange(object sender, TrackTimeChangeEventArgs e)
        {

            if (_currentTrack == null) return;

            Console.WriteLine(e.AssociatedTrack.TrackResource.Uri);
            Console.WriteLine(_currentTrack.TrackResource.Uri);
            Console.WriteLine(e.TrackTime);

            if (!String.Equals(e.AssociatedTrack.TrackResource.Uri, _currentTrack.TrackResource.Uri))
            {
                OnNewHistoryItem(GetHistoryItemFromTrack(e.AssociatedTrack));
                _currentTrack = e.AssociatedTrack;
            }
            else
            {
                OnNewHistoryItemTrackTime(e.TrackTime);
            }
        }

        private HistoryItem GetHistoryItemFromTrack(Track spotifyTrack)
        {
            HistoryItem historyItem = new HistoryItem();
            historyItem.Album = spotifyTrack.AlbumResource.Name;
            historyItem.AlbumUri = new Uri(spotifyTrack.AlbumResource.Location.Og);
            historyItem.AlbumArtUrl = spotifyTrack.GetAlbumArtUrl(SpotifyAPI.Local.Enums.AlbumArtSize.Size160);
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
                _currentTrack = status.Track;
                _currentHistoryItem = GetHistoryItemFromTrack(status.Track);
            }

            return _currentHistoryItem;
        }
    }
}

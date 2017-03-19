using HistoryForSpotify.Commons.Models;
using HistoryForSpotify.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HistoryForSpotify.ViewModels
{
    public class HistoryItemViewModel : ViewModelBase
    {
        private HistoryItem _historyItem;

        public string Name
        {
            get
            {
                return _historyItem.Name;
            }
            set
            {
                _historyItem.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Artist
        {
            get
            {
                return _historyItem.Artist;
            }
            set
            {
                _historyItem.Artist = value;
                OnPropertyChanged(nameof(Artist));
            }
        }

        public string Album
        {
            get
            {
                return _historyItem.Album;
            }
            set
            {
                _historyItem.Album = value;
                OnPropertyChanged(nameof(Album));
            }
        }

        public string CurrentPosition
        {
            get
            {
                return string.Format("{0}:{1:00}", Math.Floor(_historyItem.CurrentPosition / 60),
                              (Math.Abs(Math.Floor(_historyItem.CurrentPosition)) % 60));
            }
            set
            {
                OnPropertyChanged(nameof(CurrentPosition));
            }
        }

        public BitmapImage AlbumArt
        {
            get
            {
                return _historyItem.AlbumArt;
            }
            set
            {
                _historyItem.AlbumArt = value;
                OnPropertyChanged(nameof(AlbumArt));
            }
        }

        public Guid Id
        {
            get
            {
                return _historyItem.Id;
            }
        }

        public event Action<HistoryItem> OnPlayHistoryItem = delegate { };
        public event Action<HistoryItem, HistoryItemViewModel> OnDeleteHistoryItem = delegate { };
        public ICommand PlayHistoryItemCommand { get; set; }
        public ICommand DeleteHistoryItemCommand { get; set; }

        public HistoryItemViewModel(HistoryItem historyItem)
        {
            PlayHistoryItemCommand = new RelayCommand(PlayHistoryItem);
            DeleteHistoryItemCommand = new RelayCommand(DeleteHistoryItem);

            _historyItem = historyItem;

            _historyItem.OnAlbumArtLoaded += OnAlbumArtLoaded;

            _historyItem.DownloadAlbumBitmapAsync();
        }

        private void DeleteHistoryItem(object obj)
        {
            OnDeleteHistoryItem(_historyItem, this);
        }

        private void PlayHistoryItem(object obj)
        {
            OnPlayHistoryItem(_historyItem);
        }

        private void OnAlbumArtLoaded(BitmapImage obj)
        {
            DispatcherObject.BeginInvoke((Action)(() =>
            {
                AlbumArt = obj;
            }));
        }

        public void UpdateTrackTime(double trackTime)
        {
            _historyItem.CurrentPosition = trackTime;
            OnPropertyChanged(nameof(CurrentPosition));
        }
    }
}

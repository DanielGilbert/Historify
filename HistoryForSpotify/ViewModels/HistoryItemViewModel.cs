﻿using HistoryForSpotify.Commons.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public HistoryItemViewModel(HistoryItem historyItem)
        {
            _historyItem = new HistoryItem();

            _historyItem.Name = historyItem.Name;
            _historyItem.Album = historyItem.Album;
            _historyItem.Artist = historyItem.Artist;
            _historyItem.AlbumArtUrl = historyItem.AlbumArtUrl;
            _historyItem.AlbumArt = historyItem.AlbumArt;
            _historyItem.OnAlbumArtLoaded += OnAlbumArtLoaded;

            _historyItem.DownloadAlbumBitmapAsync();
        }

        private void OnAlbumArtLoaded(BitmapImage obj)
        {
            DispatcherObject.BeginInvoke((Action)(() =>
            {
                AlbumArt = obj;
            }));
        }
    }
}

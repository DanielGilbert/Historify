using HistoryForSpotify.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public HistoryItemViewModel(HistoryItem historyItem)
        {
            _historyItem = new HistoryItem();

            _historyItem.Name = historyItem.Name;
            _historyItem.Album = historyItem.Album;
            _historyItem.Artist = historyItem.Artist;
        }
    }
}

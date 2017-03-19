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

        public HistoryItemViewModel(HistoryItem historyItem)
        {
            _historyItem = new HistoryItem();

            _historyItem.Name = historyItem.Name;
        }
    }
}

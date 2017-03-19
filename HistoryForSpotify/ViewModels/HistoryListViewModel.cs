using HistoryForSpotify.Commons.Logging.Interfaces;
using HistoryForSpotify.Commons.Models;
using HistoryForSpotify.Core.AudioServices.Interfaces;
using HistoryForSpotify.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.ViewModels
{
    public class HistoryListViewModel : ViewModelBase, IHistoryListViewModel
    {
        private ILog _log;
        private IAudioService _audioService;
        private ObservableCollection<HistoryItemViewModel> _historyItemViewModels;

        public ObservableCollection<HistoryItemViewModel> HistoryItemViewModels
        {
            get
            {
                return _historyItemViewModels;
            }
            set
            {
                _historyItemViewModels = value;
                OnPropertyChanged(nameof(HistoryItemViewModel));
            }
        }

        public HistoryListViewModel(ILog log, IAudioService audioService)
        {
            _log = log;
            _audioService = audioService;

            _audioService.OnNewHistoryItem += OnNewHistoryItem;
            _audioService.OnNewHistoryItemTrackTime += OnNewHistoryItemTrackTime;
            _audioService.OnServiceConnected += OnServiceConnected;

            _historyItemViewModels = new ObservableCollection<HistoryItemViewModel>();

            _log.Debug("Created HistoryListViewModel");
        }

        private void OnServiceConnected()
        {
            OnNewHistoryItem(_audioService.GetCurrentHistoryItem());
        }

        private void OnNewHistoryItem(HistoryItem historyItem)
        {
            DispatcherObject.BeginInvoke((Action)(() =>
            {
                _historyItemViewModels.Insert(0, new HistoryItemViewModel(historyItem));
            }));
            
        }

        private void OnNewHistoryItemTrackTime(double trackTime)
        {
        }
    }
}